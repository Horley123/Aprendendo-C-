

namespace ApiCatalogo.Repositories
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        ICategoriaRepository GategoriaRepository { get; }

        void Commit();
        // void RoolBack();

    }
}