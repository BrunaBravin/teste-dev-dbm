using System.Collections.Generic;
using TesteDevDbm.Models;

namespace TesteDevDbm.Models.ViewModels
{
    public class ProtocoloViewModel
{
    public List<Protocolo> Protocolos { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
}