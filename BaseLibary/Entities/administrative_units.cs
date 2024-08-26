using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class administrative_units
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string? full_name { get; set; }
        public string? full_name_en { get; set; }
        public string? short_name { get; set; }
        public string? short_name_en { get; set; }
        public string? code_name { get; set; }
        public string? code_name_en { get; set; }

        public virtual ICollection<provinces>? Provinces { get; set; }
        public virtual ICollection<districts>? Districts { get; set; }
        public virtual ICollection<wards>? Wards { get; set; }
    }
}
