using Reneee.Application.DTOs.Product;
using Reneee.Application.DTOs.User;

namespace Reneee.Application.DTOs.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public UserDto User { get; set; }
        public ProductDto Product { get; set; }
    }
}
