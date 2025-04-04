﻿using ShopNow.Application.DTOs.Carts;
using ShowNow.Domain.Entities;

namespace ShopNow.Application.Services.Interfaces
{
	public interface ICartService
	{
		Task<bool> AddToCart(Guid userId, AddCartItemDTO addCartItemDTO);
		Task<CartDTO> GetCart(Guid userId);
		Task<bool> RemoveFromCart(Guid userId, Guid itemId);
		Task<bool> UpdateCartItem(Guid userId, UpdateCartItemDTO updateCartItemDTO);
	}
}
