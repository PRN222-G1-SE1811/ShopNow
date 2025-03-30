using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShopNow.Application.DTOs.User;
using ShopNow.Application.Services.Interfaces;
using ShowNow.Domain.Entities;

namespace ShopNow.Application.Services.Implements
{
	public class UserService(UserManager<User> userManager, IMapper mapper) : IUserService
	{
		public async Task<UserDetailDTO> GetUserDetail(Guid userId)
		{
			var user = await userManager.FindByIdAsync(userId.ToString());
			return mapper.Map<UserDetailDTO>(user);
		}
	}
}
