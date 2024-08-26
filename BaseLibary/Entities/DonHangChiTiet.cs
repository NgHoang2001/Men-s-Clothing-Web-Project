using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class DonHangChiTiet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("DonHang")]
        public int Id_DonHang { get; set; }
        [ForeignKey("SanPhamChiTiet")]
        public int Id_SanPhamChiTiet { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public DateTime ThoiGianTao { get; set; }

        public virtual DonHang? DonHang { get; set; }
        public virtual SanPhamChiTiet? SanPhamChiTiet { get; set; }
    }
}
