using BaseLibary.Responses;
using ServerLibary.Model.Params;
using ServerLibary.Model.Responses;

namespace ClientLibary.Services.Client.Constracts
{
    public interface ITaiKhoanClientServices
    {
        public Task<ThongTinKhachHangResp> ChiTietThongTinKhachHang();
        public Task<GeneralRespone> CapNhatThongTinTaiKhoan(ThongTinKhachHangParam? param);
        public Task<List<DonHangResp>> DanhSachDonHang(); // id khach hang
        public Task<DonHangChiTietResp> DonHangChiTiet(int? id); // id đơn hàng chi tiết
        public Task<List<DiaChiDatHangResp>> DanhSachDiaChiGiaoHang(); // id khach hang
        public Task<GeneralRespone> CapNhatDiaChiGiaoHang(DiaChiDatHangParam param);
        public Task<GeneralRespone> ThemDiaChiGiaoHang(DiaChiDatHangParam param);
        public Task<GeneralRespone> XoaDiaChiGiaoHang(int? idDiaChi); // Xóa địa chỉ đặt hàng của khách hàng
        public Task<GeneralRespone> CapNhatMatKhau(CapNhatMatKhauParam param);
        public Task<List<ThanhPhoResp>> DanhSachThanhPho();
        public Task<List<HuyenResp>> DanhSachHuyenTheoId(string? code);
        public Task<List<PhuongResp>> DanhSachXaTheoId(string? code);
    }
}
