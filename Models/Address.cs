using System.ComponentModel.DataAnnotations.Schema;

namespace EmbarcaPro.API.Models
{
    [ComplexType]
    public record Address
    {
        // TODO: futuramente solciitar somente CEP e Numero.

        public string Street { get; init; } // Logradouro
        public string Number { get; init; } // Numero
        public string Complement { get; init; } // Complemento
        public string Neighborhood { get; init; } // Bairro
        public string City { get; init; } // Cidade
        public string State { get; init; } // Estado
        public string ZipCode { get; init; } // CEP

        protected Address()
        {

        }

        public Address (string street, string number, string complement, string neighborhood, string city, string state, string zipCode)
        {
            Street = street.Trim();
            Number = number.Trim();
            Complement = complement.Trim();
            Neighborhood = neighborhood.Trim();
            City = city.Trim();
            State = state.Trim();
            ZipCode = zipCode.Trim();
        }

    }
}
