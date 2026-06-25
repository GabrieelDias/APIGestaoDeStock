using FluxoDeEstoque.DTO;
using FluxoDeEstoque.Models;
using FluxoDeEstoque.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FluxoDeEstoque.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _prod;
        private readonly ICategoriaRepository _cat;

        public ProdutoService(IProdutoRepository prod, ICategoriaRepository cat)
        {
            _prod = prod;
            _cat = cat;
        }

        public async Task<IEnumerable<Produto>> ListarTodos() 
        {
           return await _prod.GetAllProdutos();
        }

        public async Task<Produto?> BuscarPorId(int id) 
        {
            return await _prod.GetProduto(id);
        }
        public async Task<IEnumerable<Produto>> FiltrarNome(string nome) 
        {
            if (string.IsNullOrWhiteSpace(nome)) return new List<Produto>();
            return await _prod.GetNomeProd(nome.Trim());
        }
        public async Task<Produto?> CriarProduto(CriarProdutoDTO dto) 
        {
            if (!await _cat.ExisteAsync(dto.CategoriaId)) return null;

            var produto = new Produto
            {
                Nome = dto.Nome,
                Preco = dto.Preco,
                QuantidadeEmStock = dto.QuantidadeEmStock,
                StockMinimo = dto.StockMinimo,
                CategoriaId = dto.CategoriaId
            };

            await _prod.AdicionarProduto(produto);
            await _prod.SalvarAsync();
            return produto;
        }

        public async Task<bool> Atualizar(int id, CriarProdutoDTO dto) 
        {
            var existe = await _prod.GetProduto(id);
            if (existe == null) return false;

            if (!await _cat.ExisteAsync(dto.CategoriaId)) return false;

            existe.Nome = dto.Nome;
            existe.Preco = dto.Preco;
            existe.StockMinimo = dto.StockMinimo;
            existe.QuantidadeEmStock = dto.QuantidadeEmStock;
            existe.CategoriaId = dto.CategoriaId;

            await _prod.AtualizarProduto(existe);
            return await _prod.SalvarAsync();
        }
        public async Task<bool> AjustarEstoque(int id, AjustarQuantidadeDTO dto) 
        {
            if(dto.Quantidade < 0) { return false; }
            
            var existe = await _prod.GetProduto(id);
            if(existe == null) return false;
            
            existe.QuantidadeEmStock = dto.Quantidade;
         
            await _prod.AtualizarProduto(existe);
            return await _prod.SalvarAsync();
        }

        public async Task<bool> Delete(int id) 
        {
            var existe = await _prod.GetProduto(id);
            if (existe == null) return false;

            await _prod.DeletarProduto(existe);
            return await _prod.SalvarAsync();
        }

        public async Task<IEnumerable<ProdutoDTO>> ListarStockBaixo() 
        {
            var prod = await _prod.ObterStockBaixo();

            return prod.Select(p => new ProdutoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                Quantidade = p.QuantidadeEmStock,
                StockMinimo = p.StockMinimo,
                CategoriaId = p.CategoriaId,
                NomeCategoria = p.Categoria != null ? p.Categoria.Nome : string.Empty
            
            }).ToList();
        }
    }
}
