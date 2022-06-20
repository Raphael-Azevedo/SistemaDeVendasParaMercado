using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjetoMercadinho.Context;
using ProjetoMercadinho.DTO;
using ProjetoMercadinho.Models;

namespace ProjetoMercadinho.Controllers
{
    public class PromocoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PromocoesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Salvar(PromocaoDTO promocaoTemporaria)
        {
            if (ModelState.IsValid)
            {
                Promocao promocao = new Promocao();
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Produto = _context.Produtos.FirstOrDefault(p => p.Id == promocaoTemporaria.ProdutoID);
                promocao.Porcentagem = promocaoTemporaria.Porcentagem;
                promocao.Status = true;
                _context.Promocoes.Add(promocao);
                _context.SaveChanges();
                return RedirectToAction("Promocoes", "Gestao");
            }
            else
            {
                ViewBag.Produtos = _context.Produtos.ToList();
                return View("/Views/Gestao/NovaPromocao.cshtml");
            }
        }
        [HttpPost]
        public IActionResult Atualizar(PromocaoDTO promocaoTemporaria)
        {
            if (ModelState.IsValid)
            {
                var promocao = _context.Promocoes.FirstOrDefault(promo => promo.Id == promocaoTemporaria.Id);
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Produto = _context.Produtos.FirstOrDefault(p => p.Id == promocaoTemporaria.ProdutoID);
                promocao.Porcentagem = promocaoTemporaria.Porcentagem;
                _context.SaveChanges();
                return RedirectToAction("Promocoes", "Gestao");
            }
            else
            {
                ViewBag.Produtos = _context.Produtos.ToList();
                return View("/Views/Gestao/EditarPromocao.cshtml");
            }
        }
        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var promocao = _context.Promocoes.FirstOrDefault(prod => prod.Id == id);
                promocao.Status = false;
                _context.SaveChanges();
            }
            return RedirectToAction("Promocoes", "Gestao");
        }
    }

}