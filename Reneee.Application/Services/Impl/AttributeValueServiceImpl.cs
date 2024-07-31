using AutoMapper;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.AttributeValue;
using Reneee.Application.Exceptions;
using Reneee.Domain.Entities;

namespace Reneee.Application.Services.Impl
{
    public class AttributeValueServiceImpl(IAttributeValueRepository attributeValueRepository,
                                           IUnitOfWork unitOfWork,
                                           IAttributeRepository attributeRepository,
                                           IMapper mapper) : IAttributeValueService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IAttributeRepository _attributeRepository = attributeRepository;
        private readonly IAttributeValueRepository _attributeValueRepository = attributeValueRepository;
        public async Task<AttributeValueDto> CreateAttributeValue(CreateAttributeValueDto attributeValueRequest)
        {
            var attributeEntity = await _attributeRepository.Get(attributeValueRequest.AttributeId)
                ?? throw new NotFoundException("Attribute not found");
            var attributeValueEntity = new AttributeValue
            {
                Value = attributeValueRequest.Value,
                Status = 0,
                Attribute = attributeEntity
            };
            AttributeValue savedAttributeValue = await _attributeValueRepository.Add(attributeValueEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AttributeValueDto>(savedAttributeValue);
        }
    }
}
