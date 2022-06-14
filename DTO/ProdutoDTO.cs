using System.ComponentModel.DataAnnotations;

namespace ProjetoMercadinho.DTO
{
    public class ProdutoDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome do produto é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome do produto é muito grande, tente um nome menor!")]
        [MinLength(2, ErrorMessage = "Nome do produto muito pequeno, tente um nome com mais de 2 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Categoria do produto é obrigatório")]
        public int CategoriaID { get; set; }
        [Required(ErrorMessage = "Fornecedor do produto é obrigatório")]
        public int FornecedorID { get; set; }
        [Required(ErrorMessage = "Preço de custo do produto é obrigatório")]
        public float PrecoDeCusto { get; set; }
        [Required(ErrorMessage = "Preço de venda do produto é obrigatório")]
        public float PrecoDeVenda { get; set; }
        [Required(ErrorMessage = "Medição do produto é obrigatória")]
        [Range(0,2, ErrorMessage ="Medição inválida")]
        public int Medicao { get; set; }
    }
}