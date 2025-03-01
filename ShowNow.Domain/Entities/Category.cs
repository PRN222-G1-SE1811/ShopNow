using ShopNow.Shared.Enums;
using ShowNow.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class Category : BaseEntity<Guid>, IHasUpdatedAt, IHasCreatedAt, IHasDeletedAt
	{
		[Column(TypeName = "varchar(255)")]
		public required string Name { get; set; }

		[Column(TypeName = "varchar(255)")]
		public required string Slug { get; set; }

		[ForeignKey("ParentCategory")]
		public Guid? ParentId { get; set; }

		[Column(TypeName = "text")]
		public string? Description { get; set; }
		[Column(TypeName = "varchar(255)")]
		public string? Image { get; set; }
		public CategoryStatus Status { get; set; }
		public DateTime UpdatedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		public List<Product>? Products { get; set; }
		public Category? ParentCategory { get; set; }
		public List<Category>? ChildCategories { get; set; }
	}
}
