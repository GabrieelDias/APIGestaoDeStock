using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FluxoDeEstoque.Models
{
    public class Categoria
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório"), StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        
        [JsonIgnore]
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
