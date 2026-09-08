using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Models
{
    public class Trailer
    {

        public int Id { get; private set; }

        public int CompanyId { get; private set; }
        public Company Company { get; private set; } = null!;
        
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

        public Trailer(Company company, string licensePlate, int trailerAxle, TrailerType type, string brand, decimal maxCapacityKg, decimal cubicMetersVolume)
        {

            ArgumentNullException.ThrowIfNull(company);

            Company = company;
            LicensePlate = licensePlate.Replace("-", "").Replace(" ", "").ToUpper().Trim();
            TrailerAxle = trailerAxle;
            Type = type;
            Brand = brand;
            MaxCapacityKg = maxCapacityKg;
            CubicMetersVolume = cubicMetersVolume;
            IsAvailable = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsUnavailable() => IsAvailable = false;
        public void MarkAsAvailable() => IsAvailable = true;
    }
}
