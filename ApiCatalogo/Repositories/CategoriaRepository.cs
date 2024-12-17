
using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public PagedList<Categoria> GetCategorias(CategoriasParemeters produtosParameters)
        {
            var categorias = GetAll().OrderBy(p => p.Nome)
            .AsQueryable();

            var categoriasOrdenados = PagedList<Categoria>.ToPageList(categorias, produtosParameters.PageNumber, produtosParameters.PageSize);
            return categoriasOrdenados;
        }
    }
}