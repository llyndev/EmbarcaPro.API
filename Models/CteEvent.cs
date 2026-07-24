using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Registro de um evento vinculado a um CT-e já autorizado.
    /// </summary>
    public class CteEvent
    {

        public Guid Id { get; init; }
        public Guid CteId { get; init; }

        public CteEventType Type { get; init; }
        public int SequenceNumber { get; init; } // nSeqEvento - controla mútiplas CC-e, por exemplo
        public DateTime EventDateTime { get; init; }
        public string? Justification { get; init; }
        public string AuthorizationProtocol { get; init; }
        public string EventXml { get; init; }

        protected CteEvent()
        {

        }

        public CteEvent(Guid cteId, CteEventType type, int sequenceNumber, DateTime eventDateTime, string? justification = null)
        {
            CteId = cteId;
            Type = type;
            SequenceNumber = sequenceNumber;
            EventDateTime = eventDateTime;
            Justification = justification?.Trim();
        }

    }
}
