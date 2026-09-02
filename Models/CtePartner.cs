using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Entidade de associação pois o mesmo partner pode ser referenciados em papeis diferentes
    /// (rem, dest, exped, receb) 
    /// </summary>
    public class CtePartner
    {
        public int Id { get; init; }
        public int CteId { get; private set; }
        public int PartnerId { get; private set; }
        public virtual Partner Partner { get; private set; } = null!;
        public PartnerType Type { get; init; }

        protected CtePartner()
        {

        }

        public CtePartner(Partner partner, PartnerType type)
        {
            ArgumentNullException.ThrowIfNull(partner);

            Partner = partner;
            Type = type;
        }
    }
}
