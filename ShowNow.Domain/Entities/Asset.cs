using ShopNow.Shared.Enums;
using ShowNow.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class Asset : BaseEntity<Guid>, IHasCreatedAt
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
