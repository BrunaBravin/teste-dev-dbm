using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TesteDevDbm.Context;
using TesteDevDbm.Models;

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

        [HttpPost]
        public IActionResult Criar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); 
            }
            return View(cliente);
        }

        public IActionResult Editar(int id)
        {
            var cliente = _context.Clientes.Find(id);

            if (cliente == null)
              return NotFound();

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Editar(Cliente cliente) 
        {
            if (cliente != null)
            {
                Console.WriteLine(cliente);
            }
            
            var clienteBanco = _context.Clientes.Find(cliente.IdCliente);

            clienteBanco.Nome = cliente.Nome;
            clienteBanco.Email = cliente.Email;
            clienteBanco.Telefone = cliente.Telefone;
            clienteBanco.Endereco = cliente.Endereco;

            _context.Clientes.Update(clienteBanco); 
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes(int id)
        {
            var cliente = _context.Clientes.Find(id);

            if (cliente == null)
              return RedirectToAction(nameof(Index));

            return View(cliente);
        }

        public IActionResult Deletar(int id)
        {
            var cliente = _context.Clientes.Find(id);

            if (cliente == null)
              return RedirectToAction(nameof(Index));

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Deletar(Cliente cliente)
        {
            var clienteBanco = _context.Clientes.Find(cliente.IdCliente);

            _context.Clientes.Remove(clienteBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}