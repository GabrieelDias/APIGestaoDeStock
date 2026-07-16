using System.ComponentModel.DataAnnotations;

namespace FluxoDeEstoque.DTO
{
    public class FecharPedidoDTO
    {
        [Required]
        public string UsuarioId { get; set; } = string.Empty;
        public string? CupomCodigo { get; set; }
    }
}
