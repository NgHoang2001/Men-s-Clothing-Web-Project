using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class DonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("KhachHang")]
        public int? Id_KhachHang { get; set; }
        [ForeignKey("DiaChi")]
        public int? Id_DiaChi { get; set; } = null;
        //public string? DiaChiChiTiet { get; set; }
        public DateTime ThoiGianTao { get; set; }

        public virtual KhachHang? KhachHang { get; set; }
        public virtual DiaChi? DiaChi { get; set; }

        public virtual ICollection<DonHangChiTiet>? DonHangChiTiets { get; set; }
        public virtual ICollection<DonHang_TrangThaiDonHang>? DonHang_TrangThaiDonHangs { get; set; }
    }
}
