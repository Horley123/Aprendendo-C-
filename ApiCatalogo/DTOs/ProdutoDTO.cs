using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.DTOs
{
    public class ProdutoDTO
    {


        public int ProdutoId { get; set; }

        [Required(ErrorMessage = " o nome � obriagtorio")]
        [StringLength(80, ErrorMessage = "Nome deve ter entre 5  20")]

        public string? Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string? Descricao { get; set; }

        [Required]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }

        //parte do relacionamento
        public int CategoriaId { get; set; }



    }
}