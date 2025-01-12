using System.ComponentModel.DataAnnotations;

namespace TesteDevDbm.Models
{
    public class Protocolo
    {
        [Key]
        public int IdProtocolo { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [StringLength(500)]
        public string Descricao { get; set; }

        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int ProtocoloStatusId { get; set; }
        public StatusProtocolo ProtocoloStatus { get; set; }
    }
}








