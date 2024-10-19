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
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {

            var produtos = _context.Produtos.AsNoTracking().Take(10).ToList();

            if (produtos is null) return NotFound("Sem produtos");

            return produtos;
        }
        [HttpGet("{id:int}", Name = "ObeterProduto")]
        public ActionResult<Produto> Get(int id)
        {

            var produtos = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produtos is null) return NotFound("Sem produtos");

            return produtos;
        }

        [HttpPost()]

        public ActionResult Post(Produto produto)
        {

            if (produto is null)
            {
                return BadRequest();
            }
            _context.Produtos.Add(produto);
            //persite no banco
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObeterProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {

            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            _context.SaveChanges();

            return Ok(produto);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {

            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            // outra forna pra apagar se estivesse salvo namemoria
            //    var produto = _context.Produtos.Find(id);

            if (produto is null)
            {

                return NotFound("Produto nao localizado");

            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
    }
}