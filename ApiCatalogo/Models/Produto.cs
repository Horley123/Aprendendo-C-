

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ApiCatalogo.Validation;

namespace ApiCatalogo.Models
{

    [Table("Produtos")]
    public class Produto : IValidatableObject
    {

        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = " o nome � obriagtorio")]
        [StringLength(80, ErrorMessage = "Nome deve ter entre 5  20")]



        [PrimeiraLetraMaiuscula]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string? Descricao { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 1000, ErrorMessage = " o repco deve estar entre {1} e {2}")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }


        //parte do relacionamento
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {


            var primeiraLetra = this.Nome[0].ToString();

            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new ValidationResult("Primeira letra deve ser maiuscula", new[] { nameof(this.Nome) });
            }

        }
    }
}