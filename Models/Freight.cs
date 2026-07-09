using EmbarcaPro.API.Enum;

namespace EmbarcaPro.API.Models
{
    public class Freight
    {
        public int Id { get; private set; }

        public int DriverId { get; private set; }
        public int TruckId { get; private set; }
        public int TrailerId { get; private set; }

        public int OriginFacilityId { get; private set; }
        public int DestinationFacilityId { get; private set; }

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
        public virtual Facility OriginFacility { get; private set; } = null!;
        public virtual Facility DestinationFacility { get; private set; } = null!;

        protected Freight() { }

        public Freight(int driverId, int truckId, int trailerId, int originFacilityId, int destinationFacilityId,
                        string cargoDescription, decimal estimatedWeightKg, decimal freightValue)
        {
            DriverId = driverId;
            TruckId = truckId;
            TrailerId = trailerId;
            OriginFacilityId = originFacilityId;
            DestinationFacilityId = destinationFacilityId;

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
