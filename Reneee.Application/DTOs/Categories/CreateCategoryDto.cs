using System.ComponentModel.DataAnnotations;

namespace Reneee.Application.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100 characters.")]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
