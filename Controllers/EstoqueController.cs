using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjetoMercadinho.Context;
using ProjetoMercadinho.Models;

namespace ProjetoMercadinho.Controllers
{
    public class EstoqueController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EstoqueController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Salvar(Estoque estoqueTemp)
        {
            _context.Estoques.Add(estoqueTemp);
            _context.SaveChanges();
            return RedirectToAction("Estoque","Gestao");
        }
    }
}