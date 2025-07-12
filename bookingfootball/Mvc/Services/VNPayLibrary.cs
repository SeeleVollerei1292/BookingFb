using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Mvc.Services
{
    public class VNPayLibrary
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<VNPayLibrary> _logger;

        public VNPayLibrary(IConfiguration configuration, ILogger<VNPayLibrary> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public string CreatePaymentUrl(decimal amount, string orderId, string orderInfo)
        {
            var vnpay = new SortedList<string, string>(new VnPayCompare());
            vnpay.Add("vnp_Amount", ((long)(amount * 100)).ToString());
            vnpay.Add("vnp_Command", "pay");
            vnpay.Add("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.Add("vnp_CurrCode", "VND");
            vnpay.Add("vnp_IpAddr", "127.0.0.1");
            vnpay.Add("vnp_Locale", "vn");
            vnpay.Add("vnp_OrderInfo", orderInfo);
            vnpay.Add("vnp_OrderType", "other");
            vnpay.Add("vnp_ReturnUrl", _configuration.GetValue<string>("VnPay:ReturnUrl"));
            vnpay.Add("vnp_TmnCode", _configuration.GetValue<string>("VnPay:TmnCode"));
            vnpay.Add("vnp_TxnRef", orderId);
            vnpay.Add("vnp_Version", "2.1.0");

            // Không encode key trong signData
            var signData = string.Join("&", vnpay.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            var hash = HmacSHA512(_configuration.GetValue<string>("VnPay:HashSecret"), signData);

            // Encode value trong query string
            var queryString = string.Join("&", vnpay.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            queryString += "&vnp_SecureHash=" + hash;

            var url = $"{_configuration.GetValue<string>("VnPay:BaseUrl")}?{queryString}";
            _logger.LogInformation("CreatePaymentUrl: Generated payment URL for OrderId {OrderId}, SignData: {SignData}, SecureHash: {SecureHash}", orderId, signData, hash);
            return url;
        }

        public bool ValidatePayment(string responseData)
        {
            var vnpay = new SortedList<string, string>(new VnPayCompare());
            var responseParams = responseData.Split('&');
            foreach (var param in responseParams)
            {
                var keyValue = param.Split('=', 2);
                if (keyValue.Length == 2)
                {
                    vnpay.Add(keyValue[0], keyValue[1]); // Sử dụng giá trị gốc
                    _logger.LogDebug("ValidatePayment: Added {Key}={Value}", keyValue[0], keyValue[1]);
                }
            }

            var secureHash = vnpay["vnp_SecureHash"];
            vnpay.Remove("vnp_SecureHash");

            var signData = string.Join("&", vnpay.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            var computedHash = HmacSHA512(_configuration.GetValue<string>("VnPay:HashSecret"), signData);

            _logger.LogDebug("ValidatePayment: SignData: {SignData}", signData);
            _logger.LogDebug("ValidatePayment: ComputedHash: {ComputedHash}", computedHash);
            _logger.LogDebug("ValidatePayment: ReceivedHash: {ReceivedHash}", secureHash);

            var isValid = secureHash.Equals(computedHash, StringComparison.InvariantCultureIgnoreCase);
            if (!isValid)
            {
                _logger.LogError("ValidatePayment: Invalid signature for OrderId {OrderId}. ComputedHash: {ComputedHash}, ReceivedHash: {ReceivedHash}", vnpay["vnp_TxnRef"], computedHash, secureHash);
            }
            else
            {
                _logger.LogInformation("ValidatePayment: Signature validated successfully for OrderId {OrderId}", vnpay["vnp_TxnRef"]);
            }

            return isValid;
        }

        private string HmacSHA512(string key, string inputData)
        {
            var hash = new StringBuilder();
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }
            return hash.ToString();
        }

        public class VnPayCompare : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                return CompareInfo.GetCompareInfo("en-US").Compare(x, y, CompareOptions.Ordinal);
            }
        }

        public class PaymentResponseModel
        {
            public bool Success { get; set; }
            public string PaymentMethod { get; set; }
            public string OrderDescription { get; set; }
            public string OrderId { get; set; }
            public string PaymentId { get; set; }
            public string TransactionId { get; set; }
            public string Token { get; set; }
            public string VnPayResponseCode { get; set; }
            public string Message { get; set; }
        }
    }
}