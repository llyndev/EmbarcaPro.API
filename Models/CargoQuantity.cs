namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Grupo infQ do CT-e - uma carga pode ter mais de uma unidade de medida
    /// (peso bruto e cubagem, por exemplo).
    /// </summary>
    public class CargoQuantity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid CargoId { get; init; }

        // Código da unidade: 00-M3, 01-KG, 02-TON, 03-Unidade, 04-Litros, 05-MMBTU
        public string UnitCode { get; init; }
        public string MeasureType { get; init; } // xUnid (descrição)
        public decimal Quantity { get; init; }

        protected CargoQuantity() { }

        public CargoQuantity(Guid cargoId, string unitCode, string measureType, decimal quantity)
        {
            CargoId = cargoId;
            UnitCode = unitCode.Trim();
            MeasureType = measureType.Trim();
            Quantity = quantity;
        }

    }
}
