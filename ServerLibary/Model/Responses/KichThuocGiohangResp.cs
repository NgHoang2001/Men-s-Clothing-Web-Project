namespace ServerLibary.Model.Responses
{
    // class này trả ra với mã sp đó thì trả kèm theo danh sách các mã kích thước của sản phẩm đó
    public class KichThuocGiohangResp
    {
        public int? Id_SP { get; set; } //Id của sản phẩm
        public List<KichThuocResp>? ListKichThuoc { get; set; }
    }
}
