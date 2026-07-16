using System.ComponentModel.DataAnnotations;

namespace FluxoDeEstoque.DTO
{
    public class CriarProdutoDTO
    {
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public int QuantidadeEmStock { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public string DescricaoDetalhada { get; set; }
        [Required]
        public int StockMinimo { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        public List<IFormFile>? Urls { get; set; }
    }
}
