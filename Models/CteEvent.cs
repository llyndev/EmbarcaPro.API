using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Registro de um evento vinculado a um CT-e já autorizado.
    /// </summary>
    public class CteEvent
    {

        public int Id { get; init; }
        public int CteId { get; private set; }

        public CteEventType Type { get; init; }

        public int SequenceNumber { get; init; } // nSeqEvento - permite múltiplas cartas de correção no mesmo CT-e.
        public DateTime EventDateTime { get; init; }
        public string? Justification { get; init; }

        public string? AuthorizationProtocol { get; private set; } // preenchido só após retorno da SEFAZ
        public string? EventXml { get; private set; }

        protected CteEvent()
        {

        }

        public CteEvent(CteEventType type, int sequenceNumber, DateTime eventDateTime, string? justification = null)
        {
            
            // Verifica se o CteEventType é Cancellation ou CorrectionLetter 
            var requiresJustification = type is CteEventType.Cancellation or CteEventType.CorrectionLetter;

            // Verifica se existe justificativa
            if (requiresJustification)
            {
                if (string.IsNullOrWhiteSpace(justification)
                    throw new ArgumentException($"O evento {type} exige justificativa.", nameof(justification));

                var texto = justification.Trim();

                if (texto.Length is < 15 or > 255)
                    throw new ArgumentException("A justificativa deve ter entre 15 a 255 caracteres.", nameof(justification));
            }

            if (sequenceNumber < 1)
                throw new ArgumentException("A sequência do evento deve ser maior ou igual a 1.", nameof(sequenceNumber));
            

            Type = type;
            SequenceNumber = sequenceNumber;
            EventDateTime = eventDateTime;
            Justification = string.IsNullOrWhiteSpace(justification) ? null : justification.Trim();
        }

        public void Authorize(string protocol, string eventXml)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(protocol);

            AuthorizationProtocol = protocol.Trim();
            EventXml = eventXml;
        }

    }
}
