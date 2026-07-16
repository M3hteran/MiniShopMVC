namespace MiniShopMVC.ViewModels
{
	public class DashboradViewModel
	{
		public int ProductCount { get; set; }
		public int UserCount { get; set; }
		public int OrderCount { get; set; }
		public decimal TotalRevenue { get; set; }
		public int LowStockProductCount { get; set; }
		public int TodayOrderCount { get; set; }
	}
}
