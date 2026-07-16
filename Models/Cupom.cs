using System.ComponentModel.DataAnnotations;

namespace FluxoDeEstoque.Models
{
    public class Cupom
    {
        [Key]
        public int Id { get; set; }
       
        [Required, MaxLength(50)]
        public string Codigo { get; set; }
        [Required]
        public decimal Desconto { get; set; }
        public bool Porcentagem { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool Ativo { get; set; }
    }
}
