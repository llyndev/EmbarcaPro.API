namespace EmbarcaPro.API.Dtos.Request
{
    public record CreateFreightRequest
    {

        public required int DriverId { get; init; }

        public required int TruckId { get; init; }

        public required int TrailerId { get; init; }

        public required int OriginFacilityId { get; init; }

        public required int DestinationFacilityId { get; init; }

        public required string CargoDescription { get; init; }

        public required decimal EstimatedWeightKg { get; init; }

        public required decimal FreightValue { get; init; }

    }
}
