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
            // deixarei esse comentario para ver se o weslin vai ver oq foi feito aqui 
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
    }
}
