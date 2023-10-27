using System.ComponentModel.DataAnnotations;

namespace project1.ViewModel
{
	public class Login
	{

		[Required]
		public string email { get; set; }
		[Required]
		public string password { get; set; }
	}
}
