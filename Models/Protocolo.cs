using System.ComponentModel.DataAnnotations;

namespace TesteDevDbm.Models
{
    public class Protocolo
    {
        [Key]
        public int IdProtocolo { get; set; }

        [Required(ErrorMessage = "O nome do protocolo é obrigatório.")]
        [StringLength(50)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(250)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data de abertura é obrigatória.")]
        public DateOnly DataAbertura { get; set; }
        public DateOnly? DataFechamento { get; set; }

        [Required(ErrorMessage = "O cliente associado é obrigatório.")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "O status do protocolo é obrigatório.")]
        public int ProtocoloStatusId { get; set; }
        public StatusProtocolo ProtocoloStatus { get; set; }
    }
}








