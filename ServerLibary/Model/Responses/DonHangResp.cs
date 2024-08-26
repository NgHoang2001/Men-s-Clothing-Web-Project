namespace ServerLibary.Model.Responses
{
    public class TrangThaiDonHang
    {
        public int? Id { get; set; }
        public string? Ten { get; set; }
    }
    public class DonHangResp
    {
        public int? Id { get; set; } // id đơn hàng trong csdl
        public string? MaDon { get; set; }
        public DateTime? NgayDatHang { get; set; }
        public int? SoLuong { get; set; }
        public decimal? TongTien { get; set; }
        public TrangThaiDonHang? TrangThaiDonHang { get; set; }
    }
}
