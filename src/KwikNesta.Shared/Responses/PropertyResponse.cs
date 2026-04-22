
namespace KwikNesta.Shared.Responses
{
    public static class PropertyResponse
    {
        public static readonly string InvalidRequest = "Invalid request";
        public static readonly string LocationInfoRequired = "State and Country names are required";
        public static readonly string InvalidSelectedFeatures = "One or more features are invalid";
        public static readonly string RecordNotFound = "{0} record not found";
        public static readonly string UploadValidFiles = "Please upload valid file(s).";
        public static readonly string Invalidfile = "Invalid file or size exceeds the acceptable limits";
        public static readonly string WillExceedMaxUploadCount = "You can only upload {0} more files.";
        public static readonly string DuplicatesDetectedInFile = "Duplicate files detected in upload.";
        public static readonly string AtLeastOneDocRequired = "At least one document is required";
        public static readonly string VerificationRequestSubmitted = "Verification request submitted successfully. You'll be notified once reviewed.";
        public static readonly string UnsupportedFileType = "Unsupported file type.";
        public static readonly string TitleDeedRequired = "Title deed is required";
        public static readonly string HasPendingVerificationRequest = "A verification request is already pending";
        public static readonly string PleaseSpecifyForOther = "Please specify for Other document type";
        public static readonly string UserNotAuthenticated = "User must be authenticated";
        public static readonly string RequestProcessed = "Request already processed";
        public static readonly string RejectionReasonRequired = "Rejection reason is required";
        public static readonly string VerificationRequestReviewed = "Property verification request successfully reviewed.";
        public static readonly string PropertyVerificationInformationSubject = "Property Verification {0}";
        public static readonly string PropertyApprovalInformationMessage = "Good news — Your property \"{0}\" has been verified.<br>Your submitted documents have been reviewed and approved. Your property is now marked as verified and may receive increased visibility and trust from potential users.<br>You can log in to your account to view your property details.<br>If you have any questions, feel free to reach out to our support team.";
        public static readonly string PropertyDeclineInformationMessage = "We’ve reviewed your property verification request, but unfortunately, we were unable to approve it at this time.<br><br>Reason:<br>{0}<br><br>Please review the feedback above and submit a new verification request with the required or corrected documents.<br>If you need help, our support team is available to assist you.";
        public static readonly string PropertyLocationUpdated = "Property Location successfully updated.";
        public static readonly string PropertyInfoUpdated = "Property info successfully updated.";
        public static readonly string ViewRequestSubject = "Inspection Request Scheduled for Your Property";
        public static readonly string ViewRequestMessage = "You have a new inspection request for your property.<br><br><b>Property:</b> {0}<br><b>Location:</b> {1}<br><b>Requested Date & Time:</b> {2}<br><b>Mode:</b> {3}<br>Kindly note that this request will expire within the next {4} hours. Delayed responses may result in the requester exploring other available properties.";
        public static readonly string ConflictingViewRequest = "There is an existing inspection request for the property within the selected time frame. Please select a different time or date to continue.";
        public static readonly string InvalidRequestId = "Invalid request id";
        public static readonly string ViewRequestAlreadyApproved = "View request already approved.";
        public static readonly string ViewRequestApproved = "View session successfully approved.";
        public static readonly string ViewRequestWasCancelled = "View request had already been cancelled.";
        public static readonly string ViewRequestSessionSubject = "Property Inspection Scheduled";
        public static readonly string ViewRequestSessionReminderSubject = "[Reminder] Property Inspection Coming Up";
    }
}