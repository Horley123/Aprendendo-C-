using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.DTOs
{
    public class ProdutoDTORequest : IValidatableObject
    {
        [Range(1, 9999, ErrorMessage = "valor de estoque deve estar entre 1 e 9999")]
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataCadastro.Date <= DateTime.Now.Date)
            {
                yield return new ValidationResult("a data deve ser maior que a data atual", new[] { nameof(this.DataCadastro) });
            }
        }
    }
}