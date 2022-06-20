using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjetoMercadinho.Context;
using ProjetoMercadinho.DTO;
using ProjetoMercadinho.Models;

namespace ProjetoMercadinho.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Salvar(ProdutoDTO produtoTemporario)
        {
            if (ModelState.IsValid)
            {
                Produto produto = new Produto();
                produto.Nome = produtoTemporario.Nome;
                produto.Categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == produtoTemporario.CategoriaID);
                produto.Fornecedor = _context.Fornecedores.FirstOrDefault(fornecedor => fornecedor.Id == produtoTemporario.FornecedorID);
                produto.PrecoDeCusto = produtoTemporario.PrecoDeCusto;
                produto.PrecoDeVenda = produtoTemporario.PrecoDeVenda;
                produto.Medicao = produtoTemporario.Medicao;
                produto.Status = true;
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                return RedirectToAction("Produtos", "Gestao");
            }
            else
            {
                ViewBag.Categorias = _context.Categorias.ToList();
                ViewBag.Fornecedores = _context.Fornecedores.ToList();
                return View("/Views/Gestao/NovoProduto.cshtml");
            }
        }
        [HttpPost]
        public IActionResult Atualizar(ProdutoDTO produtoTemporario)
        {
            if (ModelState.IsValid)
            {
                var produto = _context.Produtos.FirstOrDefault(prod => prod.Id == produtoTemporario.Id);
                produto.Nome = produtoTemporario.Nome;
                produto.Categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == produtoTemporario.CategoriaID);
                produto.Fornecedor = _context.Fornecedores.FirstOrDefault(fornecedor => fornecedor.Id == produtoTemporario.FornecedorID);
                produto.PrecoDeCusto = produtoTemporario.PrecoDeCusto;
                produto.PrecoDeVenda = produtoTemporario.PrecoDeVenda;
                produto.Medicao = produtoTemporario.Medicao;
                _context.SaveChanges();
                return RedirectToAction("Produtos", "Gestao");
            }
            else
            {
                ViewBag.Categorias = _context.Categorias.ToList();
                ViewBag.Fornecedores = _context.Fornecedores.ToList();
                return View("/Views/Gestao/EditarProduto.cshtml");
            }
        }
        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var produto = _context.Produtos.FirstOrDefault(prod => prod.Id == id);
                produto.Status = false;
                _context.SaveChanges();
            }
            return RedirectToAction("Produtos", "Gestao");
        }
        [HttpPost]
        public IActionResult Produto(int id)
        {
            if (id > 0)
            {
                var produto = _context.Produtos.Where(p => p.Status == true)
                                               .Include(p => p.Categoria)
                                               .Include(p => p.Fornecedor)
                                               .FirstOrDefault(p => p.Id == id);
                if(produto != null){
                    var estoque = _context.Estoques.First(e => e.ProdutoId == produto.Id);
                    if(produto == null){
                        produto = null;
                    }
                }
                if (produto != null)
                {
                    Promocao promocao;
                    try
                    {
                         promocao = _context.Promocoes.First(p => p.Produto.Id == produto.Id && p.Status == true);
                    }
                    catch (Exception)
                    {
                        promocao = null;
                    }
                    if(promocao != null)
                    {
                        produto.PrecoDeVenda -= (produto.PrecoDeVenda*(decimal)(promocao.Porcentagem/100));
                    }
                    Response.StatusCode = 200;
                    return Json(produto);
                }
                else
                {
                    Response.StatusCode = 404;
                    return Json(null);
                }
            }
            else
            {
                Response.StatusCode = 404;
                return Json(null);
            }
        }
    }
}