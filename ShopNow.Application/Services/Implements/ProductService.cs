using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopNow.Application.Services.Interfaces;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;
using System.Linq.Expressions;

namespace ShopNow.Application.Services.Implements
{
	public class ProductService(IUnitOfWork<Product, Guid> unitOfWork, IMapper mapper) : IProductService
	{
		

	}
}
