using System.ComponentModel.DataAnnotations;

namespace BaseLibary.Entities
{
    public class provinces
    {
        [Key]
        public string code { get; set; } = null!;
        public string name { get; set; } = null!;
        public string? name_en { get; set; }
        public string full_name { get; set; } = null!;
        public string? full_name_en { get; set; }
        public string? code_name { get; set; }
        public int? administrative_unit_id { get; set; }
        public int? administrative_region_id { get; set; }

        public virtual administrative_units? Administrative_Units { get; set; }
        public virtual administrative_regions? Administrative_Regions { get; set; }
        public virtual ICollection<districts>? Districts { get; set; }
    }
}
