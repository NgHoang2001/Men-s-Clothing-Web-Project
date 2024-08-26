namespace ServerLibary.Model.Params
{
    public class ThemSanPhamGioHangParam
    {
        public int? Id_KhachHang { get; set; }
        public int? Id_SanPhamChiTiet { get; set; }
        public int SoLuong { get; set; } = 1;
    }
}
