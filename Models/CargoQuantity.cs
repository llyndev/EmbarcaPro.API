namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Grupo infQ do CT-e - uma carga pode ter mais de uma unidade de medida
    /// (peso bruto e cubagem, por exemplo).
    /// </summary>
    public class CargoQuantity
    {
        public int Id { get; init; }
        public Guid PublicId { get; init; } = Guid.NewGuid();
        public int CargoId { get; private set; }

        // Código da unidade: 00-M3, 01-KG, 02-TON, 03-Unidade, 04-Litros, 05-MMBTU
        public string UnitCode { get; init; }
        public string MeasureType { get; init; } // xUnid (descrição)
        public decimal Quantity { get; init; }

        protected CargoQuantity() { }

        public CargoQuantity(string unitCode, string measureType, decimal quantity)
        {
            UnitCode = unitCode.Trim();
            MeasureType = measureType.Trim();
            Quantity = quantity;
        }

    }
}
