﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class ProductAssetAttribute
	{
		public Guid ProductId { get; set; }
		public Guid AssetId { get; set; }
		public Guid AttributeId { get; set; }
		public required string Value { get; set; }
		[ForeignKey("ProductId")]
		public ProductVariant? Product { get; set; }
		public Asset? Asset { get; set; }
		public Attribute? Attribute { get; set; }
	}
}
