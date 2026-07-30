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
        public Guid PublicId { get; init; } = Guid.NewGuid();
        public Guid CteId { get; init; }
        public Guid PartnerId { get; init; }
        public virtual Partner Partner { get; private set; } = null!;
        public PartnerType Type { get; init; }

        protected CtePartner()
        {

        }

        public CtePartner(Guid cteId, Partner partner, PartnerType type)
        {
            CteId = cteId;
            Partner = partner;
            PartnerId = partner.Id;
            Type = type;
        }
    }
}
