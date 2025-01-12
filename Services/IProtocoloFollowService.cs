using TesteDevDbm.Models;

namespace TesteDevDbm.Services
{
    public interface IProtocoloFollowService
    {
        ProtocoloFollow BuscaProtocoloFollowPorId(int id);
        void CriaProtocoloFollow(ProtocoloFollow protocoloFollow);
    }
}