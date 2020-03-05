using System.ComponentModel.DataAnnotations;

namespace Shop.Models {
    public class User {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Este campo é obrigatório")]
        [EmailAddress (ErrorMessage = "Precisa informar um email válido")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Este campo é obrigatório")]
        [MaxLength (60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength (3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string FirstName { get; set; }

        [MaxLength (60, ErrorMessage = "Este campo deve conter no máximo 60 caracteres")]
        public string LastName { get; set; }

        [Required (ErrorMessage = "Este campo é obrigatório")]
        [MinLength (6, ErrorMessage = "Este campo deve conter no mínimo 6 caracteres")]
        public string Password { get; set; }

        [Required (ErrorMessage = "Este campo é obrigatório")]
        public string Role { get; set; }

    }
}