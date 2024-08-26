using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class SanPham_SuKien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("SanPham")]
        public int Id_SanPham { get; set; }
        [ForeignKey("SuKien")]
        public int Id_SuKien { get; set; }
        public int DonGia { get; set; }
        public bool TrangThai { get; set; } = true;
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }

        public virtual SanPham SanPham { get; set; }
        public virtual SuKien SuKien { get; set; }
    }
}
