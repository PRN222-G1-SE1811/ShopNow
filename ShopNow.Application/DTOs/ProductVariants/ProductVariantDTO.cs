namespace ShopNow.Application.DTOs.Products
{
    public class ProductVariantDTO
    {
        public Guid Id { get; set; }
        public string SKU { get; set; } = string.Empty;
        public float? Discount { get; set; }
        public int Quantity { get; set; }
        public int Sold { get; set; }
        public string Size { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
