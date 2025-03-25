using ShopNow.Application.DTOs.Prodducts;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IAttributeService
	{
		Task<List<Guid>> AddAttributes(List<AttributeDTO> attributeDTOs);
	}
}
