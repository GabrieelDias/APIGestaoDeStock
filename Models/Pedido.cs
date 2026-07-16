using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxoDeEstoque.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string UsuarioId { get; set; } = string.Empty;
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;
        [Required]
        public decimal TotalBruto { get; set; }
        public decimal TotalDesconto { get; set; }
        public decimal TotalLiquido => TotalBruto - TotalDesconto;
        public int? CupomId { get; set; }
        [ForeignKey("CupomId")]
        public Cupom? Cupom { get; set; }
        public ICollection<PedidoItem> Itens { get; set; } = new List<PedidoItem>();
        [Required]
        public StatusPedido Status { get; set; } = StatusPedido.Pendente;
    }
}
