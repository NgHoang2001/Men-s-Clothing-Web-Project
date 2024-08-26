using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class administrative_regions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string name { get; set; } = null!;
        public string? name_en { get; set; }
        public string? code_name { get; set; }
        public string? code_name_en { get; set; }

        public virtual ICollection<provinces>? Provinces { get; set; }


    }
}
