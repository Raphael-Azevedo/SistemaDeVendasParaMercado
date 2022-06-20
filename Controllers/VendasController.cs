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
    public class VendasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult GerarVenda([FromBody]VendaDTO dados){ 
            Venda venda = new Venda();
            venda.Total = (float)dados.total;
            venda.Troco =(float)dados.troco;
            venda.ValorPago = dados.troco <= 0.01f ? (float)dados.total : (float)(dados.total + dados.troco);
            venda.Data = DateTime.Now;
            _context.Vendas.Add(venda);
            _context.SaveChanges();
            List<Saida> saidas = new List<Saida>();
            foreach (var saida in dados.produtos)
            {
                Saida s = new Saida();
                s.Quantidade = saida.quantidade;
                s.ValorDaVenda = saida.subtotal;
                s.Venda = venda;
                s.Produto = _context.Produtos.First(p => p.Id == saida.produto);
                s.Data = DateTime.Now;
                saidas.Add(s);
            }
            _context.AddRange(saidas);
            _context.SaveChanges();
            return Ok(new{msg="Venda processada com sucesso!"});
        }
    }
}