using AutoMapper;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.Comment;
using Reneee.Application.Exceptions;
using Reneee.Domain.Entities;

namespace Reneee.Application.Services.Impl
{
    public class CommentServiceImpl(ICommentRepository commentRepository,
                                    IUnitOfWork unitOfWork,
                                    ILogger<CommentServiceImpl> logger,
                                    IMapper mapper,
                                    IUserRepository userRepository,
                                    IProductRepository productRepository) : ICommentService
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<CommentDto> CreateComment(CreateCommentDto commentRequest)
        {
            _logger.LogInformation("Enter CreateComment method");
            var userEntity = await _userRepository.Get(1)
                            ?? throw new NotFoundException("User not found");
            var productEntity = await _productRepository.Get(commentRequest.ProductId)
                            ?? throw new NotFoundException("Product not found");
            var commentEntity = new Comment
            {
                Content = commentRequest.Content,
                Product = productEntity,
                User = userEntity,
                Status = 1
            };
            await _commentRepository.Add(commentEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CommentDto>(commentEntity);
        }

        public async Task<IReadOnlyList<CommentDto>> GetCommentByProduct(int id)
        {
            var productEntity = await _productRepository.Get(id)
                                    ?? throw new NotFoundException("Product not found");
            return _mapper.Map<IReadOnlyList<CommentDto>>(await _commentRepository.GetCommentByProduct(productEntity));
        }
    }
}
