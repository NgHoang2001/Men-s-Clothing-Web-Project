namespace ServerLibary.Model.Params
{
    public class ThongTinKhachHangParam
    {
        public int? id { get; set; } // id tai khoan
        public string? Ten { get; set; }
        public bool? GioiTinh { get; set; }
        public string? Sdt { get; set; }
        public DateTime? NgaySinh { get; set; }
    }
}
