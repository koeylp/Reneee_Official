using AutoMapper;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.Attribute;
using Reneee.Application.Exceptions;
using Attribute = Reneee.Domain.Entities.Attribute;

namespace Reneee.Application.Services.Impl
{
    public class AttributeServiceImpl(IAttributeRepository attributeRepository,
                                      IMapper mapper,
                                      IUnitOfWork unitOfWork) : IAttributeService
    {
        private readonly IAttributeRepository _attributeRepository = attributeRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<AttributeDto> CreateAttribute(CreateUpdateAttributeDto attributeRequest)
        {
            var attributeEntity = new Attribute
            {
                Name = attributeRequest.Name,
                Status = 0
            };
            Attribute savedAttribute = await _attributeRepository.Add(attributeEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AttributeDto>(savedAttribute);
        }

        public async Task<string> DeleteAttribute(int id)
        {
            var attributeEntity = await _attributeRepository.Get(id)
                                ?? throw new NotFoundException("Attribute not found");
            await _attributeRepository.Delete(attributeEntity);
            await _unitOfWork.SaveChangesAsync();
            return "Done deleting";
        }

        public async Task<IReadOnlyList<AttributeDto>> GetAllAttributes()
        {
            return _mapper.Map<IReadOnlyList<AttributeDto>>(await _attributeRepository.GetAll());
        }

        public async Task<AttributeDto> UpdateAttibute(int id, CreateUpdateAttributeDto attributeRequest)
        {
            var attributeEntity = await _attributeRepository.Get(id)
                                    ?? throw new NotFoundException("Attribute not found");
            attributeEntity.Name = attributeRequest.Name ?? attributeEntity.Name;
            await _attributeRepository.Update(attributeEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AttributeDto>(attributeEntity);
        }
    }
}
