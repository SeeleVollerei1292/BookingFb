using bookingfootball.IRepository;
using bookingfootball.IRepository.Repository;
using Microsoft.EntityFrameworkCore;



using bookingfootball.Constract;
using bookingfootball.Db_QL;
using bookingfootball.Service;
using Microsoft.AspNetCore.Identity;
using Mvc.Areas.Admin.IServices;
using Mvc.Areas.Admin.IServices.Services;
using bookingfootball.Data;
using Mvc.Areas.Admin.IService;
using Mvc.Areas.Admin.IService.Service;
using DuongPia.Areas.Admin.IServices;
using DuongPia.Areas.Admin.IServices.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<SbDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IThongKeService, ThongKeService>();


builder.Services.AddControllersWithViews().ConfigureApplicationPartManager(manager =>
{
    // Loại bỏ assembly chứa controller API
    var apiAssembly = typeof(bookingfootball.Controllers.AuthController).Assembly;
    var part = manager.ApplicationParts.FirstOrDefault(p => p.Name == apiAssembly.GetName().Name);
    if (part != null)
    {
        manager.ApplicationParts.Remove(part);
    }
}); 
builder.Services.AddHttpClient();
builder.Services.AddScoped<INhanVienServices, NhanVienServices>();
builder.Services.AddScoped<INuocuongServices, NuocuongServices>();
builder.Services.AddScoped<IAuthAPIService, AuthAPIService>();
builder.Services.AddScoped<IDoThueService, DoThueServices>();

//builder.Services.AddScoped<IAuthService, AuthService>();



// Add services to the container.

// Thêm HttpClientFactory

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Auth/SignIn";
        options.LogoutPath = "/Auth/Logout";
    });
// Thêm session
builder.Services.AddDistributedMemoryCache(); // Dùng bộ nhớ tạm để lưu session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian sống của session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=SanBong}/{action=Index}/{id?}"
    );
});

app.Run();
