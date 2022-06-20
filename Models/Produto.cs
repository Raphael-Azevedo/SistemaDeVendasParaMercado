using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoMercadinho.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Categoria Categoria { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public decimal PrecoDeCusto { get; set; }      
        public decimal PrecoDeVenda { get; set; }
        public int Medicao { get; set; }
        public bool Status { get; set; }
    }
}