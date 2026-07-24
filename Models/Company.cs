namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Transportadora emitente do CTe
    /// </summary>
    public class Company
    {

        public Guid Id { get; private set; }

        public string Cnpj { get; private set; }
        public string StateTaxId { get; private set; } // IE - Inscrição 
        public string LegalName { get; private set; } // Razão social
        public string TradeName { get; private set; } // Nome fantasia

        /// <summary>
        /// Código de Regime Tributário - CRT (1-Simples, 2-Simples excesso, 3-Normal)
        /// </summary>
        public int TaxRegimeCode { get; private set; }

        public Address Address { get; private set; }

        /// <summary>
        /// Registro Nacional de Transportadores Rodoviários de Cargas
        /// Obrigatório para o modal rodoviário
        /// </summary>
        public string Rntrc { get; private set; }

        
        // Configuração fiscal / ingração SEFAZ
        public string IssuingAuthorityState { get; private set; } // UF autorizadora
        public bool IsProductionEnviroment { get; private set; } // false = homologação
        public string CertificateThumbprint { get; private set; } // referêrencia ao certificado A1/A3 

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
