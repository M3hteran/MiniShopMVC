using System.ComponentModel.DataAnnotations;

namespace MiniShopMVC.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Kullanıcı Adı Zorunludur")]
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Şifre zorunludur.")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

		public string? ReturnUrl { get; set; }
	}
}
