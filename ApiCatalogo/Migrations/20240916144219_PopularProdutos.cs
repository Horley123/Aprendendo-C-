using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopularProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {

            mb.Sql("INSERT INTO Produtos(Nome,Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId)" +
            "VALUES ('Coca-Cola Diet','efrigerante de cola 350ml','5.45','cocacola.jpg','50',now(),1)");

            mb.Sql("INSERT INTO Produtos(Nome,Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId)" +
            "VALUES ('Lanche de atum','Lanchew de atum com maionese','8.50','atum.jpg','10',now(),2)");

            mb.Sql("INSERT INTO Produtos(Nome,Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId)" +
            "VALUES ('Pudim 100 g','Pudim de leite','6.65','pudim.jpg','20',now(),3)");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {

            mb.Sql("DELETE FROM Produtos");
        }
    }
}
