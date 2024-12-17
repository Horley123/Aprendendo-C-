
using ApiCatalogo.Models;
using AutoMapper;

namespace ApiCatalogo.DTOs.Mappings
{
    public class ProdutoDTOMappingProfile : Profile
    {

        public ProdutoDTOMappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTORequest>().ReverseMap();
            CreateMap<Produto, ProdutoDTOResponse>().ReverseMap();

        }
    }
}