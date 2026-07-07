using EmbarcaPro.API.Enum;

namespace EmbarcaPro.API.Models
{
    public class Trailer
    {

        public int Id { get; private set; }
        
        // Placa da Carreta
        public string LicensePlate { get; private set; }

        public int TrailerAxle { get; private set; }

        public TrailerType Type { get; private set; }

        public decimal MaxCapacityKg { get; private set; }

        public string Brand { get; private set; } // Ex: Randon, Facchini, Guerra

        // A carreta tem um volume cúbico (m³)
        public decimal CubicMetersVolume { get; private set; }

        public bool IsAvailable { get; private set; }

        public DateTime CreatedAt { get; private set; }

        protected Trailer() { }

        public Trailer(string licensePlate, int trailerAxle, TrailerType type, string brand, decimal maxCapacityKg, decimal cubicMetersVolume)
        {

            LicensePlate = licensePlate.Replace("-", "").Replace(" ", "").ToUpper().Trim();
            TrailerAxle = trailerAxle;
            Type = type;
            Brand = brand;
            MaxCapacityKg = maxCapacityKg;
            IsAvailable = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsUnavailable() => IsAvailable = false;
        public void MarkAsAvailable() => IsAvailable = true;
    }
}
