using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopNow.Application.DTOs.Categories;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Infrastructure.Repositories;
using ShopNow.Shared.Enums;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;

namespace ShopNow.Application.Services.Implements
{
    public class CategoryService(IUnitOfWork<Category, Guid> unitOfWork, IMapper mapper) : ICategoryService
    {

        public async Task<IEnumerable<SelectCategoryDTO>> GetSelectListCategories()
        {
            var categories = await unitOfWork.GenericRepository.GetAllListAsync();
            return mapper.Map<IEnumerable<SelectCategoryDTO>>(categories);
        }

        public async Task<IEnumerable<ListCategoryDTO>> GetListCategories()
        {
            var categories = await unitOfWork.GenericRepository.GetAllListAsync();
            return mapper.Map<IEnumerable<ListCategoryDTO>>(categories);
        }
        public async Task<List<SelectCategoryDTO>> GetParentCategories()
        {
            //var categories = await unitOfWork.GenericRepository
            //    .GetAll(c => c.ParentId != null) // Chỉ lấy danh mục cha
            //    .Select(c => new SelectCategoryDTO
            //    {
            //        Id = c.Id,
            //        Name = c.Name
            //    })
            //    .ToListAsync();
            var categories = await unitOfWork.GenericRepository
                            .GetAll(c => c.ParentId != null)
                            .GroupBy(c => c.Name) // Nhóm theo Name để loại bỏ trùng lặp
                            .Select(g => new SelectCategoryDTO
                            {
                                Id = g.First().Id, // Lấy Id của phần tử đầu tiên trong nhóm
                                Name = g.Key // Dùng Key của GroupBy (tức là Name)
                            })
                            .ToListAsync();

            return categories;
        }
        public async Task<ListCategoryDTO> GetCategoryById(Guid id)
        {
            var categories = await unitOfWork.GenericRepository.GetAllListAsync();
            var category = categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            return new ListCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                ParentCategoryName = categories.FirstOrDefault(p => p.Id == category.ParentId)?.Name ?? "N/A",
                Slug = category.Slug,
                Description = category.Description,
                Image = category.Image,
                Status = category.Status,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }
        public async Task<bool> CreateCategory(CreateCategoryDTO dto)
        {
            // Nếu người dùng tạo danh mục cha mới
            if (!string.IsNullOrEmpty(dto.NewParentCategory))
            {
                var newParentCategory = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = dto.NewParentCategory,
                    ParentId = null,
                    Slug = dto.NewParentCategory.ToLower().Replace(" ", "-"),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Status = CategoryStatus.Active
                };

                unitOfWork.GenericRepository.Insert(newParentCategory);
                await unitOfWork.CommitAsync();

                // Gán danh mục cha mới tạo vào ParentId của danh mục con
                dto.ParentId = newParentCategory.Id;
            }

            // Tạo danh mục con
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                ParentId = dto.ParentId,
                Slug = dto.Slug ?? dto.Name.ToLower().Replace(" ", "-"),
                Description = dto.Description,
                Image = dto.Image,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            unitOfWork.GenericRepository.Insert(category);
            return await unitOfWork.CommitAsync() > 0;
        }

        public async Task<UpdateCategoryDTO> GetCategoryUpdateById(Guid id)
        {
            var category =  unitOfWork.GenericRepository.GetById(id);
            if (category == null) return null;

            return new UpdateCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                ParentId = category.ParentId,
                Description = category.Description,
                Image = category.Image,
                Status = category.Status
            };
        }

        public async Task<bool> UpdateCategory(UpdateCategoryDTO dto)
        {
            var category =  unitOfWork.GenericRepository.GetById(dto.Id);
            if (category == null) return false;

            category.Name = dto.Name;
            category.Slug = dto.Slug;
            category.ParentId = dto.ParentId;
            category.Description = dto.Description;
            category.Image = dto.Image;
            category.Status = dto.Status;
            category.UpdatedAt = DateTime.UtcNow;

            unitOfWork.GenericRepository.Update(category);
            return await unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            var category = unitOfWork.GenericRepository.GetById(id);
            if (category == null) return false;

            unitOfWork.GenericRepository.Delete(category);
            return await unitOfWork.CommitAsync() > 0;
        }


    }
}
