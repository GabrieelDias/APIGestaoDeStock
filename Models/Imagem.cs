using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FluxoDeEstoque.Models
{
    public class Imagem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Url { get; set; } = string.Empty;
        public bool IsImage { get; set; }
        [Required]
        public int IdProduct { get; set; }
        
        [ForeignKey("ProdutoId")]
        [JsonIgnore]
        public Produto? Produto { get; set; }
    }
}
