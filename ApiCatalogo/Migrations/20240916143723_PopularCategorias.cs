﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {

            mb.Sql("INSERT INTO Categorias(Nome, ImageUrl) VALUES ('Bebidas','bebidas.jpg')");
            mb.Sql("INSERT INTO Categorias(Nome, ImageUrl) VALUES ('Lanches','Lanches.jpg')");
            mb.Sql("INSERT INTO Categorias(Nome, ImageUrl) VALUES ('Sobremesas','Sobremesas.jpg')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {

            mb.Sql("DELETE FROM Categorias");


        }
    }
}
