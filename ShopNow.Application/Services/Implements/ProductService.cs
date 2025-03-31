using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopNow.Application.DTOs.Products;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Shared.Paginators;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;
using System.Linq.Expressions;

namespace ShopNow.Application.Services.Implements
{
    public class ProductService(IUnitOfWork<Product, Guid> unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<Guid> CreateProduct(CreateProductDTO createProductDTO)
        {
            var productDomain = mapper.Map<Product>(createProductDTO);
            var createdProduct = unitOfWork.GenericRepository.Insert(productDomain);
            await unitOfWork.CommitAsync();
            return createdProduct.Id;
        }

        public async Task<ProductDetailDTO> GetProductDetail(Guid productId)
        {
            var query = unitOfWork.GenericRepository.GetQueryAble();
            query = query.Include(_ => _.Category)
                .Include(_ => _.ProductVariants!).ThenInclude(_ => _.Assets);
            var productDetails = await query.FirstOrDefaultAsync(_ => _.Id == productId && _.Status == Shared.Enums.ProductStatus.Active);
            return mapper.Map<ProductDetailDTO>(productDetails);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await unitOfWork.GenericRepository.GetQueryAble()
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.ProductVariants)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Status = p.Status,
                    Featured = p.Featured,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    CategoryID = p.CategoryID,
                    CategoryName = p.Category!.Name,
                    Variants = p.ProductVariants.Select(v => new ProductVariantDTO
                    {
                        Id = v.Id,
                        SKU = v.SKU,
                        Discount = v.Discount,
                        Quantity = v.Quantity,
                        Sold = v.Sold,
                        Size = v.Size,
                        Color = v.Color,
                        CreatedAt = v.CreatedAt,
                        UpdatedAt = v.UpdatedAt
                    }).ToList()
                })
                .ToListAsync();

            return products;
        }


        public async Task<(List<ProductDTO> Products, int TotalCount)> GetPaginatedAsync(
       int pageIndex,
       int pageSize,
       string? search = null,
       Guid? categoryId = null,
       decimal? minPrice = null,
       decimal? maxPrice = null,
       string? sortBy = null,
       string? sortOrder = "asc")
        {
            var query = unitOfWork.GenericRepository.GetQueryAble()
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.ProductVariants)
                .AsQueryable();

            // Lọc theo danh mục (Category)
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryID == categoryId.Value);
            }

            // Lọc theo tên sản phẩm (Search)
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }

            // Lọc theo khoảng giá
            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            // Sắp xếp dữ liệu theo yêu cầu
            if (!string.IsNullOrEmpty(sortBy))
            {
                bool isAscending = sortOrder?.ToLower() == "asc";
                query = sortBy.ToLower() switch
                {
                    "name" => isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
                    "price" => isAscending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price),
                    "createdat" => isAscending ? query.OrderBy(p => p.CreatedAt) : query.OrderByDescending(p => p.CreatedAt),
                    _ => query
                };
            }

            // Lấy tổng số lượng sản phẩm
            int totalCount = await query.CountAsync();

            // Áp dụng phân trang
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Status = p.Status,
                    Featured = p.Featured,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    CategoryID = p.CategoryID,
                    CategoryName = p.Category!.Name,
                    Variants = p.ProductVariants.Select(v => new ProductVariantDTO
                    {
                        Id = v.Id,
                        SKU = v.SKU,
                        Discount = v.Discount,
                        Quantity = v.Quantity,
                        Sold = v.Sold,
                        Size = v.Size,
                        Color = v.Color,
                        CreatedAt = v.CreatedAt,
                        UpdatedAt = v.UpdatedAt
                    }).ToList()
                })
                .ToListAsync();

            return (items, totalCount);
        }


    }
}
