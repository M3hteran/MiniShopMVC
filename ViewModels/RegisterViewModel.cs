using System.ComponentModel.DataAnnotations;

namespace MiniShopMVC.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Kullanıcı adı zorunludur")]
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Email zorunludur")]
		[EmailAddress(ErrorMessage = "Geçerli bir e-mail adresi giriniz")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Şifre zorunludur")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Şifreler Eşleşmiyor")]
		public string ConfirmPasswood { get; set; } = string.Empty;


	}
}
