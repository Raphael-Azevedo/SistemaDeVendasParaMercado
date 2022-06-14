using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProjetoMercadinho.Models;

namespace ProjetoMercadinho.DTO
{
    public class PromocaoDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome da promoção é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome da promoção é muito grande, tente um nome menor!")]
        [MinLength(2, ErrorMessage = "Nome da promoção é muito pequeno, tente um nome com mais de 2 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Produto da promoção é obrigatório")]
        public int ProdutoID { get; set; }
        [Required(ErrorMessage = "Valor da promoção é obrigatório")]
        [Range(0,100)]
        public float Porcentagem { get; set; }
    }
}