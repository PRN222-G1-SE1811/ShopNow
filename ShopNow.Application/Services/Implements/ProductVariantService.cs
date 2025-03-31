using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopNow.Application.DTOs.Products;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Infrastructure.Repositories;
using ShopNow.Presentation.Models.ProductViewModel;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;

namespace ShopNow.Application.Services.Implements
{
	public class ProductVariantService(IUnitOfWork<ProductVariant, Guid> unitOfWork, IAssetService assetService, ISKUGenerator SKUGenerator, IMapper mapper) : IProductVariantService
	{
		public async Task<bool> CreateProdductVariants(Guid id, List<CreateProductVariantDTO> createProductVariantDTOs)
		{
			var productVariantDomain = mapper.Map<List<ProductVariant>>(createProductVariantDTOs);
			foreach (var p in productVariantDomain)
			{
				p.ProductId = id;
				p.Sold = 0;
				p.SKU = SKUGenerator.GenerateSKU();
				unitOfWork.GenericRepository.Insert(p);
			}
			
			var rowChanged = await unitOfWork.CommitAsync();
			var i = 0;
			foreach (var productVariant in createProductVariantDTOs)
			{
				await assetService.CreateAssetsRange(productVariantDomain[i++].Id, productVariant.Assets);
			}
			i = 0;
			return rowChanged > 0;
		}


        public async Task<bool> EditProductVariantAsync(EditProductVariantDTO dto)
        {
            // Lấy đối tượng biến thể sản phẩm từ repository sử dụng GetQueryable()
            var variant = await unitOfWork.GenericRepository.GetQueryAble()
                .FirstOrDefaultAsync(a => a.Id == dto.Id); // Sử dụng Id của variant chứ không phải ProductId

            if (variant == null)
            {
                return false;
            }

            // Cập nhật các thuộc tính của biến thể sản phẩm
            variant.Discount = (float?)dto.Discount;
            variant.Quantity = dto.Quantity;
            variant.Color = dto.Color;
            variant.Size = dto.Size;

            // Kiểm tra xem có hình ảnh nào không, nếu có thì thêm vào
            if (dto.Assets != null && dto.Assets.Any())
            {
                await assetService.CreateAssetsRange(variant.Id, dto.Assets);
            }

            unitOfWork.GenericRepository.Update(variant);

            // Commit changes to the database
            await unitOfWork.CommitAsync();

            // Kiểm tra xem các thay đổi có thực sự được áp dụng
            return 
                   variant.Quantity == dto.Quantity &&
                   variant.Color == dto.Color &&
                   variant.Size == dto.Size;
        }


        // Triển khai phương thức DeleteProductVariantAsync
        public async Task<bool> DeleteProductVariantAsync(Guid id)
        {
            // Tìm biến thể sản phẩm bằng Id
            var variant = await unitOfWork.GenericRepository.GetQueryAble()
                .FirstOrDefaultAsync(v => v.Id == id);

            if (variant == null)
            {
                // Nếu không tìm thấy biến thể sản phẩm, trả về false
                return false;
            }

            // Xóa biến thể sản phẩm
            unitOfWork.GenericRepository.Delete(variant);

            // Lưu thay đổi vào cơ sở dữ liệu
            var rowsAffected = await unitOfWork.CommitAsync();

            // Trả về true nếu có dòng bị thay đổi (tức là xóa thành công)
            return rowsAffected > 0;
        }


        public async Task<ProductVariant> GetProductVariantByIdAsync(Guid id)
        {
            // Lấy đối tượng biến thể sản phẩm từ repository
            var variant = await unitOfWork.GenericRepository.GetQueryAble()
                .Where(v => v.Id == id)
                .FirstOrDefaultAsync();

            return variant;
        }
        public async Task<List<AssetDTO>> GetAssetsByProductVariantIdAsync(Guid id)
        {
            // Truy vấn danh sách assets từ ProductVariant có id tương ứng
            var assets = await unitOfWork.GenericRepository.GetQueryAble()
                .Where(pv => pv.Id == id)  // Lọc theo ProductVariantId
                .SelectMany(pv => pv.Assets)  // Lấy tất cả assets của ProductVariant
                .Select(a => new AssetDTO
                {
                    FileName = a.FileName,
                    Path = a.Path,
                    Type = a.Type,
                    Size = a.Size,
                    CreatedAt = a.CreatedAt,
                    ProductVariantId = a.ProductVariantId
                })
                .ToListAsync();

            return assets;
        }

        public async Task DecreaseQuantity(Guid id, int quantity)
		{
			var product = unitOfWork.GenericRepository.GetById(id);
			product!.Quantity -= quantity;
			unitOfWork.GenericRepository.Update(product);
			await unitOfWork.CommitAsync();
		}
	}
}
