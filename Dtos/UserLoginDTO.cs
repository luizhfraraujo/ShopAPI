using System.ComponentModel.DataAnnotations;

namespace Shop.Dtos {
    public class UserLoginDTO {
        [Required (ErrorMessage = "Este campo é obrigatório")]
        [EmailAddress (ErrorMessage = "Precisa informar um email válido")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Este campo é obrigatório")]
        [MinLength (6, ErrorMessage = "Este campo deve conter no mínimo 6 caracteres")]
        public string Password { get; set; }
    }
}