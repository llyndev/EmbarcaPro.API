namespace EmbarcaPro.API.Models
{
    public class Truck
    {

        public int Id { get; private set; }

        // Placa veículo
        public string LicensePlate { get; private set; }

        public int TruckAxle { get; private set; }

        public string Brand { get; private set; }

        public string Model { get; private set; }

        // Capacidade de carga em KG
        public decimal MaxCapacityKg { get; private set; }

        public bool IsAvailable { get; private set; }

        public DateTime CreatedAt { get; private set; }

        protected Truck() { }

        public Truck(string licensePlate, int truckAxle, string brand, string model, decimal maxCapacityKg)
        {
            LicensePlate = licensePlate.ToUpper().Trim();
            TruckAxle = truckAxle;
            Brand = brand;
            Model = model;
            MaxCapacityKg = maxCapacityKg;
            IsAvailable = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsUnavailable()
        {
            IsAvailable = false;
        }
        
        public void MarkAsAvailable()
        {
            IsAvailable = true;
        }

    }
}
