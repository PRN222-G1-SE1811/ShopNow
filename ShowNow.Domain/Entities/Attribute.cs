namespace ShowNow.Domain.Entities
{
	public class Attribute : BaseEntity<Guid>
	{
		public required string Name { get; set; }
		public required string Description { get; set; }
		public List<ProductAssetAttribute>? ProductAssetAttributes { get; set; }
	}
}
