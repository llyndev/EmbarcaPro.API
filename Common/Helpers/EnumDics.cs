using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Common.Helpers
{
    public static class EnumDics
    {
        public static List<KeyDescriptionValue<UserStatus>> GetUserStatusTypes()
        {
            var lst = new List<KeyDescriptionValue<UserStatus>>
            {
                new KeyDescriptionValue<UserStatus>(UserStatus.Pending, "P"),
                new KeyDescriptionValue<UserStatus>(UserStatus.Active, "A"),
                new KeyDescriptionValue<UserStatus>(UserStatus.Blocked, "B")
            };

            return lst;
        }

    }
}
