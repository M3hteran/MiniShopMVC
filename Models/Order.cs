using MiniShopMVC.Models.Enums;

namespace MiniShopMVC.Models
{
	public class Order
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalPrice { get; set; }
		public int AppUserId { get; set; }

		public OrderStatus Status { get; set; } = OrderStatus.Pending;

		public AppUser AppUser { get; set; } = null!;
		public List<OrderItem> OrderItems { get; set; } = new();
	}
}
