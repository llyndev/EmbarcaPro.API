namespace EmbarcaPro.API.Models
{
    public class Cargo
    {
        public int Id { get; init; }
        public Guid PublicId { get; init; } = Guid.NewGuid();
        public Guid CteId { get; init; }

        public decimal CargoValue { get; init; } // vCarga
        public string PredominantProduct { get; init; } // proPred
        public string? OtherCharacteristics { get; init; } // xOutCat

        private readonly List<CargoQuantity> _quantities = new();
        public virtual IReadOnlyCollection<CargoQuantity> Quantities => _quantities.AsReadOnly();

        protected Cargo() { }

        public Cargo(Guid cteId, decimal cargoValue, string predominantProduct, string? otherCharacteristics = null)
        {
            CteId = cteId;
            CargoValue = cargoValue;
            PredominantProduct = predominantProduct.Trim();
            OtherCharacteristics = otherCharacteristics?.Trim();
        }

        public void AddQuantity(string unitCode, string measureType, decimal quantity) =>
            _quantities.Add(new CargoQuantity(Id, unitCode, measureType, quantity));

    }
}
