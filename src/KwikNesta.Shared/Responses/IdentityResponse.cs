namespace KwikNesta.Shared.Responses
{
    public static class IdentityResponse
    {
        public static readonly string UserExists = "A user already exists with this email, please login instead.";
        public static readonly string RegistrationFailed = "User registration failed. Please try again";
        public static readonly string AccountActivationSubject = "Email Confirmation";
        public static readonly string InvalidRegistrationRole = "Invalid role for registration.";
        public static readonly string AccountActivationMessage = "Welcome! Please use the following OTP to confirm your account:";
        public static readonly string AccountActivationSecurityNotice = "If you didn’t create this account, you can safely ignore this email.";
        public static readonly string InvalidRequest = "Invalid request. Please check your inputs and try again";
        public static readonly string UserNotFoundWithEmail = "No user found with the specified email address";
        public static readonly string InvalidOtp = "Invalid OTP. Please check and try again.";
        public static readonly string AccountVerificationSuccessful = "Account successfully verified. Please proceed to login";
        public static readonly string OtpExpired = "OTP has expired. Please request for a new one.";
        public static readonly string InvalidEmailOrPassword = "Email address and Password are required fields.";
        public static readonly string EmailNotConfirmedOrAccountInactive = "Account not activated. Please activate your account or contact support for assistance.";
        public static readonly string WrongPassword = "You entered an incorrect password.";
        public static readonly string NoAssignedRoles = "You have no assigned role. Please contact support for assistance.";
        public static readonly string UserNotFoundWithId = "No user found with the specified id";
        public static readonly string UserAlreadyVerified = "Account already verified. Please login";
        public static readonly string ActivationOtpSent = "OTP successfully resent. Please check your email.";
        public static readonly string PasswordResetSubject = "Reset Your Password";
        public static readonly string PasswordResetMessage = "We received a request to reset your password. Use the OTP code below to set a new password:";
        public static readonly string PasswordResetSecurityNotice = "If you didn’t request a password reset, you can safely ignore this email. Your account is safe.";
        public static readonly string AccountReactivationSubject = "Reactivate Your Account";
        public static readonly string AccountReactivationMessage = "We received a request to reactivate your account. Pleaseuse the OTP below to complete reactivation.";
        public static readonly string AccountReactivationSecurityNotice = "If you request a reactivation, you can safely ignore this email. Your account is safe.";
        public static readonly string WelcomeEmailSubject = "Welcome to {0}";
        public static readonly string InvalidRefreshToken = "Invalid refresh token";
        public static readonly string UserInactive = "User is inactive. Please reactivate your account to continue or contact support for assistance.";
        public static readonly string ExpiredToken = "Invalid or expired token";
        public static readonly string AccessDenied = "Access denied!!! You're not authorized to perform this action.";
        public static readonly string PasswordChanged = "Password changed successfully. Please login with the new password";
        public static readonly string PasswordChangedFailed = "Password change failed. Please try again shortly.";
        public static readonly string PasswordResetSuccessful = "Password reset request successful. Please enter the OTP sent to your email to complete the process";
        public static readonly string ForgotPasswordInformationSubject = "Password Reset Successful";
        public static readonly string ForgotPasswordInformationMessage = "This is a confirmation that your password has been successfully reset.<br>If you made this change, you can now sign in using your new password.<br>If you did not request this password reset, please contact our support team immediately so we can help secure your account.";
        public static readonly string ChangePasswordInformationSubject = "Your Password Was Changed";
        public static readonly string ChangePasswordInformationMessage = "This is a confirmation that the password for your account has been successfully changed.<br>If you made this change, no further action is required.<br>If you did not change your password, please reset your password immediately and contact our support team so we can help secure your account.";
        public static readonly string AccountDeactivationSuccessful = "Account deactivated successfully";
        public static readonly string AccountDeactivationSubject = "Your Account Has Been Deactivated";
        public static readonly string AccountDeactivationMessage = "Your account has been deactivated and you will no longer be able to access our services.<br>If you believe this was done in error or would like to request reactivation, please contact our support team for assistance.<br>Thank you for your understanding.";
        public static readonly string AccountReactivationRequestSuccessful = "Account reactivation request successful. Please use the OTP sent to your email to complete the process.";
        public static readonly string AccountReactivationSuccessful = "Account reactivation successful. You can proceed to login.";
        public static readonly string AccountReactivationInformationSubject = "Your Account Has Been Reactivated";
        public static readonly string AccountReactivationInformationMessage = "Your account has been successfully reactivated. You can now sign in and continue using our services as usual.<br>If you experience any issues accessing your account, please contact our support team for assistance.";
        public static readonly string UserAlreadySuspended = "User already suspended.";
        public static readonly string AccountSuspended = "Account successfully suspended.";
        public static readonly string SuspensionInformationSubject = "Your Account Has Been Suspended";
        public static readonly string SuspensionInformationMessage = "Your account has been temporarily suspended. Reason - ({0}). As a result, you will not be able to access our services at this time.<br>If you believe this action was taken in error or would like more information, please contact our support team for assistance.<br>We appreciate your understanding.";
        public static readonly string UserNotSuspended = "User account not suspended";
        public static readonly string UserRestored = "User account successfully restored.";
        public static readonly string AccountRestorationInformationSubject = "Your Account Has Been Restored";
        public static readonly string AccountRestorationInformationMessage = "Good news — your account has been successfully restored. You can now access all features and services as usual.<br>If you have any questions or encounter any issues, please don’t hesitate to contact our support team.<br><br>Welcome back!";
        public static readonly string UserDetailsUpdated = "User details successfully updated.";
    }
}