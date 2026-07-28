namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Componente do valor do frete de um CT-e (ex.: Frete peso, Pedágio, GRIS).
    /// A soma dos componentes deve bater com o TotalServiceValue do CT-e.
    /// </summary>
    public class CteFreightComponent
    {
        public int Id { get; private set; }

        public int CteId { get; private set; }

        public string Name { get; private set; }

        public decimal Value { get; private set; }

        protected CteFreightComponent() { }

        public CteFreightComponent(string name, decimal value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do componente de frete é obrigatório.");

            if (value <= 0)
                throw new ArgumentException("O valor do componente de frete deve ser maior que zero.");

            Name = name.Trim();
            Value = value;
        }
    }
}
