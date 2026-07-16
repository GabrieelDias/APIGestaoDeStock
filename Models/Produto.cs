using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxoDeEstoque.Models
{
    public class Produto
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório"), StringLength(200)]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public int QuantidadeEmStock { get; set; }
        [Required(ErrorMessage = "O SKU é obrigatório"), StringLength(50)]
        public string SKU { get; set; }
        [Required]
        public string? DescricaoDetalhada { get; set; }
        [Required]
        public int StockMinimo { get; set; }
        [Required]
        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public ICollection<Imagem> Imagens { get; set; } = new List<Imagem>();
    }
}
