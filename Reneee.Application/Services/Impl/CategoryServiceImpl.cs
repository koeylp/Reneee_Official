using AutoMapper;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.Category;
using Reneee.Application.Exceptions;
using Reneee.Domain.Entities;

namespace Reneee.Application.Services.Impl
{
    public class CategoryServiceImpl(IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<CategoryDto> EnableCategory(int id)
        {
            Category foundCategory = await _categoryRepository.Get(id);
            if (foundCategory == null)
            {
                throw new NotFoundException("Category not found with id " + id);
            }
            if (foundCategory.Status == 1) throw new BadRequestException("Category was activated!");
            Category updatedCategory = await _categoryRepository.UpdateCategoryStatus(foundCategory, 1);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(updatedCategory);
        }

        public async Task<CategoryDto> CreateCategory(CreateCategoryDto request)
        {
            var categoryEntity = _mapper.Map<Category>(request);

            await _categoryRepository.Add(categoryEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(categoryEntity);
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAllActiveCategories()
        {
            IReadOnlyList<Category> categories = await _categoryRepository.GetActiveCategories();
            return _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAllCategories()
        {
            IReadOnlyList<Category> categories = await _unitOfWork.CategoryRepository.GetAll();
            return _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
        }
    }
}
