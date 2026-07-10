namespace EmbarcaPro.API.Dtos.Response
{
    public record FreightResponse(
        int Id,
        string DriverName,
        string TruckPlate,
        string TrailerPlate,
        string OriginCity,
        string DestinationCity,
        string CargoDescription,
        string Status,
        decimal FreightValue,
        DateTime CreatedAt);
}
