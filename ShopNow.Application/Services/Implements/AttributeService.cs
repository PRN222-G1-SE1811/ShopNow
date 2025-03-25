using AutoMapper;
using ShopNow.Application.DTOs.Prodducts;
using ShopNow.Application.Services.Interfaces;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;

namespace ShopNow.Application.Services.Implements
{
	public class AttributeService(IUnitOfWork<ShowNow.Domain.Entities.Attribute, Guid> unitOfWork, IMapper mapper) : IAttributeService
	{
		public async Task<List<Guid>> AddAttributes(List<AttributeDTO> attributeDTOs)
		{
			var attributes = mapper.Map<List<ShowNow.Domain.Entities.Attribute>>(attributeDTOs);
			await unitOfWork.GenericRepository.InsertRange(attributes);
			return attributes.Select(x => x.Id).ToList();
		}
	}
}
