using ShopNow.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Application.DTOs.Categories
{
    public class ListCategoryDTO
    {
        [Required]
        public Guid Id { get; set; } // ID của danh mục
        [Required]
        public string Name { get; set; } = null!; // Tên danh mục
        [Required]
        public string Slug { get; set; } = null!; // Đường dẫn Slug
        [Required]
        public Guid? ParentId { get; set; } // ID danh mục cha (nullable nếu không có danh mục cha)

        [Required]
        public string? ParentCategoryName { get; set; } // Tên danh mục cha

        [Required]
        public string? Description { get; set; } // Mô tả danh mục
        [Required]
        public string? Image { get; set; } // Link ảnh danh mục
        [Required]
        public CategoryStatus Status { get; set; } // Trạng thái danh mục (Active/Inactive)
        [Required]
        public DateTime CreatedAt { get; set; } // Ngày tạo danh mục
        [Required]
        public DateTime UpdatedAt { get; set; } // Ngày cập nhật danh mục
    }
}
