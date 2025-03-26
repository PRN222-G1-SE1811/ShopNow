using AutoMapper;
using ShopNow.Application.DTOs.Prodducts;

namespace ShopNow.Application.Mappers
{
	public class AttributeProfile : Profile
	{
		public AttributeProfile()
		{
			CreateMap<AttributeDTO, ShowNow.Domain.Entities.Attribute>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Key))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Value));
		}
	}
}
