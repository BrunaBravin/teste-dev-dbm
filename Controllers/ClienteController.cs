using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TesteDevDbm.Context;

namespace TesteDevDbm.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ProtocoloContext _context;

        public ClienteController(ProtocoloContext context)
        {
            _context = context;
        }
       public IActionResult Index()
       {
            var clientes = _context.Clientes.ToList();
            return View(clientes);
       }
       public IActionResult Criar()
        {
            return View();
        }
    }
}