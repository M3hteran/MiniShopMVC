namespace MiniShopMVC.Models
{
	public class AppUser
	{
		public int Id { get; set; }
		public string UserName { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string PasswordHash { get; set; } = string.Empty;

		public string Role { get; set; } = "User";

		public Cart? Cart { get; set; }

		public List<Order> Orders { get; set; } = new();
	}
}
