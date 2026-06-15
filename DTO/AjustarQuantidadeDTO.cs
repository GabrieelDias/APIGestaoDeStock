using System.ComponentModel.DataAnnotations;

namespace FluxoDeEstoque.DTO
{
    public class AjustarQuantidadeDTO
    {
        [Required]
        public int Quantidade { get; set; }
    }
}
