using bookingfootball.Data;
using bookingfootball.Db_QL;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mvc.Models;
using Mvc.Services;
using System;
using System.Text.Json;
using System.Web;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using static Mvc.Services.VNPayLibrary;

namespace Mvc.Controllers
{

    public class PaymentController : Controller
    {
        private readonly SbDbcontext _context;
        private readonly IVnpay _vnpay;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(
            SbDbcontext context,
            IConfiguration configuration,
            ILogger<PaymentController> logger,
            IVnpay vnpay)
        {
            _context = context;
            _logger = logger;
            _vnpay = vnpay;

            // Kiểm tra cấu hình VNPay
            var tmnCode = configuration["VNPay:TmnCode"];
            var hashSecret = configuration["VNPay:HashSecret"];
            var baseUrl = configuration["VNPay:BaseUrl"];
            var returnUrl = configuration["VNPay:ReturnUrl"];

            if (string.IsNullOrEmpty(tmnCode) || string.IsNullOrEmpty(hashSecret) ||
                string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(returnUrl))
            {
                _logger.LogError("VNPay configuration error: TmnCode={TmnCode}, HashSecret={HashSecret}, BaseUrl={BaseUrl}, ReturnUrl={ReturnUrl}",
                    tmnCode ?? "null", hashSecret ?? "null", baseUrl ?? "null", returnUrl ?? "null");
                throw new ArgumentException("Không tìm thấy BaseUrl, TmnCode, HashSecret, hoặc ReturnUrl");
            }

            // Khởi tạo VNPay
            _vnpay.Initialize(tmnCode, hashSecret, baseUrl, returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> InitiatePayment(int hoaDonId)
        {
            _logger.LogInformation("InitiatePayment: Starting for HoaDonId {HoaDonId}", hoaDonId);

            if (hoaDonId <= 0)
            {
                _logger.LogError("InitiatePayment Error: Invalid HoaDonId {HoaDonId}", hoaDonId);
                return BadRequest("Invalid HoaDonId");
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.KhachHang)
                .FirstOrDefaultAsync(h => h.Id == hoaDonId);

            if (hoaDon == null)
            {
                _logger.LogError("InitiatePayment Error: HoaDonId {HoaDonId} not found", hoaDonId);
                return NotFound();
            }

            var orderInfo = $"Thanh toán hóa đơn {hoaDon.MaHoaDon} cho khách hàng {hoaDon.TenNguoiDat}";
            var request = new PaymentRequest
            {
                PaymentId = hoaDon.Id,
                Money = (double)hoaDon.TongTien,
                Description = orderInfo,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1",
                BankCode = BankCode.ANY,
                CreatedDate = DateTime.Now,
                Currency = Currency.VND,
                Language = DisplayLanguage.Vietnamese
            };

            var paymentUrl = _vnpay.GetPaymentUrl(request);
            _logger.LogInformation("Generated VNPay URL: {PaymentUrl}", paymentUrl);
            _logger.LogInformation("Order Info: {OrderInfo}, IP: {Ip}", orderInfo, request.IpAddress);

            var bookingData = HttpContext.Session.GetString("BookingData");
            if (string.IsNullOrEmpty(bookingData))
            {
                _logger.LogError("InitiatePayment Error: BookingData is empty for HoaDonId {HoaDonId}", hoaDonId);
                return RedirectToAction("Index", "SanBong");
            }

            var model = JsonSerializer.Deserialize<OrderViewModel>(bookingData);
            if (model == null)
            {
                _logger.LogError("InitiatePayment Error: Failed to deserialize BookingData for HoaDonId {HoaDonId}", hoaDonId);
                return RedirectToAction("Index", "SanBong");
            }

            model.HoaDonId = hoaDonId;
            model.PaymentUrl = paymentUrl;
            _logger.LogInformation("Passing PaymentUrl to view: {PaymentUrl}", model.PaymentUrl);

            _logger.LogInformation("InitiatePayment Success: Generated payment URL for HoaDonId {HoaDonId}, MaHoaDon {MaHoaDon}, OrderInfo: {OrderInfo}", hoaDonId, hoaDon.MaHoaDon, orderInfo);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PaymentCallback()
        {
            _logger.LogInformation("PaymentCallback: Received callback from VNPay");
            _logger.LogInformation("Received VNPay Callback at: {Url}", Request.GetDisplayUrl());
            _logger.LogInformation("VNPay Query Params: {Params}", string.Join("&", Request.Query.Select(q => $"{q.Key}={q.Value}")));

            _logger.LogDebug("PaymentCallback: Raw Request URL: {RequestUrl}, Method: {Method}, Headers: {Headers}",
                Request.GetDisplayUrl(), Request.Method, string.Join("; ", Request.Headers.Select(h => $"{h.Key}: {h.Value}")));

            if (!Request.QueryString.HasValue)
            {
                _logger.LogError("PaymentCallback Error: No query string received from VNPay");
                return View("PaymentFailed", new PaymentResponseModel
                {
                    Success = false,
                    Message = "Không nhận được dữ liệu từ VNPay"
                });
            }

            try
            {
                var paymentResult = _vnpay.GetPaymentResult(Request.Query);
                _logger.LogInformation("VNPay Result - Success: {IsSuccess}, Code: {Code}, Description: {Desc}, PaymentId: {Pid}",
                    paymentResult.IsSuccess, paymentResult.PaymentResponse.Code, paymentResult.PaymentResponse.Description, paymentResult.PaymentId);

                var orderId = paymentResult.PaymentId.ToString(); // Convert long to string
                var vnPayTranId = paymentResult.VnpayTransactionId.ToString();
                var responseCode = paymentResult.PaymentResponse.Code == 0 ? "00" : paymentResult.PaymentResponse.Code.ToString();
                var orderInfo = paymentResult.Description;

                if (!paymentResult.IsSuccess)
                {
                    _logger.LogError("PaymentCallback Error: Invalid signature or failed transaction for OrderId {OrderId}, ResponseCode: {ResponseCode}", orderId, responseCode);
                    return View("PaymentFailed", new PaymentResponseModel
                    {
                        Success = false,
                        OrderId = orderId,
                        OrderDescription = orderInfo,
                        VnPayResponseCode = responseCode,
                        Message = paymentResult.PaymentResponse.Description ?? "Sai chữ ký hoặc giao dịch thất bại."
                    });
                }

                var hoaDon = await _context.HoaDons.FirstOrDefaultAsync(h => h.Id.ToString() == orderId);
                if (hoaDon == null)
                {
                    _logger.LogError("PaymentCallback Error: HoaDon with Id {OrderId} not found", orderId);
                    return View("PaymentFailed", new PaymentResponseModel
                    {
                        Success = false,
                        OrderId = orderId,
                        OrderDescription = orderInfo,
                        VnPayResponseCode = responseCode,
                        Message = "Không tìm thấy hóa đơn."
                    });
                }

                if (responseCode == "00")
                {
                    hoaDon.TrangThaiThanhToan = "Hoàn thành";
                    hoaDon.TrangThaiHoaDon = TrangThaiHoaDon.DaThanhToan;
                    hoaDon.VNPayTransactionId = vnPayTranId;
                    _logger.LogInformation("PaymentCallback Success: HoaDon {OrderId} updated to Success, TrangThaiHoaDon: {TrangThaiHoaDon}", orderId, hoaDon.TrangThaiHoaDon);
                }
                else
                {
                    hoaDon.TrangThaiThanhToan = "Failed";
                    hoaDon.TrangThaiHoaDon = TrangThaiHoaDon.DaHuy;
                    _logger.LogInformation("PaymentCallback Failed: HoaDon {OrderId} updated to Failed, TrangThaiHoaDon: {TrangThaiHoaDon}, VnPayResponseCode: {VnpResponseCode}", orderId, hoaDon.TrangThaiHoaDon, responseCode);
                }

                try
                {
                    _logger.LogInformation("Saving HoaDon update to DB: MaHoaDon = {MaHoaDon}, TrangThai = {Status}", hoaDon.MaHoaDon, hoaDon.TrangThaiHoaDon);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("HoaDon {OrderId} status updated successfully", orderId);
                    _logger.LogInformation("PaymentCallback: Updated status for HoaDon {OrderId}", orderId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "PaymentCallback Error: Failed to update HoaDon {OrderId}", orderId);
                    return View("PaymentFailed", new PaymentResponseModel
                    {
                        Success = false,
                        OrderId = orderId,
                        OrderDescription = orderInfo,
                        VnPayResponseCode = responseCode,
                        Message = "Không thể cập nhật trạng thái hóa đơn."
                    });
                }

                var resultModel = new PaymentResponseModel
                {
                    Success = paymentResult.IsSuccess,
                    PaymentMethod = paymentResult.PaymentMethod ?? "VNPay",
                    OrderDescription = orderInfo,
                    OrderId = orderId,
                    PaymentId = vnPayTranId,
                    TransactionId = vnPayTranId,
                    Token = Request.Query["vnp_SecureHash"].ToString() ?? "",
                    VnPayResponseCode = responseCode
                };

                return View(resultModel.Success ? "PaymentSuccess" : "PaymentFailed", resultModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PaymentCallback Error: Exception processing callback");
                return View("PaymentFailed", new PaymentResponseModel
                {
                    Success = false,
                    Message = $"Lỗi xử lý callback: {ex.Message}"
                });
            }
        }

        [HttpGet]
        public IActionResult PaymentSuccess(PaymentResponseModel model)
        {
            _logger.LogInformation("PaymentSuccess: Showed success view for {OrderId}", model.OrderId);
            return View(model);
        }

        [HttpGet]
        public IActionResult PaymentFailed(PaymentResponseModel model)
        {
            _logger.LogInformation("PaymentFailed: Showed failure view for {OrderId}, Message: {Message}", model.OrderId, model.Message);
            return View(model);
        }
    }
}