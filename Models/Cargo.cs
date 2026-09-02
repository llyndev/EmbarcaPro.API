using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Models
{
    public class Cargo
    {
        public int Id { get; init; }
        public int CteId { get; private set; }

        public decimal CargoValue { get; init; } // vCarga
        public string PredominantProduct { get; init; } = null!;// proPred
        public string? OtherCharacteristics { get; init; } // xOutCat

        private readonly List<CargoQuantity> _quantities = new();
        public virtual IReadOnlyCollection<CargoQuantity> Quantities => _quantities.AsReadOnly();

        protected Cargo() { }

        public Cargo(decimal cargoValue, string predominantProduct, string? otherCharacteristics = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(predominantProduct);

            if (cargoValue < 0)
                throw new ArgumentException("O valor da carga deve ser maior que zero.", nameof(cargoValue));

            CargoValue = cargoValue;
            PredominantProduct = predominantProduct.Trim();
            OtherCharacteristics = string.IsNullOrWhiteSpace(otherCharacteristics) ? null : otherCharacteristics.Trim();
        }

        public void AddQuantity(CteUnitCode unitCode, string measureType, decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantity));

            _quantities.Add(new CargoQuantity(unitCode, measureType, quantity));
        }

    }
}
