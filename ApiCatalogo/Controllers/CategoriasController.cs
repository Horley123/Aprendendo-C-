using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IMeuServico _meuServico;
        private readonly IConfiguration _configuration;

        public CategoriasController(
            AppDbContext context,
            IMeuServico meuServico,
            IConfiguration configuration
        )
        {
            _context = context;
            _meuServico = meuServico;
            _configuration = configuration;
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


        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {

            try
            {
                throw new Exception();
                return _context.Categorias.Include(p => p.Produtos).ToList();
            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Broxei");
            }


        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {

            var categorias = _context.Categorias.ToList();

            if (categorias is null) return NotFound("Sem categorias");

            return categorias;
        }

        [HttpGet("{id:int}", Name = "ObeterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {

            throw new Exception("testando mmeus ovos");
            var categorias = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categorias is null) return NotFound("Sem categorias");

            return Ok(categorias);
        }

        [HttpPost()]

        public ActionResult Post(Categoria categoria)
        {

            if (categoria is null)
            {
                return BadRequest();
            }
            _context.Categorias.Add(categoria);
            //persite no banco
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObeterCategoria",
                    new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {

            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            _context.SaveChanges();

            return Ok(categoria);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {

            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            // outra forna pra apagar se estivesse salvo namemoria
            //    var categira = _context.categiras.Find(id);

            if (categoria is null)
            {

                return NotFound("categira nao localizado");

            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
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