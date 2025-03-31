using ShopNow.Application.DTOs.User;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IUserService
	{
		Task<UserDetailDTO> GetUserDetail(Guid userId);
	}
}
