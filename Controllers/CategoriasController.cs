using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjetoMercadinho.Context;
using ProjetoMercadinho.DTO;
using ProjetoMercadinho.Models;

namespace ProjetoMercadinho.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Salvar(CategoriaDTO categoriaTemporaria)
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = new Categoria();
                categoria.Nome = categoriaTemporaria.Nome;
                categoria.Status = true;
                _context.Categorias.Add(categoria);
                _context.SaveChanges();
                return RedirectToAction("Categorias", "Gestao");
            }
            else
            {
                return View("/Views/Gestao/NovaCategoria.cshtml");
            }
        }
        [HttpPost]
        public IActionResult Atualizar(CategoriaDTO categoriaTemporaria)
        {
            if (ModelState.IsValid)
            {
                var categoria = _context.Categorias.FirstOrDefault(cat => cat.Id == categoriaTemporaria.Id);
                categoria.Nome = categoriaTemporaria.Nome;
                _context.SaveChanges();
                return RedirectToAction("Categorias", "Gestao");
            }
            else
            {
                return View("/Views/Gestao/EditarCategoria.cshtml");
            }
        }
        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if(id > 0)
            {
                var categoria = _context.Categorias.FirstOrDefault(cat => cat.Id == id);
                categoria.Status = false;
                _context.SaveChanges();              
            }
            return RedirectToAction("Categorias", "Gestao");
        }
    }
}