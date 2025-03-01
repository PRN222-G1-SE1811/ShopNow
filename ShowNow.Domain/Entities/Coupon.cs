using ShopNow.Shared.Enums;
using ShowNow.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class Coupon : BaseEntity<Guid>, IHasCreatedAt, IHasUpdatedAt, IHasDeletedAt
	{
		[Column(TypeName = "varchar(255)")]
		public required string CouponCode { get; set; }

		public required CouponType CouponType { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public required decimal CouponValue { get; set; }

		public required DateTime StartDate { get; set; }
		public required DateTime EndDate { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public required decimal CouponMinSpend { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public required decimal CouponMaxSpend { get; set; }
		public required CouponStatus CouponStatus { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
	}
}
