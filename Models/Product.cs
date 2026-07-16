using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MiniShopMVC.Models
{
	public class Product
	{

		public int Id { get; set; }

		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Ürün adı enfazla 100 karakter olabilir")]
		public string Name { get; set; } = string.Empty;

		[Range(1, 1000000, ErrorMessage = "Fiyat 1 ile 1000000 arasında olmalıdır")]
		[Required(ErrorMessage = "Bu alanın doldurulması zorunludur")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Açıklama zorunludur")]
		public string Description { get; set; } = string.Empty;

		[Required(ErrorMessage = "Resim URL zorunludur")]
		public string ImageUrl { get; set; } = string.Empty;

		public Category Category { get; set; } = null!;

		public List<CartItem> CartItems { get; set; } = new();

		public int Stock { get; set; }
	}
}
