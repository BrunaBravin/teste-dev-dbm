using System.ComponentModel.DataAnnotations;

namespace TesteDevDbm.Models
{
     public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(320)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(25)]
        public string Telefone { get; set; }

        [Required]
        [StringLength(200)]
        public string Endereco { get; set; }
    }
}