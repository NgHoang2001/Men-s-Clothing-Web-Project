namespace Client.Models
{
    public class ThanhToanParam
    {
        public int? Id_DiaChi { get; set; }
        public DiaChiDatHangClientParam? DiaChiParam { get; set; } = new DiaChiDatHangClientParam();
        //public bool isDefault { get; set; } = false;
    }
}
