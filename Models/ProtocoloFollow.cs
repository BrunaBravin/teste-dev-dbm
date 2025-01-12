using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteDevDbm.Models
{
    public class ProtocoloFollow
    {
        [Key]
        public int IdFollow { get; set; }

        [Required]
        public int ProtocoloId { get; set; }

        [Required]
        public DateOnly DataAcao { get; set; }

        [Required]
        [StringLength(500)]
        public string DescricaoAcao { get; set; }

        [ForeignKey("ProtocoloId")]
        public Protocolo Protocolo { get; set; }
    }
}