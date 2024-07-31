using AutoMapper;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.Attribute;
using Reneee.Domain.Entities;
using Attribute = Reneee.Domain.Entities.Attribute;

namespace Reneee.Application.Services.Impl
{
    public class AttributeServiceImpl(IAttributeRepository attributeRepository, IMapper mapper, IUnitOfWork unitOfWork) : IAttributeService
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
    }
}
