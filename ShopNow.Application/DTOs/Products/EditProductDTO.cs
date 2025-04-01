using ShopNow.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Application.DTOs.Products
{
    public class EditProductDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public ProductFeatured Featured { get; set; }
        public Guid CategoryID { get; set; }
        public string? CategoryName { get; set; }


    }
}
