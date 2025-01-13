using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteDevDbm.Context;
using TesteDevDbm.Models;

namespace TesteDevDbm.Controllers
{
    public class StatusProtocoloController : Controller
    {
        private readonly ProtocoloContext _context;

        public StatusProtocoloController(ProtocoloContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var statusprocolo = _context.StatusProtocolos.ToList();
            return View(statusprocolo);
        }

        [Authorize]
        [HttpGet]
        [Route("Status/Criar")]
        public IActionResult Criar()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("Status/Criar")]
        public IActionResult Criar(StatusProtocolo statusprocolo)
        {
            if (ModelState.IsValid)
            {
                _context.StatusProtocolos.Add(statusprocolo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(statusprocolo);
        }

        [Authorize]
        public IActionResult Editar(int id)
        {
            var statusprocolo = _context.StatusProtocolos.Find(id);

            if (statusprocolo == null)
                return NotFound();

            return View(statusprocolo);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Editar(StatusProtocolo statusprocolo)
        {
            if (statusprocolo != null)
            {
                Console.WriteLine(statusprocolo);
            }

            var statusprocoloBanco = _context.StatusProtocolos.Find(statusprocolo.IdStatus);

            statusprocoloBanco.NomeStatus = statusprocolo.NomeStatus;

            _context.StatusProtocolos.Update(statusprocoloBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult Deletar(int id)
        {
            var statusprocolo = _context.StatusProtocolos.Find(id);

            if (statusprocolo == null)
                return RedirectToAction(nameof(Index));

            return View(statusprocolo);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Deletar(StatusProtocolo statusprocolo)
        {
            var statusprocoloBanco = _context.StatusProtocolos.Find(statusprocolo.IdStatus);

            _context.StatusProtocolos.Remove(statusprocoloBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}