using ServerLibary.Model.Responses;

namespace ServerLibary.Model.Params
{
    public class ThanhToanParam
    {
        public int? Id_KhachHang { get; set; }
        public int? Id_DiaChi { get; set; }
        public string? PhuongThucThanhToan { get; set; } = "Thanh toán khi nhận hàng (COD)";
    }
}
