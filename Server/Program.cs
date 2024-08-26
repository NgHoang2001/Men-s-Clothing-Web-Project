using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Utities;
using ServerLibary.Context;
using ServerLibary.Helpers;
using ServerLibary.Respositories.Contracts;
using ServerLibary.Respositories.Implementations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddGenSwaggerConfiguration();
//

builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
var jwtsession = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();
builder.Services.AddDbContext<DuAnWebCanifaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Khong tim thay ket noi database"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtsession.Issuer!,
        ValidAudience = jwtsession.Audience!,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsession.Key!))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = ctx =>
        {
            ctx.Request.Cookies.TryGetValue("accessToken", out var accessToken);
            if (!string.IsNullOrEmpty(accessToken))
                ctx.Token = accessToken;
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddScoped<IUserAuthentication, UserAuthentication>();
builder.Services.AddScoped<IGioHangServices, GioHangServices>();
builder.Services.AddScoped<ISanPhamServices, SanPhamServices>();
builder.Services.AddScoped<IThanhToanServices, ThanhToanServices>();
builder.Services.AddScoped<ITaiKhoanServices, TaiKhoanServices>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm",
        builder => builder
        .WithOrigins("https://localhost:7250", "http://localhost:5041")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});
//


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazorWasm");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
