using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Common.Helpers
{
    /// <summary>
    /// Listas de enumeradores
    /// </summary>
    public class EmbarcaProEnumsList
    {
        // ---------------- Freight Status ----------------
        public static List<KeyDescriptionValue<FreightStatus>> GetFreightStatus()
        {
            return new List<KeyDescriptionValue<FreightStatus>>
            {
                new KeyDescriptionValue<FreightStatus>(FreightStatus.Pending, "P"),
                new KeyDescriptionValue<FreightStatus>(FreightStatus.InTransit, "T"),
                new KeyDescriptionValue<FreightStatus>(FreightStatus.Delivered, "E"),
                new KeyDescriptionValue<FreightStatus>(FreightStatus.Canceled, "C")
            };
        }
        public static string GetFreightStatusValue(FreightStatus enumItem)
            => GetFreightStatus().Find(p => p.Enum == enumItem).Value;
        public static string GetFreightStatusDescription(FreightStatus enumItem)
            => GetFreightStatus().Find(p => p.Enum == enumItem).Description;

        // ---------------- User Role ----------------
        public static List<KeyDescriptionValue<UserRole>> GetUserRoles()
        {
            return new List<KeyDescriptionValue<UserRole>>
            {
                new KeyDescriptionValue<UserRole>(UserRole.Admin, "A"),
                new KeyDescriptionValue<UserRole>(UserRole.Operacional, "O"),
                new KeyDescriptionValue<UserRole>(UserRole.Consulta, "C"),
                new KeyDescriptionValue<UserRole>(UserRole.Suporte, "S")
            };
        }
        public static string GetUserRoleValue(UserRole enumItem)
            => GetUserRoles().Find(p => p.Enum == enumItem).Value;
        public static string GetUserRoleDescription(UserRole enumItem)
            => GetUserRoles().Find(p => p.Enum == enumItem).Description;

        // ---------------- User Status ----------------
        public static List<KeyDescriptionValue<UserStatus>> GetUserStatus()
        {
            return new List<KeyDescriptionValue<UserStatus>>
            {
                new KeyDescriptionValue<UserStatus>(UserStatus.Pending, "P"),
                new KeyDescriptionValue<UserStatus>(UserStatus.Active, "A"),
                new KeyDescriptionValue<UserStatus>(UserStatus.Blocked, "B")
            };
        }
        public static string GetUserStatusValue(UserStatus enumItem)
            => GetUserStatus().Find(p => p.Enum == enumItem).Value;
        public static string GetUserStatusDescription(UserStatus enumItem)
            => GetUserStatus().Find(p => p.Enum == enumItem).Description;

        // ---------------- Trailer Type ----------------
        public static List<KeyDescriptionValue<TrailerType>> GetTrailerTypes()
        {
            return new List<KeyDescriptionValue<TrailerType>>
            {
                new KeyDescriptionValue<TrailerType>(TrailerType.DryBox, "BS"),
                new KeyDescriptionValue<TrailerType>(TrailerType.Refrigerated, "RF"),
                new KeyDescriptionValue<TrailerType>(TrailerType.Bulk, "GR"),
                new KeyDescriptionValue<TrailerType>(TrailerType.CurtainSider, "SD"),
                new KeyDescriptionValue<TrailerType>(TrailerType.Flatbed, "PR")
            };
        }
        public static string GetTrailerTypeValue(TrailerType enumItem)
            => GetTrailerTypes().Find(p => p.Enum == enumItem).Value;
        public static string GetTrailerTypeDescription(TrailerType enumItem)
            => GetTrailerTypes().Find(p => p.Enum == enumItem).Description;

        // ---------------- CT-e Type ----------------
        public static List<KeyDescriptionValue<CteType>> GetCteTypes()
        {
            return new List<KeyDescriptionValue<CteType>>
            {
                new KeyDescriptionValue<CteType>(CteType.Normal, "N"),
                new KeyDescriptionValue<CteType>(CteType.Complementary, "C"),
                new KeyDescriptionValue<CteType>(CteType.Substitute, "S"),
                new KeyDescriptionValue<CteType>(CteType.Annulment, "A")
            };
        }
        public static string GetCteTypeValue(CteType enumItem)
            => GetCteTypes().Find(p => p.Enum == enumItem).Value;
        public static string GetCteTypeDescription(CteType enumItem)
            => GetCteTypes().Find(p => p.Enum == enumItem).Description;

        // ---------------- CT-e Service Type ----------------
        public static List<KeyDescriptionValue<CteServiceType>> GetCteServiceTypes()
        {
            return new List<KeyDescriptionValue<CteServiceType>>
            {
                new KeyDescriptionValue<CteServiceType>(CteServiceType.Normal, "N"),
                new KeyDescriptionValue<CteServiceType>(CteServiceType.Subcontracting, "S"),
                new KeyDescriptionValue<CteServiceType>(CteServiceType.Redispatch, "R"),
                new KeyDescriptionValue<CteServiceType>(CteServiceType.IntermediateRedispatch, "I"),
                new KeyDescriptionValue<CteServiceType>(CteServiceType.MultimodalLinkedService, "M")
            };
        }
        public static string GetCteServiceTypeValue(CteServiceType enumItem)
            => GetCteServiceTypes().Find(p => p.Enum == enumItem).Value;
        public static string GetCteTypeDescription(CteServiceType enumItem)
            => GetCteServiceTypes().Find(p => p.Enum == enumItem).Description;
    }
}
