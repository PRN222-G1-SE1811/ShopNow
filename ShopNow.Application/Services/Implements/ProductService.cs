using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopNow.Application.DTOs.Categories;
using ShopNow.Application.DTOs.Products;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Infrastructure.Repositories;
using ShopNow.Presentation.Models.ProductViewModel;
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
            var productDetails = await query.FirstOrDefaultAsync(_ => _.Id == productId);
            if (productDetails == null)
            {
                return null;  // hoặc bạn có thể ném ra một ngoại lệ tùy vào cách xử lý lỗi của bạn.
            }
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

        public async Task<bool> EditProduct(EditProductDTO editProductDTO)
        {
            // Lấy sản phẩm hiện tại từ cơ sở dữ liệu
            var product = await unitOfWork.GenericRepository.GetQueryAble()
                .FirstOrDefaultAsync(p => p.Id == editProductDTO.Id);

            // Kiểm tra xem sản phẩm có tồn tại hay không
            if (product == null)
            {
                return false; // Nếu không tìm thấy sản phẩm, trả về false
            }

            // Cập nhật thông tin của sản phẩm
            product.Name = editProductDTO.Name;
            product.Description = editProductDTO.Description;
            product.Price = editProductDTO.Price;
            product.Status = editProductDTO.Status;
            product.Featured = editProductDTO.Featured;
            product.CategoryID = editProductDTO.CategoryID;
            product.UpdatedAt = DateTime.UtcNow; // Cập nhật thời gian sửa đổi

            // Lưu thông tin sản phẩm đã chỉnh sửa vào cơ sở dữ liệu
            unitOfWork.GenericRepository.Update(product);
            await unitOfWork.CommitAsync();

            // Trả về true nếu cập nhật thành công
            return true;
        }

        public async Task<EditProductDTO> GetProductEditById(Guid id)
        {
            // Lấy sản phẩm theo ID từ cơ sở dữ liệu
            var product =  unitOfWork.GenericRepository.GetById(id); // Giả sử GetByIdAsync là phương thức lấy dữ liệu bất đồng bộ
            if (product == null) return null; // Trả về null nếu sản phẩm không tồn tại

            // Trả về EditProductDTO với dữ liệu ánh xạ từ sản phẩm
            return new EditProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Status = product.Status,
                Featured = product.Featured,
                CategoryID = product.CategoryID // Thêm CategoryID nếu cần thiết
            };
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            // Lấy sản phẩm theo id
            var pro = await unitOfWork.GenericRepository.FirstOrDefaultAsync(id);
            if (pro == null) return false;

            // Lấy danh sách ProductVariant từ Product
            var productVariants = await unitOfWork.GenericRepository.GetAll()
                .Where(p => p.Id == id)
                .SelectMany(p => ((Product)p).ProductVariants!)
                .ToListAsync();

            if (productVariants.Any())
            {
                throw new InvalidOperationException("Sản phẩm này có các biến thể, không thể xóa.");
            }

            // Nếu không có ProductVariant, tiếp tục xóa sản phẩm
            unitOfWork.GenericRepository.Delete(pro);
            return await unitOfWork.CommitAsync() > 0;
        }









    }
}
