using AutoMapper;
using ShopNow.Application.DTOs.Carts;
using ShowNow.Domain.Entities;

namespace ShopNow.Application.Mappers
{
	public class CartProfile : Profile
	{
		public CartProfile()
		{
			CreateMap<AddCartItemDTO, CartItem>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductVariantId));
		}
	}
}
