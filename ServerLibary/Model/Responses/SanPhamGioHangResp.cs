namespace ServerLibary.Model.Responses
{
    public class SanPhamGioHangResp
    {
        public int? Id { get; set; } //id gio hang
        public int? SoLuong { get; set; }
        public SanPhamModel? SanPham { get; set; }
        public SanPhamChiTietModel? SanPhamChiTiet { get; set; }
        public bool IsChon { get; set; } = true;
    }
}
