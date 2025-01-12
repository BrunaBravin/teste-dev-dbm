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
        public IActionResult Index(string searchString, string sortOrder, int pageNumber = 1, int pageSize = 10)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SearchString = searchString;

            ViewBag.IdSortParam = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.TitleSortParam = sortOrder == "title_desc" ? "title" : "title_desc";
            ViewBag.DateAberturaSortParam = sortOrder == "DataAbertura" ? "dataAbertura_desc" : "DataAbertura";
            ViewBag.DataFechamentoSortParam = sortOrder == "DataFechamento" ? "dataFechamento_desc" : "DataFechamento";
            ViewBag.ClienteSortParam = sortOrder == "Cliente" ? "cliente_desc" : "Cliente";
            ViewBag.StatusSortParam = sortOrder == "Status" ? "status_desc" : "Status";

            var protocolos = _context.Protocolos
                .Include(p => p.Cliente)
                .Include(p => p.ProtocoloStatus)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                protocolos = protocolos.Where(p => p.Titulo.Contains(searchString) || p.Cliente.Nome.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    protocolos = protocolos.OrderByDescending(p => p.IdProtocolo);
                    break;
                case "title":
                    protocolos = protocolos.OrderBy(p => p.Titulo);
                    break;
                case "title_desc":
                    protocolos = protocolos.OrderByDescending(p => p.Titulo);
                    break;
                case "DataAbertura":
                    protocolos = protocolos.OrderBy(p => p.DataAbertura);
                    break;
                case "dataAbertura_desc":
                    protocolos = protocolos.OrderByDescending(p => p.DataAbertura);
                    break;
                case "DataFechamento":
                    protocolos = protocolos.OrderBy(p => p.DataFechamento);
                    break;
                case "dataFechamento_desc":
                    protocolos = protocolos.OrderByDescending(p => p.DataFechamento);
                    break;
                case "Cliente":
                    protocolos = protocolos.OrderBy(p => p.Cliente.Nome);
                    break;
                case "cliente_desc":
                    protocolos = protocolos.OrderByDescending(p => p.Cliente.Nome);
                    break;
                case "Status":
                    protocolos = protocolos.OrderBy(p => p.ProtocoloStatus.NomeStatus);
                    break;
                case "status_desc":
                    protocolos = protocolos.OrderByDescending(p => p.ProtocoloStatus.NomeStatus);
                    break;
                default:
                    protocolos = protocolos.OrderBy(p => p.IdProtocolo);
                    break;
            }

            var totalRecords = protocolos.Count();

            var protocolosPaginated = protocolos
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

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