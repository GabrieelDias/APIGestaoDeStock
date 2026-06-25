using FluxoDeEstoque.Data;
using FluxoDeEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeEstoque.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _appDbcontext;

        public ProdutoRepository(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }

        public async Task<IEnumerable<Produto>> GetAllProdutos()
        {
            return await _appDbcontext.Produtos.Include(p => p.Categoria).ToListAsync();
        }

        public async Task<Produto?> GetProduto(int id)
        {
            return await _appDbcontext.Produtos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> GetNomeProd(string nome)
        {

            var termo = nome.ToLower();
            return await _appDbcontext.Produtos.Include(p => p.Categoria)
                .Where(p => p.Nome.ToLower().Contains(termo)).ToListAsync();
        }

        public async Task AdicionarProduto(Produto produto)
        {
            await _appDbcontext.Produtos.AddAsync(produto);
        }

        public async Task AtualizarProduto(Produto produto)
        {
            _appDbcontext.Produtos.Update(produto);
        }

        public async Task DeletarProduto(Produto produto)
        {
            _appDbcontext.Produtos.Remove(produto);
        }
        public async Task<bool> SalvarAsync() => (await _appDbcontext.SaveChangesAsync()) > 0;
        
        public async Task<IEnumerable<Produto>> ObterStockBaixo() 
        {
            return await _appDbcontext.Produtos
                .Include(p => p.Categoria)
                .Where(p => p.QuantidadeEmStock <= p.StockMinimo)
                .ToListAsync();
        }
    }
}
