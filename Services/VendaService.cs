using FluxoDeEstoque.DTO;
using FluxoDeEstoque.Interfaces;
using FluxoDeEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace FluxoDeEstoque.Services
{
    public class VendaService
    {
        private readonly ICarrinhoRepository _carrinhoRepo;
        private readonly IPedidoRepository _pedidoRepo;
        private readonly IProdutoRepository _produtoRepo;
        private readonly ICupomRepository _cupomRepo;

        public VendaService(ICarrinhoRepository carrinhoRepo, IPedidoRepository pedidoRepo, IProdutoRepository produtoRepo, ICupomRepository cupomRepo)
        {
            _carrinhoRepo = carrinhoRepo;
            _pedidoRepo = pedidoRepo;
            _produtoRepo = produtoRepo;
            _cupomRepo = cupomRepo;
        }

        public async Task<bool> AdicionarAoCarrinho(string usuarioId, AddItemDTO dto)
        {
            var produto = await _produtoRepo.GetProduto(dto.ProdutoId);
            if (produto == null || produto.QuantidadeEmStock < dto.Quantidade) return false;

            var carrinho = await _carrinhoRepo.GetCarrinhoPorUsuarioAsync(usuarioId);
            if (carrinho == null)
            {
                carrinho = new Carrinho { UsuarioId = usuarioId };
                await _carrinhoRepo.CriarCarrinhoAsync(carrinho);
            }

            var itemExistente = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == dto.ProdutoId);
            if (itemExistente != null)
            {
                itemExistente.Quantidade += dto.Quantidade;
            }
            else
            {
                carrinho.Itens.Add(new CarrinhoItem { ProdutoId = dto.ProdutoId, Quantidade = dto.Quantidade });
            }

            return await _carrinhoRepo.SalvarAsync();
        }

        public async Task<Pedido?> FecharPedido(FecharPedidoDTO dto)
        {
            var carrinho = await _carrinhoRepo.GetCarrinhoPorUsuarioAsync(dto.UsuarioId);
            if (carrinho == null || !carrinho.Itens.Any()) return null;

            var pedido = new Pedido { UsuarioId = dto.UsuarioId, Status = StatusPedido.Pendente };
            decimal subTotal = 0;

            foreach (var item in carrinho.Itens)
            {
                var produto = await _produtoRepo.GetProduto(item.ProdutoId);
                
                if (produto == null || produto.QuantidadeEmStock < item.Quantidade) return null;

                produto.QuantidadeEmStock -= item.Quantidade;
                await _produtoRepo.AtualizarProduto(produto);

                var pedidoItem = new PedidoItem
                {
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade,
                    PrecoUnit = produto.Preco 
                };

                subTotal += produto.Preco * item.Quantidade;
                pedido.Itens.Add(pedidoItem);
            }

            pedido.TotalBruto = subTotal;

           
            if (!string.IsNullOrWhiteSpace(dto.CupomCodigo))
            {
                var cupom = await _cupomRepo.GetPorCodigoAsync(dto.CupomCodigo);
                if (cupom != null && cupom.Date > DateTime.Now)
                {
                    pedido.CupomId = cupom.Id;
                    pedido.TotalDesconto = cupom.Porcentagem
                        ? (pedido.TotalBruto * (cupom.Desconto / 100))
                        : cupom.Desconto;
                }
            }

            await _pedidoRepo.AdicionarPedidoAsync(pedido);
            _carrinhoRepo.RemoverCarrinho(carrinho); 

            await _pedidoRepo.SalvarAsync();
            return pedido;
        }

        public async Task<bool> UpdateStatus(int pedidoId, StatusPedido status) 
        {
            var pedido = await _pedidoRepo.GetPedidoPorIdAsync(pedidoId);
            if (pedido == null) return false;

            if(status == StatusPedido.Cancelado && pedido.Status != StatusPedido.Cancelado) 
            {
                foreach(var item in pedido.Itens) 
                {
                    var produto = await _produtoRepo.GetProduto(item.Quantidade);
                    await _produtoRepo.AtualizarProduto(produto);
                }
            }
            pedido.Status = status;
            return await _pedidoRepo.SalvarAsync();
        }

        public async Task<PedidoDTO?> RespostaPedido(int id) 
        {
           var pedido = await _pedidoRepo.GetPedidoPorIdAsync(id);

            if(pedido == null) { return null; }

            return new PedidoDTO
            {
                Id = pedido.Id,
                UsuarioId = pedido.UsuarioId,
                DataPedido = pedido.DataPedido,
                TotalBruto = pedido.TotalBruto,
                TotalDesconto = pedido.TotalDesconto,
                TotalLiquido = pedido.TotalLiquido,
                StatusId = (int)pedido.Status,
                StatusNome = pedido.Status.ToString(),
                Itens = pedido.Itens.Select(i => new PedidoItemDTO
                {
                    Id = i.Id,
                    ProdutoId = i.ProdutoId,
                    NomeProduto = i.Produto?.Nome ?? "Produto não cadastrado",
                    Quantidade = i.Quantidade,
                    PrecoUnitarioNoMomento = i.PrecoUnit
                }).ToList()
            };
        }

        public async Task<CarrinhoDTO?> RespostaCart(string usuarioId)
        {
            var carrinho = await _carrinhoRepo.GetCarrinhoPorUsuarioAsync(usuarioId);
            if (carrinho == null) { return null; }

            var dto = new CarrinhoDTO
            {
                UsuarioId = carrinho.UsuarioId,
                Itens = carrinho.Itens.Select(i => new CarrinhoItemDTO
                {

                    Id = i.Id,
                    ProdutoId = i.ProdutoId,
                    NomeProduto = i.Produto.Nome,
                    PrecoUnitario = i.Produto.Preco,
                    Quantidade = i.Quantidade

                }).ToList()
            };

            return dto;
        }
    }
}
    

