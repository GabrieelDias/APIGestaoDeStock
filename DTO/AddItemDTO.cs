using System.ComponentModel.DataAnnotations;

namespace FluxoDeEstoque.DTO
{
    public class AddItemDTO
    {
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        public int Quantidade { get; set; }
    }
}
