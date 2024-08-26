using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class DonHang_TrangThaiDonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("DonHang")]
        public int Id_DonHang { get; set; }
        [ForeignKey("TrangThaiDonHang")]
        public int Id_TrangThaiDonHang { get; set; }
        public DateTime? ThoiGianTao { get; set; }

        public virtual DonHang? DonHang { get; set; }
        public virtual TrangThaiDonHang? TrangThaiDonHang { get; set; }

    }
}
