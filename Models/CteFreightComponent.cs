namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Componente do valor do frete de um CT-e (ex.: Frete peso, Pedágio, GRIS).
    /// A soma dos componentes deve bater com o TotalServiceValue do CT-e.
    /// </summary>
    public class CteFreightComponent
    {

        public int Id { get; private set; }

        public Guid PublicId { get; init; } = Guid.NewGuid();
        public int CteId { get; init; }

        public string Name { get; init; }
        public decimal Value { get; init; }

        protected CteFreightComponent() { }

        public CteFreightComponent(int cteId, string name, decimal value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do componente de frete é obrigatório.");

            if (value <= 0)
                throw new ArgumentException("O valor do componente de frete deve ser maior que zero.");
          
            CteId = cteId;
            Name = name.Trim();
            Value = value;
        }
    }
}
