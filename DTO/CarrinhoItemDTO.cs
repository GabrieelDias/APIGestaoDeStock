namespace FluxoDeEstoque.DTO
{
    public class CarrinhoItemDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal SubTotal => PrecoUnitario * Quantidade;
    }
}
