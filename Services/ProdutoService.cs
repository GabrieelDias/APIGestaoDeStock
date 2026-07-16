using FluxoDeEstoque.DTO;
using FluxoDeEstoque.Interfaces;
using FluxoDeEstoque.Models;
using FluxoDeEstoque.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FluxoDeEstoque.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _prod;
        private readonly ICategoriaRepository _cat;
        private readonly IWebHostEnvironment _env;

        public ProdutoService(IProdutoRepository prod, ICategoriaRepository cat, IWebHostEnvironment env)
        {
            _prod = prod;
            _cat = cat;
            _env = env;
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
        public async Task<Produto?> CriarProduto(CriarProdutoDTO dto, string baseUrl) 
        {
            if (!await _cat.ExisteAsync(dto.CategoriaId)) return null;

            var prod = await _prod.GetSKU(dto.SKU);
            if(prod != null) 
            {
                throw new Exception("Conflito: já existe produto com este SKU");
            }

            var produto = new Produto
            {
                Nome = dto.Nome,
                SKU = dto.SKU,
                Preco = dto.Preco,
                QuantidadeEmStock = dto.QuantidadeEmStock,
                StockMinimo = dto.StockMinimo,
                DescricaoDetalhada = dto.DescricaoDetalhada,
                CategoriaId = dto.CategoriaId
            };

            if(dto.Urls != null && dto.Urls.Any()) 
            {

                string? webRootPath = _env.WebRootPath;

                if (string.IsNullOrEmpty(webRootPath)) 
                {
                    webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    Directory.CreateDirectory(webRootPath);
                }

                string fileImage = Path.Combine(webRootPath, "images");

                if (!Directory.Exists(fileImage))
                    Directory.CreateDirectory(fileImage);

                bool first = true;
                foreach(var arquivo in dto.Urls)
                {
                    if (arquivo.Length == 0) continue;
                    string UniqueName = Guid.NewGuid().ToString() + Path.GetExtension(arquivo.FileName);
                    string PathComplete = Path.Combine(fileImage, UniqueName);
                    
                    using (var stream = new FileStream(PathComplete, FileMode.Create))
                    {
                        await arquivo.CopyToAsync(stream);
                    }
                    string PublicUrl = $"{baseUrl}/images/{UniqueName}";

                    produto.Imagens.Add(new Imagem
                    {
                        Url = PublicUrl,
                        IsImage = first,
                    });
                    first = false;
                }
            }

            await _prod.AdicionarProduto(produto);
            var success = await _prod.SalvarAsync();
            return success ? produto : null;
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
            existe.DescricaoDetalhada = dto.DescricaoDetalhada;

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
                DescricaoDetalhada = p.DescricaoDetalhada,
                Quantidade = p.QuantidadeEmStock,
                StockMinimo = p.StockMinimo,
                CategoriaId = p.CategoriaId,
                NomeCategoria = p.Categoria != null ? p.Categoria.Nome : string.Empty
            
            }).ToList();
        }
    }
}
