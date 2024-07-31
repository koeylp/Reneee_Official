using System.ComponentModel.DataAnnotations;

namespace Reneee.Application.DTOs.Attribute
{
    public class CreateUpdateAttributeDto
    {
        [Required]
        public string? Name { get; set; }
    }
}
