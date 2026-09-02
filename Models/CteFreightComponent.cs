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

        public string Name { get; init; }
        public decimal Value { get; init; }

        protected CteFreightComponent() { }

        public CteFreightComponent(string name, decimal value)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            Name = name.Trim();
            Value = value;
        }
    }
}
