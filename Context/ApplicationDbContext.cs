using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoMercadinho.Models;

namespace ProjetoMercadinho.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Saida> Saidas { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .Property(p => p.PrecoDeCusto)
                    .HasPrecision(10, 2);

            modelBuilder.Entity<Produto>()
                .Property(p => p.PrecoDeVenda)
                     .HasPrecision(10, 2);

            base.OnModelCreating(modelBuilder);
        }

    }
}
