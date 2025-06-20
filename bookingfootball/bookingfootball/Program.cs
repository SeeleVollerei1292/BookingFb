using bookingfootball.IRepository;
using bookingfootball.IRepository.Repository;
using bookingfootball.Data;
using bookingfootball.Interfaces;
using bookingfootball.Persistence.Repository;
using bookingfootball.Depen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using Duong_API.IRepository;
using Duong_API.IRepository.Repository;
using bookingfootball.Repository;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .WithExposedHeaders("Authorization");
    });
});

// Cấu hình Authentication (JWT + Cookies nếu cần)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie("Cookies") // Nếu bạn không dùng cookie thì có thể bỏ dòng này
.AddJwtBearer(options =>
{
    var jwtKey = builder.Configuration["JwtSettings:SecurityKey"];
    if (string.IsNullOrEmpty(jwtKey))
    {
        throw new Exception("JWT SecurityKey is missing in appsettings.json");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Thêm Distributed Memory Cache (dùng cho session)
builder.Services.AddDistributedMemoryCache();

// Thêm Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(90);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký DbContext với SQL Server
builder.Services.AddDbContext<SbDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký các repository
builder.Services.AddInfrastructureServices();
builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();
builder.Services.AddScoped<INuocuongRepository, NuocuongRepository>();
builder.Services.AddScoped<IThongKeRepository, ThongKeRepository>();
builder.Services.AddScoped<IDoThueRepository, DoThueRepository>();

builder.Services.AddScoped<ICaNhanVienRepo, CaNhanVienRepo>();

builder.Services.AddScoped<ISancaRepository, SancaRepository>();
builder.Services.AddScoped<ICaRepository, CaRepository>();

builder.Services.AddHttpContextAccessor();

// Thêm Controllers + cấu hình JSON options tránh vòng tham chiếu
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Swagger/OpenAPI setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AsmC5 API",
        Version = "v1",
        Description = "API for AsmC5 Project",
        Contact = new OpenApiContact
        {
            Name = "Tran Xuan Quang",
            Email = "your-email@example.com"
        }
    });
});

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
