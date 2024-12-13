
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;


        public ProdutosController(IProdutoRepository produtoRepository)
        {

            _produtoRepository = produtoRepository;
        }

        [HttpGet("protudos/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
        {

            var produtos = _produtoRepository.GetProdutosPorCategoria(id);

            if (produtos is null)
            {
                return NotFound();
            }

            return Ok(produtos);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _produtoRepository.GetAll();

            return Ok(produtos);
        }


        [HttpGet("{id:int:min(1)}", Name = "ObeterProduto")]
        public ActionResult<Produto> Get([FromQuery] int id)
        {


            var produtos = _produtoRepository.Get(p => p.ProdutoId == id);
            if (produtos is null)
            {
                return NotFound();
            }


            return Ok(produtos);
        }

        [HttpPost()]
        public ActionResult Post(Produto produto)
        {

            if (produto is null)
            {
                return BadRequest();
            }

            var produtoCriado = _produtoRepository.Create(produto);


            return new CreatedAtRouteResult("ObeterProduto", new { id = produtoCriado.ProdutoId }, produtoCriado);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {

            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            var produtoAtualizado = _produtoRepository.Update(produto);



            return Ok(produtoAtualizado);


        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {


            var produto = _produtoRepository.Get(p => p.ProdutoId == id);

            if (produto is null)
            {

                return NotFound("Produto nao encontrado");
            }

            var produtoExcluido = _produtoRepository.Delete(produto);

            return Ok(produtoExcluido);

        }
    }
}