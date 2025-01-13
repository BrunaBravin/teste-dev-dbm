using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteDevDbm.Context;
using TesteDevDbm.Models;

namespace TesteDevDbm.Controllers
{
    public class ProtocoloFollowController : Controller
    {
        private readonly ProtocoloContext _context;

        public ProtocoloFollowController(ProtocoloContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var protocolofollow = _context.ProtocolosFollow.ToList();
            return View(protocolofollow);
        }


    }
}