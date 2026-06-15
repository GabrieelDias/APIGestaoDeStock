using FluxoDeEstoque.Data;
using FluxoDeEstoque.DTO;
using FluxoDeEstoque.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FluxoDeEstoque.Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ProdutosController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("/api/produtos")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAllProdutos()
        {

            return await _appDbContext.Produtos.Include(p => p.Categoria).Select(p => new ProdutoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                Quantidade = p.QuantidadeEmStock,
                StockMinimo = p.StockMinimo,
                CategoriaId = p.CategoriaId,
                NomeCategoria = p.Categoria != null ? p.Categoria.Nome : string.Empty
            }).ToListAsync();

        }

        [HttpGet("/api/produtos/{id}")]
        public async Task<ActionResult<ProdutoDTO>> GetProduto(int id)
        {
            var p = await _appDbContext.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (p == null) return NotFound("Produto não existe");

            return new ProdutoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                Quantidade = p.QuantidadeEmStock,
                StockMinimo = p.StockMinimo,
                CategoriaId = p.CategoriaId,
                NomeCategoria = p.Categoria != null ? p.Categoria.Nome : string.Empty
            };
        }

        [HttpGet("/api/produtos/pesquisa")] 
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> SearchProducts([FromQuery] string nome) 
        {

            if (string.IsNullOrWhiteSpace(nome)) return BadRequest();

            var termo = nome.ToLower();

            return await _appDbContext.Produtos.Include(p => p.Categoria)
                .Where(p => p.Nome.ToLower().Contains(termo))
                .Select(p => new ProdutoDTO
                {

                    Id = p.Id,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Quantidade = p.QuantidadeEmStock,
                    StockMinimo = p.StockMinimo,
                    CategoriaId = p.CategoriaId,
                    NomeCategoria = p.Categoria != null ? p.Categoria.Nome : string.Empty

                }).ToListAsync();
        }

        [HttpGet("/api/produtos/stock-baixo")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetStockBaixo()
        {
            return await _appDbContext.Produtos.Include(p => p.Categoria)
                .Where(p => p.QuantidadeEmStock <= p.StockMinimo)
                .Select(p => new ProdutoDTO
                {

                    Id = p.Id,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Quantidade = p.QuantidadeEmStock,
                    StockMinimo = p.StockMinimo,
                    CategoriaId = p.CategoriaId,
                    NomeCategoria = p.Categoria != null ? p.Categoria.Nome : string.Empty

                }).ToListAsync();
        }

        [HttpPost("/api/produtos")]
        public async Task<ActionResult<ProdutoDTO>> PostProduto(CriarProdutoDTO produtoDTO) 
        {
            var existe = await _appDbContext.Categorias.AnyAsync(c => c.Id == produtoDTO.CategoriaId);
            if (!existe) return BadRequest("Categoria informada não existe");

            var produto = new Produto
            {
                Nome = produtoDTO.Nome,
                Preco = produtoDTO.Preco,
                QuantidadeEmStock = produtoDTO.QuantidadeEmStock,
                StockMinimo = produtoDTO.StockMinimo,
                CategoriaId = produtoDTO.CategoriaId,
            };

            _appDbContext.Produtos.Add(produto);
            await _appDbContext.SaveChangesAsync();

            var produtocriado = await GetProduto(produto.Id);

            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produtocriado.Value);
        }

        [HttpPut("/api/produtos/{id}")]
        public async Task<IActionResult> PutProduto(int id, CriarProdutoDTO produtoDTO) 
        {

            var produto = await _appDbContext.Produtos.FindAsync(id);
            if (produto == null) return NotFound("Produto não encontrado");

            var existe = await _appDbContext.Categorias.AnyAsync(c => c.Id == produtoDTO.CategoriaId);
            if (!existe) return BadRequest("Categoria informada não existe");

            produto.Nome = produtoDTO.Nome;
            produto.Preco = produtoDTO.Preco;
            produto.QuantidadeEmStock = produtoDTO.QuantidadeEmStock;
            produto.StockMinimo = produtoDTO.StockMinimo;
            produto.CategoriaId = produtoDTO.CategoriaId;

            _appDbContext.Entry(produto).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("/api/produtos/{id}")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            var produto = await _appDbContext.Produtos.FindAsync(id);
            if (produto == null) return NotFound("Produto não existe");

            _appDbContext.Produtos.Remove(produto);
            await _appDbContext.SaveChangesAsync();

            return Ok("Produto deletado com sucesso");
        }

        [HttpPatch("api/produtos/{id}/quantidade")]
        public async Task<IActionResult> AjustarQuantidade(int id, AjustarQuantidadeDTO dto)
        {
            var produto = await _appDbContext.Produtos.FindAsync(id);
            if (produto == null) return NotFound("Produto não existe");

            int Qtd = produto.QuantidadeEmStock + dto.Quantidade;

            if (Qtd < 0)
            {
                return BadRequest(new { message = "A operação dá negativo" });
            }

            produto.QuantidadeEmStock = Qtd;
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
