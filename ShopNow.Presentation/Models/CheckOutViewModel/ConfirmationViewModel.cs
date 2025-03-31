﻿using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Application.DTOs.CheckOut;
using ShopNow.Application.DTOs.User;

namespace ShopNow.Presentation.Models.CheckOutViewModel
{
	public class ConfirmationViewModel
	{
		public List<CheckOutItemDTO> Items { get; set; } = null!;
		public UserDetailDTO UserDetailDTO { get; set; } = null!;
	}
}
