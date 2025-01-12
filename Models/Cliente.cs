using System.ComponentModel.DataAnnotations;

namespace TesteDevDbm.Models
{
     public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress]
        [StringLength(320)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone]
        [StringLength(25)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [StringLength(200)]
        public string Endereco { get; set; }
    }
}