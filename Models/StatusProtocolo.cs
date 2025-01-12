using System.ComponentModel.DataAnnotations;

namespace TesteDevDbm.Models
{
    public class StatusProtocolo
    {
        [Key]
        public int IdStatus { get; set; }

        [Required(ErrorMessage = "O nome do status é obrigatório.")]
        [StringLength(50)]
        public string NomeStatus { get; set; }
    }
}