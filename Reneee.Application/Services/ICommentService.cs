using Reneee.Application.DTOs.Comment;

namespace Reneee.Application.Services
{
    public interface ICommentService
    {
        Task<CommentDto> CreateComment(CreateCommentDto commentRequest);
        Task<IReadOnlyList<CommentDto>> GetCommentByProduct(int id);
    }
}
