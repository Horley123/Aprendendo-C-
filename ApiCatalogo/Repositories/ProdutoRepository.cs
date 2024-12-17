using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;


namespace ApiCatalogo.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {


        public ProdutoRepository(AppDbContext context) : base(context)
        {

        }

        public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
        {
            var produtos = GetAll().OrderBy(p => p.Nome)
            .AsQueryable();

            var produtosOrdenados = PagedList<Produto>.ToPageList(produtos, produtosParameters.PageNumber, produtosParameters.PageSize);
            return produtosOrdenados;
        }
        // public IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters)
        // {
        //     return GetAll().OrderBy(p => p.Nome)
        //     .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
        //     .Take(produtosParameters.PageSize)
        //     .ToList();

        // }

        public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(c => c.CategoriaId == id);
        }
    }
}