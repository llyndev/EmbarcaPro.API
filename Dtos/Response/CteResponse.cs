namespace EmbarcaPro.API.Dtos.Response
{
    /// <summary>
    /// Versão completa, usada em GET /api/ctes/{id}.
    /// </summary>
    public record CteResponse
    {
        public required Guid Id { get; init; }

        #region Identificação
        public required string Uf { get; init; }
        public required int Series { get; init; }
        public required int Number { get; init; }
        public string? AccessKey { get; init; }
        public required DateTime IssueDateTime { get; init; }
        public required EnumResponse Type { get; init; }
        public required EnumResponse ServiceType { get; init; }
        public required EnumResponse TransportMode { get; init; }
        public required string PredominantCfop { get; init; }
        public required string OriginIbgeCityCode { get; init; }
        public required string DestinationIbgeCityCode { get; init; }
        public string? CarrierRntrc { get; init; }

        #endregion

        #region Emitente, remetente, destinatário, expedidor, recebedor
        public required CteCompanyResponse Company { get; init; }

        public required IReadOnlyCollection<CtePartnerResponse> Partners { get; init; }

        #endregion

        #region Valores
        public required decimal TotalServiceValue { get; init; }
        public required decimal AmountReceivable { get; init; }
        public required IReadOnlyCollection<CteFreightComponentResponse> FreightComponents { get; init; }

        #endregion

        #region Carga e Impostos

        public CteCargoResponse? Cargo { get; init; }
        public CteIcmsResponse? Icms { get; init; }

        public required IReadOnlyCollection<CteReferencedInvoiceResponse> ReferencedInvoices { get; init; }

        #endregion

        #region Ciclo de vida

        public required EnumResponse Status { get; init; }
        public string? AuthorizationProtocol { get; init; }
        public string? RejectionReason { get; init; }
        public required DateTime CreatedAt { get; init; }
        public DateTime? AuthorizedAt { get; init; }
        public DateTime? CanceledAt { get; init; }

        public required IReadOnlyCollection<CteEventResponse> Events { get; init; }


        #endregion
    }
}
