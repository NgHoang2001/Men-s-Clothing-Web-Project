namespace ServerLibary.Model.Responses
{
    public class KichThuocVaMauSacSanPhamDonHangResp
    {
        public int? Id { get; set; }
        public int? Id_SanPhamChiTiet { get; set; }
        public List<KichThuocResp>? KichThuocs { get; set; }
        public List<MauSacResp>? MauSacs { get; set; }
    }
}
