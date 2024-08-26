using BaseLibary.Responses;
using ClientLibary.Helpers;
using ClientLibary.Services.Client.Constracts;
using ServerLibary.Model.Params;
using ServerLibary.Model.Responses;
using System.Net.Http.Json;

namespace ClientLibary.Services.Client.Implementations
{
    public class TaiKhoanClientServices : ITaiKhoanClientServices
    {
        private IUserAuthentication _servicesAuthen;
        private GetHttpClient _httpClient;
        public const string AuthUrl = "api/TaiKhoan/";
        public TaiKhoanClientServices(GetHttpClient httpClient, IUserAuthentication servicesAuthen)
        {
            this._httpClient = httpClient;
            _servicesAuthen = servicesAuthen;
        }

        public async Task<GeneralRespone> CapNhatDiaChiGiaoHang(DiaChiDatHangParam param)
        {
            var user = await _servicesAuthen.GetUserInfoClient();
            if (user == null || user.Id_KhachHang == null)
            {
                return new GeneralRespone(false, "Cập nhật địa chỉ thất bại");
            }

            var httpClient = _httpClient.GetHttpClientA();

            var respModel = await httpClient.PostAsJsonAsync($"{AuthUrl}cap-nhat-dia-chi-giao-hang", param);
            if (!respModel.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Cập nhật địa chỉ thất bại"); ;
            }
            return await respModel.Content.ReadFromJsonAsync<GeneralRespone>();
        }

        public async Task<GeneralRespone> ThemDiaChiGiaoHang(DiaChiDatHangParam param)
        {
            var user = await _servicesAuthen.GetUserInfoClient();
            if (user == null || user.Id_KhachHang == null)
            {
                return new GeneralRespone(false, "Thêm mới địa chỉ thất bại");
            }

            var httpClient = _httpClient.GetHttpClientA();

            var respModel = await httpClient.PostAsJsonAsync($"{AuthUrl}them-moi-dia-chi-giao-hang?id={user.Id_KhachHang}", param);
            if (!respModel.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Thêm mới địa chỉ thất bại"); ;
            }
            return await respModel.Content.ReadFromJsonAsync<GeneralRespone>();
        }

        public async Task<GeneralRespone> CapNhatMatKhau(CapNhatMatKhauParam param)
        {
            var user = await _servicesAuthen.GetUserInfoClient();
            if (user == null || user.Id_KhachHang == null)
            {
                return new GeneralRespone(false, "Cập nhật mật khẩu thất bại");
            }

            var httpClient = _httpClient.GetHttpClientA();

            var respModel = await httpClient.PostAsJsonAsync($"{AuthUrl}cap-nhat-mat-khau", param);
            if (!respModel.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Cập nhật mật khẩu thất bại"); ;
            }
            return await respModel.Content.ReadFromJsonAsync<GeneralRespone>();
        }

        public async Task<GeneralRespone> XoaDiaChiGiaoHang(int? idDiaChi)
        {
            var user = await _servicesAuthen.GetUserInfoClient();
            if (user == null || user.Id_KhachHang == null)
            {
                return new GeneralRespone(false, "Xóa địa chỉ thất bại");
            }

            var httpClient = _httpClient.GetHttpClientA();

            var respModel = await httpClient.DeleteAsync($"{AuthUrl}xoa-dia-chi-giao-hang?idDiaChi={idDiaChi}&id={user.Id_KhachHang}");
            if (!respModel.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Xóa địa chỉ thất bại"); ;
            }
            return await respModel.Content.ReadFromJsonAsync<GeneralRespone>();
        }

        public async Task<GeneralRespone> CapNhatThongTinTaiKhoan(ThongTinKhachHangParam? param)
        {
            var user = await _servicesAuthen.GetUserInfoClient();
            if (user == null || user.Id_KhachHang == null)
            {
                return new GeneralRespone(false, "Cập nhật thông tin tài khoản thất bại");
            }

            var httpClient = _httpClient.GetHttpClientA();

            var respModel = await httpClient.PostAsJsonAsync($"{AuthUrl}cap-nhat-thong-tin-tai-khoan", param);
            if (!respModel.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Cập nhật thông tin tài khoản thất bại"); ;
            }
            return await respModel.Content.ReadFromJsonAsync<GeneralRespone>();
        }

        public async Task<ThongTinKhachHangResp> ChiTietThongTinKhachHang()
        {
            var user = await _servicesAuthen.GetUserInfoClient();
            if (user == null || user.Id_KhachHang == null)
            {
                return new ThongTinKhachHangResp();
            }

            var httpClient = _httpClient.GetHttpClientA();

            var respModel = await httpClient.GetAsync($"{AuthUrl}chi-tiet-thong-tin-khach-hang?id={user.Id_KhachHang}");
            if (!respModel.IsSuccessStatusCode)
            {
                return new ThongTinKhachHangResp();
            }
            return await respModel.Content.ReadFromJsonAsync<ThongTinKhachHangResp>();
        }

        public async Task<List<DiaChiDatHangResp>> DanhSachDiaChiGiaoHang()
        {
            var resp = new List<DiaChiDatHangResp>();
            var user = await _servicesAuthen.GetUserInfoClient();
            if (user == null || user.Id_KhachHang == null)
            {
                return resp;
            }

            var httpClient = _httpClient.GetHttpClientA();

            var respModel = await httpClient.GetAsync($"{AuthUrl}danh-sach-dia-chi-giao-hang?id={user.Id_KhachHang}");
            if (!respModel.IsSuccessStatusCode)
            {
                return resp;
            }
            return await respModel.Content.ReadFromJsonAsync<List<DiaChiDatHangResp>>();
        }

        public async Task<List<DonHangResp>> DanhSachDonHang()
        {
            var resp = new List<DonHangResp>();
            var user = await _servicesAuthen.GetUserInfoClient();
            if (user == null || user.Id_KhachHang == null)
            {
                return resp;
            }

            var httpClient = _httpClient.GetHttpClientA();

            var respModel = await httpClient.GetAsync($"{AuthUrl}danh-sach-hon-hang?id={user.Id_KhachHang}");
            if (!respModel.IsSuccessStatusCode)
            {
                return resp;
            }
            return await respModel.Content.ReadFromJsonAsync<List<DonHangResp>>();
        }

        public async Task<List<HuyenResp>> DanhSachHuyenTheoId(string? code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return new List<HuyenResp>();
            }
            var respModel = new List<HuyenResp>();
            var httpClient = _httpClient.GetHttpClientA();

            var resp = await httpClient.GetAsync($"{AuthUrl}danh-sach-huyen?code={code}");
            if (!resp.IsSuccessStatusCode)
            {
                return await Task.FromResult(respModel);
            }
            return await resp.Content.ReadFromJsonAsync<List<HuyenResp>>();
        }

        public async Task<List<ThanhPhoResp>> DanhSachThanhPho()
        {
            var respModel = new List<ThanhPhoResp>();
            var httpClient = _httpClient.GetHttpClientA();

            var resp = await httpClient.GetAsync($"{AuthUrl}danh-sach-thanh-pho");
            if (!resp.IsSuccessStatusCode)
            {
                return await Task.FromResult(respModel);
            }
            return await resp.Content.ReadFromJsonAsync<List<ThanhPhoResp>>();
        }

        public async Task<List<PhuongResp>> DanhSachXaTheoId(string? code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return new List<PhuongResp>();
            }
            var respModel = new List<PhuongResp>();
            var httpClient = _httpClient.GetHttpClientA();

            var resp = await httpClient.GetAsync($"{AuthUrl}danh-sach-xa?code={code}");
            if (!resp.IsSuccessStatusCode)
            {
                return await Task.FromResult(respModel);
            }
            return await resp.Content.ReadFromJsonAsync<List<PhuongResp>>();
        }

        public async Task<DonHangChiTietResp> DonHangChiTiet(int? id)
        {
            var resp = new DonHangChiTietResp();
            var user = await _servicesAuthen.GetUserInfoClient();
            if (user == null || user.Id_KhachHang == null)
            {
                return resp;
            }

            var httpClient = _httpClient.GetHttpClientA();

            var respModel = await httpClient.GetAsync($"{AuthUrl}chi-tiet-don-hang?id={id}");
            if (!respModel.IsSuccessStatusCode)
            {
                return resp;
            }
            return await respModel.Content.ReadFromJsonAsync<DonHangChiTietResp>();
        }


    }
}
