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

			CreateMap<Cart, CartDTO>();

			CreateMap<CartItem, CartItemDTO>()
				.ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product!.Assets![0].Path))
				.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Product!.Name))
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
				.ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Price * src.Quantity));
		}
	}
}
