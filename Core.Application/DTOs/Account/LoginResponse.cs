using System.ComponentModel.DataAnnotations;

namespace Core.Application.DTOs.Account
{
    public class LoginResponse
    {
        [Required(ErrorMessage = "Debe colocar el nombre de usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debe colocar la contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }

    }
}
