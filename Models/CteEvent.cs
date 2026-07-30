using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Registro de um evento vinculado a um CT-e já autorizado.
    /// </summary>
    public class CteEvent
    {

        public int Id { get; init; }
        public Guid PublicId { get; init; } = Guid.NewGuid();
        public Guid CteId { get; init; }

        public CteEventType Type { get; init; }
        public int SequenceNumber { get; init; } // nSeqEvento - controla mútiplas CC-e, por exemplo
        public DateTime EventDateTime { get; init; }
        public string? Justification { get; init; }
        public string? AuthorizationProtocol { get; private set; } // preenchido só após retorno da SEFAZ
        public string? EventXml { get; private set; }

        protected CteEvent()
        {

        }

        public CteEvent(Guid cteId, CteEventType type, int sequenceNumber, DateTime eventDateTime, string? justification = null)
        {
            
            // Verifica se o CteEventType é Cancellation ou CorrectionLetter 
            var requiresJustification = type is CteEventType.Cancellation or CteEventType.CorrectionLetter;

            // Verifica se existe justificativa
            if (requiresJustification && string.IsNullOrWhiteSpace(justification))
            {
                throw new ArgumentException($"O evento {type} exige justificativa.", nameof(justification));
            }

            CteId = cteId;
            Type = type;
            SequenceNumber = sequenceNumber;
            EventDateTime = eventDateTime;
            Justification = justification?.Trim();
        }

        public void Authorize(string protocol, string eventXml)
        {
            AuthorizationProtocol = protocol;
            EventXml = eventXml;
        }

    }
}
