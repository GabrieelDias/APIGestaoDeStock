# 📦 API de Gestão de Stock

Uma Web API RESTful feita em **ASP.NET Core** com **Entity Framework Core** e banco **SQLite** para gerenciar o estoque de produtos e suas categorias.

---

## 🚀 O que o projeto faz

- **Produtos:** Cadastro completo com preço, quantidade atual e aviso de estoque mínimo.
- **Categorias:** Organização de produtos por categorias.
- **Busca:** Filtro de produtos por nome direto na API.

---

## 🛠️ Tecnologias usadas

- **C# com .NET 8.0**
- **ASP.NET Core Web API com Controllers**
- **SQLite** 
- **Swagger e EF Core Migrations**

---

## 📐 Como o banco foi montado

### Tabela: Categorias
- Id 
- Nome 
- Descricao 

### Tabela: Produtos
- Id 
- Nome
- Preco 
- QuantidadeEmStock e StockMinimo
- CategoriaId

---

## 💻 Como rodar na sua máquina

1. **Clone o projeto:**
   
       git clone [https://github.com/seu-usuario/FluxoDeEstoque.git]
       cd FluxoDeEstoque
1. **Restaure os arquivos**

       dotnet restore
3. **Crie/Atualize o banco de dados:***

       dotnet ef database update
4. **Rode a API**

       dotnet run
