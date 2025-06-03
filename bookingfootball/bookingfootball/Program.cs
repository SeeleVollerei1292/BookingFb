using bookingfootball;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mvc.Data;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()  // Cho phép tất cả phương thức (GET, POST, PUT, DELETE)
                .AllowAnyHeader() // Cho phép tất cả header
                .AllowCredentials() // Nếu dùng Cookie hoặc JWT
                .WithExposedHeaders("Authorization");
        });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  
})
.AddCookie("Cookies") // Cookie authentication scheme

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
builder.Services.AddInfrastructureServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
// Thêm các dịch vụ API
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

// Thêm dịch vụ Swagger
builder.Services.AddSession(options =>
{

    options.IdleTimeout = TimeSpan.FromSeconds(90);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
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

 // Middleware Authorization

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<SbDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseSession();
app.UseAuthentication(); // Middleware Authentication

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
