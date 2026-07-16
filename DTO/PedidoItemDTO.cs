namespace FluxoDeEstoque.DTO
{
    public class PedidoItemDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitarioNoMomento { get; set; }
        public decimal SubTotal => PrecoUnitarioNoMomento * Quantidade;
    }
}
