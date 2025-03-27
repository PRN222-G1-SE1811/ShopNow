using ShowNow.Domain.Interfaces;

namespace ShowNow.Domain.Entities
{
	public class ProductVariant : BaseEntity<Guid>, IHasUpdatedAt, IHasCreatedAt, IHasDeletedAt
	{
		public Guid ProductId { get; set; }
		public required string SKU { get; set; }
		public float? Discount { get; set; }
		public int Quantity { get; set; }
		public int Sold { get; set; }
		public string Size { get; set; } = null!;
		public string Color { get; set; } = null!;
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public Product? Product { get; set; }
		public List<Asset> Assets { get; set; }
		public List<OrderItem>? OrderItems { get; set; }
		public List<Review>? Reviews { get; set; }
		public DateTime? DeletedAt { get; set; }
	}

}
