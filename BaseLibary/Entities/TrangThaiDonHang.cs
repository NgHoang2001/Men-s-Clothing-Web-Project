using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class TrangThaiDonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(200)]
        public string? Ten { get; set; }

        public virtual ICollection<DonHang_TrangThaiDonHang>? DonHang_TrangThaiDonHangs { get; set; }

    }
}
