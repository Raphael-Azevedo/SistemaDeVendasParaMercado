using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjetoMercadinho.Context;
using ProjetoMercadinho.DTO;
using ProjetoMercadinho.Models;

namespace ProjetoMercadinho.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FornecedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Salvar(FornecedorDTO fornecedorTemporario)
        {
            if (ModelState.IsValid)
            {
                Fornecedor fornecedor = new Fornecedor();
                fornecedor.Nome = fornecedorTemporario.Nome;
                fornecedor.Email = fornecedorTemporario.Email;
                fornecedor.Telefone = fornecedorTemporario.Telefone;
                fornecedor.Status = true;
                _context.Fornecedores.Add(fornecedor);
                _context.SaveChanges();
                return RedirectToAction("Fornecedores", "Gestao");
            }
            else
            {
                return View("/Views/Gestao/NovoFornecedor.cshtml");
            }
        }
        [HttpPost]
        public IActionResult Atualizar(FornecedorDTO fornecedorTemporario)
        {
            if (ModelState.IsValid)
            {
                var fornecedor = _context.Fornecedores.FirstOrDefault(forn => forn.Id == fornecedorTemporario.Id);
                fornecedor.Nome = fornecedorTemporario.Nome;
                fornecedor.Email = fornecedorTemporario.Email;
                fornecedor.Telefone = fornecedorTemporario.Telefone;
                _context.SaveChanges();
                return RedirectToAction("Fornecedores", "Gestao");
            }
            else
            {
                return View("/Views/Gestao/EditarFornecedor.cshtml");
            }
        }
        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var fornecedor = _context.Fornecedores.FirstOrDefault(forn => forn.Id == id);
                fornecedor.Status = false;
                _context.SaveChanges();
            }
            return RedirectToAction("Fornecedores", "Gestao");
        }
    }
}