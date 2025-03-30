using AutoMapper;
using ShopNow.Application.DTOs.User;
using ShowNow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Application.Mappers
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserDetailDTO>();
		}
	}
}
