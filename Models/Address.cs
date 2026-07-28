using System.ComponentModel.DataAnnotations.Schema;

namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Estrutura de endereço padronizada para comerciais e rotas de transportes.
    /// </summary>
    /// <remarks>
    /// TODO: Integrar com a API do ViaCEP para autofill e validação de dados com base no CEP.
    /// </remarks>
    [ComplexType]
    public record Address
    {

        public string Street { get; init; } // Logradouro
        public string Number { get; init; } // Numero
        public string? Complement { get; init; } // Complemento
        public string Neighborhood { get; init; } // Bairro
        public string IbgeCode { get; init; } // Tabela IBGE
        public string City { get; init; } // Cidade
        public string Uf { get; init; } // UF estado
        public string State { get; init; } // Estado
        public string ZipCode { get; init; } // CEP
        public string CountryCode { get; init; } = "1058"; // Brasil, padrão
        public string Country { get; init; } = "Brasil";
        public string? Phone { get; init; }

        protected Address()
        {

        }

        public Address(string street, string number, string? complement, string neighborhood, 
            string city, string uf, string state, string ibgeCode, string zipCode, string? phone = null)
        {
            Street = street.Trim();
            Number = number.Trim();
            Complement = string.IsNullOrWhiteSpace(complement) ? null : complement.Trim();
            Neighborhood = neighborhood.Trim();
            City = city.Trim();
            Uf = uf.Trim().ToUpperInvariant();
            State = state.Trim();
            IbgeCode = ibgeCode.Trim();
            ZipCode = zipCode.Replace("-", "").Replace(".", "").Trim();
            Phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim();
        }

    }
}
