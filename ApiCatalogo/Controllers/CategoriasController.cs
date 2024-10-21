using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {

        private readonly AppDbContext _context;


        public CategoriasController(AppDbContext context)
        {
            _context = context;
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

    }
}