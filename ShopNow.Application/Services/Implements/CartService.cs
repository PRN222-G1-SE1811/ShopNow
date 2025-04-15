using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopNow.Application.DTOs.Carts;
using ShopNow.Application.Services.Interfaces;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;

namespace ShopNow.Application.Services.Implements
{
	public class CartService(IUnitOfWork<Cart, Guid> unitOfWork, IMapper mapper) : ICartService
	{
		public async Task<bool> AddToCart(Guid userId, AddCartItemDTO addCartItemDTO)
		{
			var cart = await unitOfWork.GenericRepository.FirstOrDefaultAsync(x => x.CustomerId == userId);

			if (cart == null)
			{
				cart = new Cart
				{
					CustomerId = userId,
					CartItems = new List<CartItem>()
				};
				unitOfWork.GenericRepository.Insert(cart);
			}
			var query = unitOfWork.GenericRepository.GetQueryAble();
			query = query.Include(x => x.CartItems);
			cart = await query.FirstOrDefaultAsync(x => x.CustomerId == userId);
			var existingCartItem = cart!.CartItems!.FirstOrDefault(c => c.ProductId == addCartItemDTO.ProductVariantId);

			if (existingCartItem != null)
			{
				existingCartItem.Quantity += addCartItemDTO.Quantity;
			}
			else
			{
				var cartItem = mapper.Map<CartItem>(addCartItemDTO);
				cart.CartItems!.Add(cartItem);
			}
			return await unitOfWork.CommitAsync() > 0;
		}

		public async Task<CartDTO> GetCart(Guid userId)
		{
			var query = unitOfWork.GenericRepository.GetQueryAble()
				.Include(x => x.CartItems!)
				.ThenInclude(ci => ci.Product!) // include product variant
				.ThenInclude(p => p.Product!)  // Include Product to get Name
				.Include(p => p.CartItems!)
				.ThenInclude(x => x.Product)
				.ThenInclude(p => p!.Assets); // product image

			var cart = await query.FirstOrDefaultAsync(x => x.CustomerId == userId);

			if (cart == null)
			{
				cart = new Cart
				{
					CustomerId = userId,
					CartItems = new List<CartItem>()
				};

				unitOfWork.GenericRepository.Insert(cart);
				await unitOfWork.CommitAsync();
			}

			return mapper.Map<CartDTO>(cart);
		}
		public async Task<bool> RemoveFromCart(Guid userId, Guid itemId)
		{
			var cart = await unitOfWork.GenericRepository
	.GetQueryAble()
	.Include(x => x.CartItems)
	.FirstOrDefaultAsync(x => x.CustomerId == userId);

			if (cart == null) return false;

			var itemToRemove = cart.CartItems!.FirstOrDefault(c => c.Id == itemId);
			if (itemToRemove == null) return false;

			cart.CartItems!.Remove(itemToRemove);
			await unitOfWork.SaveChangesAsync();
			return true;
		}
		public async Task<bool> UpdateCartItem(Guid userId, UpdateCartItemDTO updateCartItemDTO)
		{
			var cart = await unitOfWork.GenericRepository
	.GetQueryAble()
	.Include(x => x.CartItems)
	.FirstOrDefaultAsync(x => x.CustomerId == userId);

			if (cart == null) return false;

			var itemToUpdate = cart.CartItems!.FirstOrDefault(c => c.Id == updateCartItemDTO.ItemId);
			if (itemToUpdate == null) return false;

			itemToUpdate.Quantity = updateCartItemDTO.Quantity;
			await unitOfWork.SaveChangesAsync();
			return true;
		}

	}
}
