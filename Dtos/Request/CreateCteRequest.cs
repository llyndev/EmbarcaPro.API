using EmbarcaPro.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{

    /// <summary>
    /// Dados para emitir um CT-e.
    /// </summary>
    public record CreateCteRequest : IValidatableObject
    {

        [Required(ErrorMessage = "A transportadora emitente é obrigatória.")]
        public required Guid CompanyPublicId { get; init; }


        /// <summary>
        /// Viagem que este CT-e documenta. Opcional: nem todo CT-e é de um frete cadastrado
        /// </summary>
        public Guid? FreightPublicId { get; init; }

        [Required(ErrorMessage = "O tipo de CT-e é obrigatório.")]
        [EnumDataType(typeof(CteType), ErrorMessage = "Tipo de CT-e inválido.")]
        public required CteType Type { get; init; }

        [Required(ErrorMessage = "O tipo de serviço é obrigatório.")]
        [EnumDataType(typeof(CteServiceType), ErrorMessage = "Tipo de serviço inválido.")]
        public required CteServiceType ServiceType { get; init; }

        [Required(ErrorMessage = "O modal de transporte é obrigatório.")]
        [EnumDataType(typeof(CteTransportMode), ErrorMessage = "Modal de transporte inválido.")]
        public required CteTransportMode TransportMode { get; init; }

        [Required(ErrorMessage = "O CFOP é obrigatório.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "O CFOP deve ter 4 dígitos.")]
        public required string PredominantCfop { get; init; }

        [Required(ErrorMessage = "O município de início da prestação é obrigatório.")]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "O código IBGE de origem deve ter 7 dígitos.")]
        public required string OriginIbgeCode { get; init; }

        [Required(ErrorMessage = "O município de fim da prestação é obrigatório.")]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "O código IBGE de destino deve ter 7 dígitos.")]
        public required string DestinationIbgeCode { get; init; }

        [Required(ErrorMessage = "Informe remetente e destinatário.")]
        [MinLength(2, ErrorMessage = "O CT-e exige ao menos remetente e destinatário.")]
        public required List<CtePartnerRequest> Partners { get; init; }

        [Range(0.01, 99_999_999, ErrorMessage = "O valor total da prestação deve ser maior que zero.")]
        public required decimal TotalServiceValue { get; init; }

        [Range(0, 99_999_999, ErrorMessage = "O valor a receber deve ser válido.")]
        public required decimal AmountReceivable { get; init; }

        [Required(ErrorMessage = "Informe ao menos um componente de frete.")]
        [MinLength(1, ErrorMessage = "Informe ao menos um componente de frete.")]
        public required List<CteFreightComponentRequest> FreightComponents { get; init; }


        [Required(ErrorMessage = "As informações de carga são obrigatórias.")]
        public required CteCargoRequest Cargo { get; init; }

        [Required(ErrorMessage = "Informe ao menos uma NF-e transportada.")]
        [MinLength(1, ErrorMessage = "O CT-e precisa referenciar ao menos uma NF-e.")]
        public required List<CteReferencedInvoiceRequest> ReferencedInvoices { get; init; }


        [Required(ErrorMessage = "As informações de ICMS são obrigatórias.")]
        public required CteIcmsRequest Icms { get; init; }

    }
}
