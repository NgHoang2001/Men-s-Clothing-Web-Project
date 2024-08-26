using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class TrangThaiGioHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Ten { get; set; }

        public virtual ICollection<GioHangChiTiet>? GioHangChiTiets { get; set; }

    }
}
