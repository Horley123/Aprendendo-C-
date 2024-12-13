
using ApiCatalogo.Context;

namespace ApiCatalogo.Repositories
{
    public class UniOfWork : IUnitOfWork
    {
        private IProdutoRepository? _produtoRepo;
        private ICategoriaRepository? _categoriaRepo;
        public AppDbContext _context;


        public UniOfWork(AppDbContext context)
        {
            _context = context;
        }

        // Lazy loading
        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
            }
        }

        public ICategoriaRepository GategoriaRepository => throw new NotImplementedException();

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}