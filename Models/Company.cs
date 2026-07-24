namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Transportadora emitente do CTe
    /// </summary>
    public class Company
    {

        public Guid Id { get; init; }

        public string Cnpj { get; init; }
        public string StateTaxId { get; init; } // IE - Inscrição 
        public string LegalName { get; init; } // Razão social
        public string TradeName { get; init; } // Nome fantasia

        /// <summary>
        /// Código de Regime Tributário - CRT (1-Simples, 2-Simples excesso, 3-Normal)
        /// </summary>
        public int TaxRegimeCode { get; init; }

        public Address Address { get; init; }

        /// <summary>
        /// Registro Nacional de Transportadores Rodoviários de Cargas
        /// Obrigatório para o modal rodoviário
        /// </summary>
        public string Rntrc { get; init; }

        
        // Configuração fiscal / ingração SEFAZ
        public string IssuingAuthorityState { get; init; } // UF autorizadora
        public bool IsProductionEnviroment { get; init; } // false = homologação
        public string CertificateThumbprint { get; init; } // referêrencia ao certificado A1/A3 

        public int CurrentSeries { get; private set; }
        public int LastCteNumber { get; private set; }

        protected Company()
        {

        }

        public Company(string cnpj, string stateTaxId, string legalName, string tradeName, int taxRegimeCode,
            Address address, string issuingAuthorityState, int currentSeries)
        {
            Cnpj = cnpj.Trim();
            StateTaxId = stateTaxId.Trim();
            LegalName = legalName.Trim();
            TradeName = tradeName?.Trim();
            TaxRegimeCode = taxRegimeCode;
            Address = address;
            IssuingAuthorityState = issuingAuthorityState.Trim();
            CurrentSeries = currentSeries;
            LastCteNumber = 0;
        }


        /// <summary>
        /// Reserva e retorna o próximo número do CTe para a série atual desta empresa.
        /// </summary>
        public int GetNextCteNumber()
        {
            LastCteNumber++;
            return LastCteNumber;
        }
    }
}
