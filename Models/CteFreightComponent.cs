namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// detalhamento dos componentes do valor do frete
    /// (frete peso, seguro, pedágio, etc.)
    /// </summary>
    public class CteFreightComponent
    {

        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid CteId { get; init; }

        public string Name { get; init; }
        public decimal Value { get; init; }

        protected CteFreightComponent() { }

        public CteFreightComponent(Guid cteId, string name, decimal value)
        {
            CteId = cteId;
            Name = name.Trim();
            Value = value;
        }
    }
}
