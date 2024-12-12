
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _repository;

        public ProdutosController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var categorias = _repository.GetProdutos();

            return Ok(categorias);
        }


        [HttpGet("{id:int:min(1)}", Name = "ObeterProduto")]
        public ActionResult<Produto> Get([FromQuery] int id)
        {


            var produtos = _repository.GetProduto(id);
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

            var produtoCriado = _repository.Create(produto);


            return new CreatedAtRouteResult("ObeterProduto", new { id = produtoCriado.ProdutoId }, produtoCriado);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {

            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            var produtoAtualizado = _repository.Update(produto);

            if (produtoAtualizado)
            {

                return Ok(produtoAtualizado);
            }
            else
            {
                return StatusCode(500, $"Falha ao atualizar o produto de id = {id}");
            }

        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {



            var produtoExcluido = _repository.Delete(id);

            if (produtoExcluido)
            {

                return Ok(produtoExcluido);
            }
            else
            {

                return StatusCode(500, $"Falha ao excluir o produto de id = {id}");
            }

        }
    }
}