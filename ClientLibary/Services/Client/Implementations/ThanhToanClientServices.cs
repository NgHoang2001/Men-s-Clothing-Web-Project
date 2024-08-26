using BaseLibary.Responses;
using Client.Models;
using ClientLibary.Helpers;
using ClientLibary.Services.Client.Constracts;
using ServerLibary.Model.Params;
using System.Net.Http.Json;

namespace ClientLibary.Services.Client.Implementations
{
    public class ThanhToanClientServices : IThanhToanClientServices
    {
        private readonly IUserAuthentication _servicesAuthen;
        private GetHttpClient _httpClient;
        public const string AuthUrl = "api/ThanhToan/";

        public ThanhToanClientServices(IUserAuthentication servicesAuthen, GetHttpClient httpClient)
        {
            _servicesAuthen = servicesAuthen;
            _httpClient = httpClient;
        }

        public async Task<GeneralRespone> ThanhToanGioHang(ThanhToanSubmit param)
        {

            var httpClient = _httpClient.GetHttpClientA();
            var user = await _servicesAuthen.GetUserInfoClient();
            if (param?.Id_DiaChi == null)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            if (user == null || user.Id_KhachHang == null)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            var model = new ThanhToanParam
            {
                Id_DiaChi = param.Id_DiaChi,
                Id_KhachHang = user.Id_KhachHang,
                PhuongThucThanhToan = "Thanh toán khi nhận hàng (COD)"
            };
            var resp = await httpClient.PostAsJsonAsync($"{AuthUrl}thanh-toan-gio-hang", model);
            if (!resp.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            return await resp.Content.ReadFromJsonAsync<GeneralRespone>();
        }
    }
}
