using ClientLibary.Helpers;
using ClientLibary.Services.Client.Constracts;
using ClientLibary.Services.Client.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHttpClient<GetHttpClient>("SystemApiClient", clients =>
{
    clients.BaseAddress = new Uri("https://localhost:7041");
});
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<IUserAuthentication, UserAuthentication>();
builder.Services.AddScoped<IGioHangClientServices, GioHangClientServices>();
builder.Services.AddScoped<ISanPhamApiServices, SanPhamApiServices>();
builder.Services.AddScoped<ITaiKhoanClientServices, TaiKhoanClientServices>();
builder.Services.AddScoped<IThanhToanClientServices, ThanhToanClientServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TrangChu}/{action=Index}/{id?}");

app.Run();
