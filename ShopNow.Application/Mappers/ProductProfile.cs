using AutoMapper;
using ShopNow.Application.DTOs.Products;
using ShowNow.Domain.Entities;

namespace ShopNow.Application.Mappers
{
	public class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<CreateProductDTO, Product>();
			CreateMap<Product, ProductDetailDTO>().ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category!.Name));
		}
	}
}
