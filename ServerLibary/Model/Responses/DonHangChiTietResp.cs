namespace ServerLibary.Model.Responses
{
    public class DonHangChiTietResp
    {
        public DiaChiDatHangResp? DiaChi { get; set; }
        public string? HinhThucGiaoHang { get; set; } = "Giao hàng tại nhà";
        public string? HinhThucThanhToan { get; set; } = "Thanh toán khi nhận hàng";
        public DateTime? NgayMuaHang { get; set; }
        public TrangThaiDonHang? TrangThaiDonHang { get; set; }
        public decimal? TongTien { get; set; } = 0;
        public List<SanPhamChiTietResp>? SanPhamChiTiet { get; set; }
    }
}
