namespace MiniShopMVC.Models
{
	public class Cart
	{
		public int Id { get; set; }
		public int AppUserId { get; set; }
		public AppUser AppUser { get; set; } = null!;
		public List<CartItem> CartItems { get; set; } = new();
		public decimal TotalPrice
		{
			get
			{
				return CartItems.Sum(x => x.Quantity * x.UnitPrice);
			}
		}
	}
}
