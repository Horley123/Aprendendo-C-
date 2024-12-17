

using ApiCatalogo.DTOs;
using ApiCatalogo.DTOs.Mappings.Extensions;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {

        private readonly IUnitOfWork _ofw;

        private readonly ILogger _logger;


        public CategoriasController(
            IUnitOfWork ofw,

            ILogger<CategoriasController> logger
        )
        {


            _ofw = ofw;
            _logger = logger;
        }



        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get()
        {


            var categorias = _ofw.GategoriaRepository.GetAll();

            var categoriasDto = categorias.ToCategoriaDTOList();

            return Ok(categoriasDto);
        }

        [HttpGet("{id:int}", Name = "ObeterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {


            _logger.LogInformation($" =========GET api/categorias/id = {id}  ============");
            var categorias = _ofw.GategoriaRepository.Get(c => c.CategoriaId == id);

            if (categorias is null) { _logger.LogInformation($" =========GET api/categorias/id = {id}  NOT FOUND ============"); return NotFound("Sem categorias"); }

            var categoriasDto = categorias.ToCategoriaDTO();

            return Ok(categoriasDto);
        }


        [HttpGet("pagination")]
        public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] CategoriasParemeters produtosParameters)
        {
            var categorias = _ofw.GategoriaRepository.GetCategorias(produtosParameters);


            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious

            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


            var categoriasDto = categorias.ToCategoriaDTOList();
            return Ok(categoriasDto);
        }
        [HttpPost()]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {

            if (categoriaDto is null)
            {
                return BadRequest();
            }

            var categoria = categoriaDto.ToCategoria();

            var categoriaCriada = _ofw.GategoriaRepository.Create(categoria);
            _ofw.Commit();


            var categoriaDtocreate = categoriaCriada.ToCategoriaDTO();

            return new CreatedAtRouteResult("ObeterCategoria",
                    new { id = categoriaDtocreate.CategoriaId }, categoriaDtocreate);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
        {

            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest();
            }

            var categoria = categoriaDto.ToCategoria();

            var categoriaNova = _ofw.GategoriaRepository.Update(categoria);
            _ofw.Commit();

            var categoriaDtoAtualizada = categoriaNova.ToCategoriaDTO();

            return Ok(categoriaDtoAtualizada);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {

            var categoria = _ofw.GategoriaRepository.Get(c => c.CategoriaId == id);
            var categoriaExcluida = _ofw.GategoriaRepository.Delete(categoria);
            _ofw.Commit();

            var categoriaExcluidaDTO = categoriaExcluida.ToCategoriaDTO();
            return Ok(categoriaExcluida);
        }





    }
}