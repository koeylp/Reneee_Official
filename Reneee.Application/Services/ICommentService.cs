using Reneee.Application.DTOs.Comment;

namespace Reneee.Application.Services
{
    public interface ICommentService
    {
        Task<CommentDto> CreateComment(CreateCommentDto commentRequest);
        Task<string> DeleteComment(int id);
        Task<CommentDto> EditComment(int id, UpdateCommentDto commentRequest);
        Task<IReadOnlyList<CommentDto>> GetCommentByProduct(int id);
    }
}
