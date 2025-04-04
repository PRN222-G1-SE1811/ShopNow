﻿using AutoMapper;
using ShopNow.Application.DTOs.CheckOut;
using ShopNow.Application.DTOs.Orders;
using ShowNow.Domain.Entities;

namespace ShopNow.Application.Mappers
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<CreateOrderDTO, Order>();
			CreateMap<CheckOutItemDTO, OrderItem>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductVariantId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
				.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));
			CreateMap<OrderDTO, Order>().ReverseMap();
			CreateMap<OrderItem, OrderItemDTO>();
			CreateMap<Order, OrderDetailDTO>()
				.ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
		}
	}
}
