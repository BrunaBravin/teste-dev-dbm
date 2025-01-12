using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesteDevDbm.Context;
using TesteDevDbm.Models;

namespace TesteDevDbm.Controllers
{
    public class ProtocoloController : Controller
    {
        private readonly ProtocoloContext _context;

        public ProtocoloController(ProtocoloContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var protocolos = _context.Protocolos
                .Include(p => p.Cliente)
                .Include(p => p.ProtocoloStatus).ToList();
            return View(protocolos);
        }

        [HttpGet]
        [Route("Protocolo/Criar")]
        public IActionResult Criar()
        {
            ViewBag.Clientes = new SelectList(_context.Clientes, "IdCliente", "Nome");
            ViewBag.Status = new SelectList(_context.StatusProtocolos, "IdStatus", "NomeStatus");
            return View();
        }

        [HttpPost]
        [Route("Protocolo/Criar")]
        public IActionResult Criar(Protocolo protocolo)
        {
            if (ModelState.IsValid)
            {
                _context.Protocolos.Add(protocolo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(protocolo);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var protocolo = _context.Protocolos
                .Include(p => p.Cliente)
                .Include(p => p.ProtocoloStatus)
                .FirstOrDefault(p => p.IdProtocolo == id);


            if (protocolo == null)
            {
                return NotFound();
            }

            ViewBag.Clientes =new SelectList(_context.Clientes.ToList(), "IdCliente", "Nome");
            ViewBag.Status = new SelectList(_context.StatusProtocolos.ToList(), "IdStatus", "NomeStatus");

            return View(protocolo);
        }

        [HttpPost]
        public IActionResult Editar(Protocolo protocolo)
        {
            if (ModelState.IsValid)
            {
                if (protocolo.ProtocoloStatusId == _context.StatusProtocolos
                        .FirstOrDefault(s => s.NomeStatus == "Fechado")?.IdStatus)
                {
                    protocolo.DataFechamento = DateOnly.FromDateTime(DateTime.Now);
                }

                _context.Update(protocolo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clientes =new SelectList(_context.Clientes.ToList(), "IdCliente", "Nome");
            ViewBag.Status = new SelectList(_context.StatusProtocolos.ToList(), "IdStatus", "NomeStatus");
            return View(protocolo);
        }

        public IActionResult Detalhes(int id)
        {
            var protocolo = _context.Protocolos
            .Include(p => p.Cliente)
            .Include(p => p.ProtocoloStatus)
            .FirstOrDefault(p => p.IdProtocolo == id);

            if (protocolo == null)
              return RedirectToAction(nameof(Index));

            return View(protocolo);
        }

        public IActionResult Deletar(int id)
        {
            var protocolo = _context.Protocolos
            .Include(p => p.Cliente)
            .Include(p => p.ProtocoloStatus)
            .FirstOrDefault(p => p.IdProtocolo == id);


            if (protocolo == null)
              return RedirectToAction(nameof(Index));

            return View(protocolo);
        }

        [HttpPost]
        public IActionResult Deletar(Protocolo protocolo)
        {
            var protocoloBanco = _context.Protocolos.Find(protocolo.IdProtocolo);

            _context.Protocolos.Remove(protocoloBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}