using System.ComponentModel.DataAnnotations;

namespace FluxoDeEstoque.Models
{
    public class Carrinho
    {
        [Key]
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public ICollection<CarrinhoItem> Itens { get; set; } = new List<CarrinhoItem>();
    }
}
