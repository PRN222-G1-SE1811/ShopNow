using ShopNow.Application.Services.Interfaces;
using System;

namespace ShopNow.Application.Services.Implements
{
	public class SKUGenerator : ISKUGenerator
	{
		private static Random random = new Random();
		public string GenerateSKU()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, 50)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
