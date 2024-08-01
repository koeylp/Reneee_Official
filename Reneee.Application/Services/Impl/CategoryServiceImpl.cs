using AutoMapper;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.Category;
using Reneee.Application.Exceptions;
using Reneee.Domain.Entities;

namespace Reneee.Application.Services.Impl
{
    public class CategoryServiceImpl(IUnitOfWork unitOfWork,
                                     IMapper mapper,
                                     ICategoryRepository categoryRepository,
                                     IProductRepository productRepository,
                                     ILogger<CategoryServiceImpl> logger) : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ILogger<CategoryServiceImpl> _logger = logger;

        public async Task<CategoryDto> EnableCategory(int id)
        {
            _logger.LogInformation("Entering method EnableCategory with parameter {Id}", id);
            Category foundCategory = await _categoryRepository.Get(id);
            if (foundCategory == null)
            {
                var message = $"Category with Id {id} not found";
                throw new NotFoundException(message);
            }
            if (foundCategory.Status == 1)
            {
                var message = $"Cannot enabled category with Id {id} because it was enabled";
                throw new BadRequestException(message);
            }
            Category updatedCategory = await _categoryRepository.UpdateCategoryStatus(foundCategory, 1);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(updatedCategory);
        }

        public async Task<CategoryDto> CreateCategory(CreateCategoryDto request)
        {
            _logger.LogInformation("Entering method EnableCategory with Body CreateCategoryDto");
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

        public async Task<string> DeleteCategory(int id)
        {
            _logger.LogInformation("Entering method DeleteCategory with parameter {Id}", id);
            var categoryEntity = await _categoryRepository.Get(id);
            if (categoryEntity == null)
            {
                var message = $"Category with Id {id} not found";
                throw new NotFoundException(message);
            }
            if (categoryEntity.Status == 1)
            {
                var message = $"Cannot delete category with Id {id} because it is enabled";
                throw new BadRequestException(message);
            }
            await _categoryRepository.Delete(categoryEntity);
            await _unitOfWork.SaveChangesAsync();
            return "Done deleting";
        }

        public async Task<CategoryDto> DisableCategory(int id)
        {
            Category foundCategory = await _categoryRepository.Get(id);
            if (foundCategory == null)
            {
                var message = $"Category with Id {id} not found";
                throw new NotFoundException(message);
            }
            if (foundCategory.Status == 0)
            {
                var message = $"Cannot disable category with Id {id} because it was disabled";
                throw new BadRequestException(message);
            }
            var productsUsingCategory = await _productRepository.GetProductsByCategoryId(id);
            if (productsUsingCategory.Any())
            {
                var message = $"Cannot disable category with id - {id} because it is used by one or more products";
                throw new BadRequestException(message);
            }
            Category updatedCategory = await _categoryRepository.UpdateCategoryStatus(foundCategory, 0);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(updatedCategory);
        }
    }
}
