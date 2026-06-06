using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Infra
{
    public enum EAuditAction
    {
        [Description("Logged In")]
        Login,
        [Description("Changed Password")]
        ChangedPassword,
        [Description("Deactivated Account")]
        DeactivatedAccount,
        [Description("Suspended Account")]
        SuspendedAccount,
        [Description("Restored Account")]
        RestoredAccount,
        [Description("Updated User Details")]
        UpdatedUserDetails = 1000,

        [Description("Migrated Location Data")]
        MigratedLocationData,
        [Description("Toggled Location Active Status")]
        LocationToggle = 1200,

        [Description("Added New Property")]
        AddedProperty,
        [Description("Added/Updated Property Features")]
        AddOrUpdatePropertyFeature,
        [Description("Uploaded Property Image(s)")]
        UploadedPropertyImage,
        [Description("Submitted Property Verification Request")]
        PropertyVerificationRequest,
        [Description("Property Verification Request Reviewed")]
        PropertyVerificationReviewed,
        [Description("Updated Property Location")]
        UpdatedPropertyLocation,
        [Description("Updated Property Basic Info")]
        UpdatedPropertyInfo = 1500,

        [Description("Property Inspection Requested")]
        ViewRequested,
        [Description("Property Inspection Request Fee Paid")]
        ViewRequestPaid,
        [Description("Property Inspection Approved")]
        ViewRequestApproved,
        [Description("Joined View Session")]
        JoinedViewSession,
        [Description("Property Inspection Declined")]
        ViewRequestDeclined = 1800,

        [Description("Initialized Payment")]
        InitializedPayment,
        [Description("Added Payment Intent")]
        AddedPaymentIntent,
        [Description("Added User Bank Account")]
        AddedUserBankAccount,
        InitializedRefund
    }
}