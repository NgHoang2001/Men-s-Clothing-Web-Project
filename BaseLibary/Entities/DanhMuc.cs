using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class DanhMuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? Id_DanhMuc_Cha { get; set; } = null;
        [MaxLength(100)]
        public string? Ten { get; set; }

        public virtual ICollection<SanPham>? SanPhams { get; set; }
        public virtual ICollection<HinhAnh>? HinhAnhs { get; set; }

    }
}
