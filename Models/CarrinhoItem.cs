using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxoDeEstoque.Models
{
    public class CarrinhoItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CarrinhoId { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [ForeignKey("ProdutoId")]
        public Produto? Produto { get; set; }
    }
}
