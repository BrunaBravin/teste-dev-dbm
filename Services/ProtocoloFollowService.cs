
using Microsoft.EntityFrameworkCore;
using TesteDevDbm.Context;
using TesteDevDbm.Models;

namespace TesteDevDbm.Services
{
    public class ProtocoloFollowService : IProtocoloFollowService
{
    private readonly ProtocoloContext _context;

    public ProtocoloFollowService(ProtocoloContext context)
    {
        _context = context;
    }

    public ProtocoloFollow BuscaProtocoloFollowPorId(int id)
    {
        return  _context.ProtocolosFollow.Find(id);
    }

    public void CriaProtocoloFollow(ProtocoloFollow protocoloFollow)
    {
        _context.ProtocolosFollow.Add(protocoloFollow);
        _context.SaveChanges();
    }
}
}