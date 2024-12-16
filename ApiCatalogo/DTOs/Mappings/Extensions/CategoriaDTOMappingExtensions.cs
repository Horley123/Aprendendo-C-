
using ApiCatalogo.Models;

namespace ApiCatalogo.DTOs.Mappings.Extensions
{
    public static class CategoriaDTOMappingExtensions
    {


        public static CategoriaDTO? ToCategoriaDTO(this Categoria categoria)
        {

            if (categoria is null)
            {
                return null;
            }


            return new CategoriaDTO
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImageUrl = categoria.ImageUrl
            };
        }

        public static Categoria? ToCategoria(this CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
            {
                return null;
            }


            return new Categoria
            {
                CategoriaId = categoriaDto.CategoriaId,
                Nome = categoriaDto.Nome,
                ImageUrl = categoriaDto.ImageUrl
            };

        }

        public static IEnumerable<CategoriaDTO>? ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
        {
            if (categorias is null || !categorias.Any())
            {
                return new List<CategoriaDTO>();
            }


            return categorias.Select(categorias => new CategoriaDTO
            {
                CategoriaId = categorias.CategoriaId,
                Nome = categorias.Nome,
                ImageUrl = categorias.ImageUrl
            }).ToList();

        }

    }
}