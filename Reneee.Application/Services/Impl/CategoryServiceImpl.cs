using AutoMapper;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.Category;
using Reneee.Domain.Entities;

namespace Reneee.Application.Services.Impl
{
    public class CategoryServiceImpl(IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<CategoryDto> CreateCategory(CreateCategoryDto request)
        {
            var categoryEntity = _mapper.Map<Category>(request);

            await _categoryRepository.Add(categoryEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(categoryEntity);
        }
    }
}
