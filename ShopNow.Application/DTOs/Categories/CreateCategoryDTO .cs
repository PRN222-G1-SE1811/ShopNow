using ShopNow.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Application.DTOs.Categories
{
    public class CreateCategoryDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }  // Cho phép null nếu không có danh mục cha
        public string? Description { get; set; }
        public string? Image { get; set; }
        public CategoryStatus Status { get; set; } = CategoryStatus.Active; // Enum
        public string? NewParentCategory { get; set; } // Nếu tạo danh mục cha mới

    }

}
