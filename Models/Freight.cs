using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Models
{
    public class Freight
    {
        public int Id { get; private set; }

        public int CompanyId { get; private set; }
        public Company Company { get; private set; }

        public int DriverId { get; private set; }
        public int TruckId { get; private set; }
        public int TrailerId { get; private set; }

        public int OriginId { get; private set; }
        public int DestinationId { get; private set; }

        // Proprietario da Carga
        public string CargoDescription { get; private set; }
        public decimal EstimatedWeightKg { get; private set; }
        public decimal FreightValue { get; private set; }

        public FreightStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }


        // "Virtual" permite que seja modificado e sobrecrito (override) em derivadas classes.
        public virtual Driver Driver { get; private set; } = null!;
        public virtual Truck Truck { get; private set; } = null!;
        public virtual Trailer Trailer { get; private set;} = null!;
        public virtual Partner Origin { get; private set; } = null!;
        public virtual Partner Destination { get; private set; } = null!;



        protected Freight() { }

        public Freight(Company company, int driverId, int truckId, int trailerId, int originId, int destinationId,
                        string cargoDescription, decimal estimatedWeightKg, decimal freightValue)
        {

            ArgumentNullException.ThrowIfNull(company);

            Company = company;
            DriverId = driverId;
            TruckId = truckId;
            TrailerId = trailerId;
            OriginId = originId;       
            DestinationId = destinationId;

            CargoDescription = cargoDescription;
            EstimatedWeightKg = estimatedWeightKg;
            FreightValue = freightValue;

            Status = FreightStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public void StartTrip()
        {
            if (Status != FreightStatus.Pending)
                throw new InvalidOperationException("Apenas viagens pendentes podem ser iniciadas.");

            Status = FreightStatus.InTransit;
            StartedAt = DateTime.UtcNow;
        }

        public void FinishTrip()
        {
            if (Status != FreightStatus.InTransit)
                throw new InvalidOperationException("Apenas viagens em trânsito podem ser finalizados.");
            Status = FreightStatus.Delivered;
            StartedAt = DateTime.UtcNow;
        }

        public void CancelTrip()
        {
            if (Status == FreightStatus.Delivered)
                throw new InvalidOperationException("Viagens entregues não podem ser canceladas.");

            Status = FreightStatus.Canceled;
        }

    }
}
