namespace ServerLibary.Model.Responses
{
    public class ThongTinKhachHangResp
    {
        public int? id_TaiKhoan { get; set; }
        public int? id_KhachHang { get; set; }
        public string? Email { get; set; }
        public bool? GioiTinh { get; set; }
        public string? Ten { get; set; }
        public string? Sdt { get; set; }
        public DateTime? NgaySinh { get; set; }
    }
}
