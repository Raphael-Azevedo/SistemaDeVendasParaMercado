using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMercadinho.Context;
using ProjetoMercadinho.DTO;

namespace ProjetoMercadinho.Controllers
{
    public class GestaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GestaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Categorias()
        {
            var categorias = _context.Categorias.Where(cat => cat.Status == true).ToList();
            return View(categorias);
        }
        public IActionResult NovaCategoria()
        {
            return View();
        }
        public IActionResult EditarCategoria(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(cat => cat.Id == id);
            CategoriaDTO categoriaView = new CategoriaDTO();
            categoriaView.Id = categoria.Id;
            categoriaView.Nome = categoria.Nome;
            return View(categoriaView);
        }
        public IActionResult Fornecedores()
        {
            var fornecedores = _context.Fornecedores.Where(forne => forne.Status == true).ToList();
            return View(fornecedores);
        }
        public IActionResult NovoFornecedor()
        {
            return View();
        }
        public IActionResult EditarFornecedor(int id)
        {
            var fornecedor = _context.Fornecedores.FirstOrDefault(forne => forne.Id == id);
            FornecedorDTO fornecedorView = new FornecedorDTO();
            fornecedorView.Id = fornecedor.Id;
            fornecedorView.Nome = fornecedor.Nome;
            fornecedorView.Email = fornecedor.Email;
            fornecedorView.Telefone = fornecedor.Telefone;
            return View(fornecedorView);
        }
        public IActionResult Produtos()
        {
            var produtos = _context.Produtos.Include(p => p.Categoria)
                                            .Include(p => p.Fornecedor)
                                            .Where(forne => forne.Status == true)
                                            .ToList();
            return View(produtos);
        }
        public IActionResult NovoProduto()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Fornecedores = _context.Fornecedores.ToList();
            return View();
        }
        public IActionResult EditarProduto(int id)
        {
            var produto = _context.Produtos.Include(p => p.Categoria)
                                           .Include(p => p.Fornecedor)
                                           .FirstOrDefault(prod => prod.Id == id);
            ProdutoDTO produtoView = new ProdutoDTO();
            produtoView.Id = produto.Id;
            produtoView.Nome = produto.Nome;
            produtoView.PrecoDeCusto = produto.PrecoDeCusto;
            produtoView.PrecoDeVenda = produto.PrecoDeVenda;
            produtoView.CategoriaID = produto.Categoria.Id;
            produtoView.FornecedorID = produto.Fornecedor.Id;
            produtoView.Medicao = produto.Medicao;
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Fornecedores = _context.Fornecedores.ToList();
            return View(produtoView);
        }
        public IActionResult Promocoes()
        {
            var promocoes = _context.Promocoes.Include(p => p.Produto)
                                         .Where(promo => promo.Status == true)
                                         .ToList();
            return View(promocoes);
        }
        public IActionResult NovaPromocao()
        {
            ViewBag.Produtos = _context.Produtos.ToList();
            return View("/Views/Gestao/NovaPromocao.cshtml");
        }
        public IActionResult EditarPromocao(int id)
        {
            var promocao = _context.Promocoes.Include(p => p.Produto)
                                             .FirstOrDefault(prod => prod.Id == id);
            PromocaoDTO PromocaoView = new PromocaoDTO();
            PromocaoView.Id = promocao.Id;
            PromocaoView.Nome = promocao.Nome;
            PromocaoView.ProdutoID = promocao.Produto.Id;
            PromocaoView.Porcentagem = promocao.Porcentagem;
            ViewBag.Produtos = _context.Produtos.ToList();
            return View(PromocaoView);
        }
        public IActionResult Estoque()
        {
            var listaDeEstoque = _context.Estoques.Include(p => p.Produto).ToList();
            return View(listaDeEstoque);
        }
        public IActionResult NovoEstoque()
        {
            ViewBag.Produtos = _context.Produtos.ToList();
            return View();
        }
        public IActionResult EditarEstoque()
        {
            return Content("");
        }
        public IActionResult Vendas()
        {
            var listDeVendas = _context.Vendas.ToList();
            return View(listDeVendas);
        }
        [HttpPost]
        public IActionResult RelatorioDeVendas()
        {
            return Ok(_context.Vendas.ToList());
        }
    }
}