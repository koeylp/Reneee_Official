using System.ComponentModel.DataAnnotations;

namespace Reneee.Domain.Entities
{
    public class Ward
    {
        [Key]
        public string code { get; set; }
        public string full_name_en { get; set; }
        public string district_code { get; set; }
    }
}
