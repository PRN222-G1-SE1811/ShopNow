using ShopNow.Application.DTOs.Prodducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IProductVariantService
	{
		Task CreateVariantProduct(ProductVariantDTO productVariantDTO);
		Task AddRangeVariantProduct(List<ProductVariantDTO> productVariantDTOs);
	}
}
