using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesteDevDbm.Context;
using TesteDevDbm.Models;
using TesteDevDbm.Models.ViewModels;
using TesteDevDbm.Services;

namespace TesteDevDbm.Controllers
{
    public class ProtocoloController : Controller
    {
        private readonly ProtocoloContext _context;
        private readonly IProtocoloFollowService _protocoloFollowService;

        public ProtocoloController(ProtocoloContext context, IProtocoloFollowService protocoloFollowService)
        {
            _context = context;
            _protocoloFollowService = protocoloFollowService;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            var protocolos = _context.Protocolos
                .Include(p => p.Cliente)
                .Include(p => p.ProtocoloStatus);

            // Calcular o total de registros
            var totalRecords = protocolos.Count();

            // Paginando os registros
            var protocolosPaginated = protocolos
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Criar um objeto para passar para a view
            var viewModel = new ProtocoloViewModel
            {
                Protocolos = protocolosPaginated,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize)
            };

            return View(viewModel);
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

                var protocoloFollow = new ProtocoloFollow();
                protocoloFollow.ProtocoloId = protocolo.IdProtocolo;
                protocoloFollow.DataAcao = protocolo.DataAbertura;
                protocoloFollow.DescricaoAcao = "Criação do protocolo";

                _protocoloFollowService.CriaProtocoloFollow(protocoloFollow);
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

            ViewBag.Clientes = new SelectList(_context.Clientes.ToList(), "IdCliente", "Nome");
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

                var protocoloFollow = new ProtocoloFollow();
                protocoloFollow.ProtocoloId = protocolo.IdProtocolo;
                protocoloFollow.DataAcao = DateOnly.FromDateTime(DateTime.Now);
                protocoloFollow.DescricaoAcao = "Edição do protocolo";

                _protocoloFollowService.CriaProtocoloFollow(protocoloFollow);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clientes = new SelectList(_context.Clientes.ToList(), "IdCliente", "Nome");
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

            if (protocoloBanco != null)
            {
                var IdProtocolo = protocoloBanco.IdProtocolo;
                _context.Protocolos.Remove(protocoloBanco);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}