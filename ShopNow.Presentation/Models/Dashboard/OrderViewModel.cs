namespace ShopNow.Presentation.Models.Dashboard
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int OrderStatus { get; set; }
        public decimal ShipFee { get; set; }
        public decimal TotalCost { get; set; }
        public int PaymentMethod { get; set; }
        public Guid? CouponId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public string ShippingAddress { get; set; }
    }
}
