using AutoMapper;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.Category;
using Reneee.Domain.Entities;

namespace Reneee.Application.Features.V1.Commands.Categories
{
    public class CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository)
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<CategoryDto> Handle(CreateCategoryDto request, CancellationToken cancellationToken)
        {
            var categoryEntity = _mapper.Map<Category>(request);
            await _categoryRepository.Add(categoryEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(categoryEntity);
        }

    }
}
