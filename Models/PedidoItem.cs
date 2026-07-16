using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxoDeEstoque.Models
{
    public class PedidoItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PedidoId { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public decimal PrecoUnit { get; set; }
        [ForeignKey("ProdutoId")]
        public Produto? Produto { get; set; }
    }
}
