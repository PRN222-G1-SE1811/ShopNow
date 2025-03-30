using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopNow.Application.DTOs.Categories;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Infrastructure.Repositories;
using ShopNow.Shared.Enums;
using ShopNow.Shared.Paginators;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;
using System.Linq.Expressions;

namespace ShopNow.Application.Services.Implements
{
    public class CategoryService(IUnitOfWork<Category, Guid> unitOfWork, IMapper mapper) : ICategoryService
    {

        public async Task<IEnumerable<SelectCategoryDTO>> GetSelectListCategories(object? condition = null)
        {
            Expression<Func<Category, bool>> predicate = x => (
                condition == null || (Guid)condition == x.Id
            );
            var categories = await unitOfWork.GenericRepository.GetAllListAsync(predicate);
            return mapper.Map<IEnumerable<SelectCategoryDTO>>(categories);
        }

        public async Task<IEnumerable<ListCategoryDTO>> GetListCategories()
        {
            var categories = await unitOfWork.GenericRepository.GetAllListAsync();
            return mapper.Map<IEnumerable<ListCategoryDTO>>(categories);
        }

        public async Task<List<SelectCategoryDTO>> GetParentCategories()
        {
            var categories = await unitOfWork.GenericRepository
                            .GetAll(c => c.ParentId != null)
                            .GroupBy(c => c.Name)
                            .Select(g => new SelectCategoryDTO
                            {
                                Id = g.First().Id,
                                Name = g.Key
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
                UpdatedAt = (DateTime)category.UpdatedAt
            };
        }
       
        public async Task<string> UploadCategoryImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null; 
            }

            string folderPath = "wwwroot/images/categories"; 
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath); 
            }

            // Tạo tên file duy nhất
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(fullPath, fileName);

            // Lưu file vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("/images/categories", fileName); 
        }


        public async Task<bool> CreateCategory(CreateCategoryDTO dto)
        {
            string imagePath = null;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                imagePath = await UploadCategoryImage(dto.ImageFile);
            }

            if (!string.IsNullOrEmpty(dto.NewParentCategory))
            {
                var newParentCategory = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = dto.NewParentCategory,
                    ParentId = null,
                    Slug = dto.NewParentCategory.ToLower().Replace(" ", "-"),
                    Image = imagePath, 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Status = CategoryStatus.Active
                };

                unitOfWork.GenericRepository.Insert(newParentCategory);
                await unitOfWork.CommitAsync();

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
                Image = imagePath,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            unitOfWork.GenericRepository.Insert(category);
            return await unitOfWork.CommitAsync() > 0;
        }


        public async Task<UpdateCategoryDTO> GetCategoryUpdateById(Guid id)
        {
            var category = unitOfWork.GenericRepository.GetById(id);
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

            string imagePath = category.Image; 

            // Kiểm tra nếu có ảnh mới thì upload
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                imagePath = await UploadCategoryImage(dto.ImageFile);
            }

            // Cập nhật thông tin danh mục
            category.Name = dto.Name;
            category.Slug = dto.Slug;
            category.ParentId = dto.ParentId;
            category.Description = dto.Description;
            category.Image = imagePath; 
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

        public async Task<PageResult<ListCategoryDTO>> GetPaginatedCategories(string? search, string? sortByDate, int page, int pageSize)
        {
            var paginatedResult = await unitOfWork.GenericRepository.GetPaginatedAsync(
                page,
                pageSize,
                query =>
                {
                    if (!string.IsNullOrEmpty(search))
                    {
                        query = query.Where(c => c.Name.Contains(search));
                    }
                    return query;
                },
                query =>
                {
                    return sortByDate switch
                    {
                        "CreatedAt" => query.OrderByDescending(c => c.CreatedAt),
                        "UpdatedAt" => query.OrderByDescending(c => c.UpdatedAt),
                        _ => query.OrderBy(c => c.Name)
                    };
                }
            );

            var mappedItems = paginatedResult.Items.Select(c => new ListCategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                ParentId = c.ParentId,
                ParentCategoryName = paginatedResult.Items.FirstOrDefault(p => p.Id == c.ParentId)?.Name ?? "N/A",
                Slug = c.Slug,
                Description = c.Description,
                Image = c.Image,
                Status = c.Status,
                CreatedAt = c.CreatedAt,
                UpdatedAt = (DateTime)c.UpdatedAt
            }).ToList();

            return new PageResult<ListCategoryDTO>(mappedItems, paginatedResult.TotalItems, page, pageSize);
        }       
    }
}
