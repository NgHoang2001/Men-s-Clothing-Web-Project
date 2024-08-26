using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class MauSac
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Ten { get; set; }
        public string? MaMau { get; set; } // Mã màu
        public string? Url { get; set; }
        public virtual ICollection<SanPhamChiTiet>? SanPhamChiTiets { get; set; }

    }
}
