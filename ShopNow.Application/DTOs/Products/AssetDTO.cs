using ShopNow.Shared.Enums;
using ShowNow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Application.DTOs.Products
{
    public class AssetDTO
    {
        [Column(TypeName = "varchar(255)")]
        public required string FileName { get; set; }
        [Column(TypeName = "varchar(255)")]
        public required string Path { get; set; }
        public required AssetType Type { get; set; }
        public double Size { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid ProductVariantId { get; set; }
        public ProductVariant? ProductVariant { get; set; }
    }
}
