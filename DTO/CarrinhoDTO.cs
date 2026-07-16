namespace FluxoDeEstoque.DTO
{
    public class CarrinhoDTO
    {
        public string UsuarioId { get; set; } = string.Empty;
        public List<CarrinhoItemDTO> Itens { get; set; } = new List<CarrinhoItemDTO>();
        public decimal Total => Itens.Sum(i => i.SubTotal);
    }
}
