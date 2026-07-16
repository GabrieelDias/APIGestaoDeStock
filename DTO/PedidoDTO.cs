namespace FluxoDeEstoque.DTO
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public DateTime DataPedido { get; set; }
        public decimal TotalBruto { get; set; }
        public decimal TotalDesconto { get; set; }
        public decimal TotalLiquido { get; set; }
        
        public int StatusId { get; set; }
        public string StatusNome { get; set; } = string.Empty;
        public List<PedidoItemDTO> Itens { get; set; } = new List<PedidoItemDTO>();
    }
}
