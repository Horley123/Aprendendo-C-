
using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;


        public ProdutosController(IUnitOfWork uof,
            IMapper mapper)
        {

            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet("protudos/{id}")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorCategoria(int id)
        {

            var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id);

            if (produtos is null)
            {
                return NotFound();
            }
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            return Ok(produtosDto);
        }
        [HttpGet("pagination")]
        public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = _uof.ProdutoRepository.GetProdutos(produtosParameters);


            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious

            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            return Ok(produtosDto);
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            var produtos = _uof.ProdutoRepository.GetAll();
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            return Ok(produtosDto);
        }


        [HttpGet("{id:int:min(1)}", Name = "ObeterProduto")]
        public ActionResult<ProdutoDTO> Get([FromQuery] int id)
        {


            var produtos = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
            if (produtos is null)
            {
                return NotFound();
            }

            var produtosDto = _mapper.Map<ProdutoDTO>(produtos);
            return Ok(produtosDto);
        }

        [HttpPost()]
        public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
        {

            if (produtoDto is null)
            {
                return BadRequest();
            }
            var produto = _mapper.Map<Produto>(produtoDto);

            var produtoCriado = _uof.ProdutoRepository.Create(produto);
            _uof.Commit();


            var novoProdutoDto = _mapper.Map<ProdutoDTO>(produtoCriado);

            return new CreatedAtRouteResult("ObeterProduto", new { id = novoProdutoDto.ProdutoId }, novoProdutoDto);
        }

        [HttpPatch("{id}/UpdatePartial")]
        public ActionResult<ProdutoDTORequest> Patch(int id, JsonPatchDocument<ProdutoDTOResponse> patchProdutoDTO)
        {
            if (patchProdutoDTO is null || id <= 0)
            {
                return BadRequest();
            }

            var produto = _uof.ProdutoRepository.Get(c => c.ProdutoId == id);

            if (produto is null)
            {
                return NotFound();
            }

            var produtoDtoUpdate = _mapper.Map<ProdutoDTOResponse>(produto);

            patchProdutoDTO.ApplyTo(produtoDtoUpdate, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(produtoDtoUpdate))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(produtoDtoUpdate, produto);
            _uof.ProdutoRepository.Update(produto);

            return Ok(_mapper.Map<ProdutoDTOResponse>(produto));

        }
        [HttpPut("{id:int}")]
        public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
        {

            if (id != produtoDto.ProdutoId)
            {
                return BadRequest();
            }
            var produto = _mapper.Map<Produto>(produtoDto);

            var produtoAtualizado = _uof.ProdutoRepository.Update(produto);

            _uof.Commit();

            var produtosDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);
            return Ok(produtosDto);


        }
        [HttpDelete("{id:int}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {


            var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto is null)
            {

                return NotFound("Produto nao encontrado");
            }

            var produtoExcluido = _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();


            var produtosDto = _mapper.Map<Produto>(produtoExcluido);
            return Ok(produtosDto);

        }
    }
}