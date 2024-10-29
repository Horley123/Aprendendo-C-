
using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {

        private readonly ICategoriaRepository _repository;
        private readonly IMeuServico _meuServico;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;


        public CategoriasController(
            ICategoriaRepository repository,
            IMeuServico meuServico,
            IConfiguration configuration,
            ILogger<CategoriasController> logger
        )
        {
            _repository = repository;

            _meuServico = meuServico;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("UsandoFromServicies/{nome}")]

        public ActionResult<string> GetSaudacaoFromServicies([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }

        [HttpGet("SemUsarFromServicies/{nome}")]

        public ActionResult<string> GetSaudacaoSemFromServicies(IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }


        // [HttpGet("produtos")]
        // public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        // {


        //     _logger.LogInformation(" =========GET api/categorias/produtos  ============");
        //     return _context.Categorias.Include(p => p.Produtos).ToList();


        // }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get()
        {

            var categorias = _repository.GetCategorias();


            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObeterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {


            _logger.LogInformation($" =========GET api/categorias/id = {id}  ============");
            var categorias = _repository.GetCategoria(id);

            if (categorias is null) { _logger.LogInformation($" =========GET api/categorias/id = {id}  NOT FOUND ============"); return NotFound("Sem categorias"); }

            return Ok(categorias);
        }

        [HttpPost()]

        public ActionResult Post(Categoria categoria)
        {

            if (categoria is null)
            {
                return BadRequest();
            }
            var categoriaCriada = _repository.Create(categoria);

            return new CreatedAtRouteResult("ObeterCategoria",
                    new { id = categoriaCriada.CategoriaId }, categoriaCriada);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {

            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            var categoriaNova = _repository.Update(categoria);

            return Ok(categoriaNova);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {

            var categoriaExcluida = _repository.Delete(id);

            return Ok(categoriaExcluida);
        }




        [HttpGet("LerArquivoConfiguracao")]

        public string GetValores()
        {
            var valor1 = _configuration["chave1"];
            var valor2 = _configuration["chave2"];
            var secao1 = _configuration["secao1:chave2"];


            return $"Chave1 = {valor1} \n Chave2 = {valor2} \n secao1 = {secao1}";
        }
    }
}