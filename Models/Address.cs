namespace EmbarcaPro.API.Models
{
    public class Address
    {
        // TODO: futuramente solciitar somente CEP e Numero.

        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complement { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Cep { get; private set; }

    }
}
