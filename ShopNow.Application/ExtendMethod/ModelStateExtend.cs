using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ShopNow.Application.ExtendMethod
{
	public static class ModelStateExtend
	{
		public static void AddModelError(this ModelStateDictionary ModelState, string mgs)
		{
			ModelState.AddModelError(string.Empty, mgs);
		}
		public static void AddModelError(this ModelStateDictionary ModelState, IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(error.Description);
			}
		}
	}
}
