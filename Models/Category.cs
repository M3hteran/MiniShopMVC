using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MiniShopMVC.Models
{
	public class Category
	{

		public int Id { get; set; }

		[Required(ErrorMessage = "Kategori adı zorunludur")]
		[StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir")]
		public string Name { get; set; } = string.Empty;

		public List<Product> Products { get; set; } = new();
	}
}
