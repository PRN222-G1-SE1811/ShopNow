using Microsoft.AspNetCore.Http;
using ShopNow.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Application.DTOs.Categories
{
    public class UpdateCategoryDTO
    {
        public Guid Id { get; set; } // Bắt buộc khi cập nhật
        public string Name { get; set; }
        public string Slug { get; set; }
        public Guid? ParentId { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public IFormFile ImageFile { get; set; } // Ảnh mới

        public CategoryStatus Status { get; set; }
    }

}
