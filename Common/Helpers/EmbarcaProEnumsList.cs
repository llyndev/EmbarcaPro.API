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

        // ---------------- CT-e Status ----------------
        public static List<KeyDescriptionValue<CteStatus>> GetCteStatus()
        {
            return new List<KeyDescriptionValue<CteStatus>>
            {
                new KeyDescriptionValue<CteStatus>(CteStatus.Draft, "D"),
                new KeyDescriptionValue<CteStatus>(CteStatus.AwaitingAuthorization, "W"),
                new KeyDescriptionValue<CteStatus>(CteStatus.Authorized, "A"),
                new KeyDescriptionValue<CteStatus>(CteStatus.Rejected, "R"),
                new KeyDescriptionValue<CteStatus>(CteStatus.Denied, "D"),
                new KeyDescriptionValue<CteStatus>(CteStatus.Contingency, "C")
            };
        }
        public static string GetCteStatusValue(CteStatus enumItem)
            => GetCteStatus().Find(p => p.Enum == enumItem).Value;
        public static string GetCteStatusDescription(CteStatus enumItem)
            => GetCteStatus().Find(p => p.Enum == enumItem).Description;

        // ---------------- CT-e Transport Mode ----------------
        public static List<KeyDescriptionValue<CteTransportMode>> GetCteTransportMode()
        {
            return new List<KeyDescriptionValue<CteTransportMode>>
            {
                new KeyDescriptionValue<CteTransportMode>(CteTransportMode.Road, "R"),
                new KeyDescriptionValue<CteTransportMode>(CteTransportMode.Air, "A"),
                new KeyDescriptionValue<CteTransportMode>(CteTransportMode.Waterway, "W"),
                new KeyDescriptionValue<CteTransportMode>(CteTransportMode.Rail, "F"),
                new KeyDescriptionValue<CteTransportMode>(CteTransportMode.Pipeline, "P"),
                new KeyDescriptionValue<CteTransportMode>(CteTransportMode.Multimodal, "M")
            };
        }
        public static string GetCteTransportModeValue(CteTransportMode enumItem)
            => GetCteTransportMode().Find(p => p.Enum == enumItem).Value;
        public static string GetCteTransportModeDescription(CteTransportMode enumItem)
            => GetCteTransportMode().Find(p => p.Enum == enumItem).Description;

        // ---------------- CT-e Event Type ----------------
        public static List<KeyDescriptionValue<CteEventType>> GetCteEventType()
        {
            return new List<KeyDescriptionValue<CteEventType>>
            {
                new KeyDescriptionValue<CteEventType>(CteEventType.Cancellation, "CC"),
                new KeyDescriptionValue<CteEventType>(CteEventType.CorrectionLetter, "CL"),
                new KeyDescriptionValue<CteEventType>(CteEventType.DeliveryReceipt, "DR"),
                new KeyDescriptionValue<CteEventType>(CteEventType.DeliveryReceiptCancellation, "DC"),
                new KeyDescriptionValue<CteEventType>(CteEventType.DeliveryFailure, "DF"),
                new KeyDescriptionValue<CteEventType>(CteEventType.DeliveryFailureCancellation, "DT"),
                new KeyDescriptionValue<CteEventType>(CteEventType.NonCompliantServiceProvision, "NS")
            };
        }
        public static string GetCteEventTypeValue(CteEventType enumItem)
            => GetCteEventType().Find(p => p.Enum == enumItem).Value;
        public static string GetCteEventTypeDescription(CteEventType enumItem)
            => GetCteEventType().Find(p => p.Enum == enumItem).Description;

        // ---------------- Partner Type ----------------
        public static List<KeyDescriptionValue<PartnerType>> GetPartnerType()
        {
            return new List<KeyDescriptionValue<PartnerType>>
            {
                new KeyDescriptionValue<PartnerType>(PartnerType.Shipper, "S"),
                new KeyDescriptionValue<PartnerType>(PartnerType.Consignee, "C"),
                new KeyDescriptionValue<PartnerType>(PartnerType.Dispatching, "D"),
                new KeyDescriptionValue<PartnerType>(PartnerType.Receiver, "R")
            };
        }
        public static string GetPartnerTypeValue(PartnerType enumItem)
            => GetPartnerType().Find(p => p.Enum == enumItem).Value;
        public static string GetPartnerTypeDescription(PartnerType enumItem)
            => GetPartnerType().Find(p => p.Enum == enumItem).Description;

        // ---------------- ICMS Tax Situation ----------------
        public static List<KeyDescriptionValue<IcmsTaxSituation>> GetIcmsTaxSituation()
        {
            return new List<KeyDescriptionValue<IcmsTaxSituation>>
            {
                new KeyDescriptionValue<IcmsTaxSituation>(IcmsTaxSituation.NormalTaxation, "NT"),
                new KeyDescriptionValue<IcmsTaxSituation>(IcmsTaxSituation.TaxationWithReducedBase, "TR"),
                new KeyDescriptionValue<IcmsTaxSituation>(IcmsTaxSituation.Exempt, "EX"),
                new KeyDescriptionValue<IcmsTaxSituation>(IcmsTaxSituation.NotTaxed, "NO"),
                new KeyDescriptionValue<IcmsTaxSituation>(IcmsTaxSituation.Deferred, "DF"),
                new KeyDescriptionValue<IcmsTaxSituation>(IcmsTaxSituation.TaxWithHolding, "TH"),
                new KeyDescriptionValue<IcmsTaxSituation>(IcmsTaxSituation.OthersWithExemptionGrant, "OT")
            };
        }
        public static string GetIcmsTaxSituationValue(IcmsTaxSituation enumItem)
            => GetIcmsTaxSituation().Find(p => p.Enum == enumItem).Value;
        public static string GetIcmsTaxSituationDescription(IcmsTaxSituation enumItem)
            => GetIcmsTaxSituation().Find(p => p.Enum == enumItem).Description;
    }
}
