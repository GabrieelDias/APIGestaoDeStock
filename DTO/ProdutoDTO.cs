namespace FluxoDeEstoque.DTO
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public int StockMinimo { get; set; }
        public string NomeCategoria { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
    }
}
