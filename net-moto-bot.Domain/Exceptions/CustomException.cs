using net_moto_bot.Domain.Enums.Custom;

namespace net_moto_bot.Domain.Exceptions;

public class CustomException(ExceptionEnum exceptionEnum) : Exception
{
    private readonly ExceptionEnum _exceptionEnum = exceptionEnum;

    public string Code
    {
        get
        {
            return _exceptionEnum switch
            {
                ExceptionEnum.IdCardRequired => "id-card-required",
                ExceptionEnum.FirstNameRequired => "first-name-required",
                ExceptionEnum.LastNameRequired => "last-name-required",
                ExceptionEnum.EmailRequired => "email-required",
                ExceptionEnum.InvalidExtensionType => "invalid-extension-type",
                ExceptionEnum.AccountExistsWithDifferentCredential => "account-exists-with-different-credential",
                ExceptionEnum.AlreadyExistsCompanyWithThatIdCard => "already-exists-company-with-that-id-card",
                ExceptionEnum.EmailAlreadyExists => "email-already-in-use",
                ExceptionEnum.EstablishmentAddressIsRequired => "establishment-address-is-required",
                ExceptionEnum.ExpiredAccount => "expired-account",
                ExceptionEnum.ExpiredActionCode => "expired-action-code",
                ExceptionEnum.ExpiredCredential => "expired-credential",
                ExceptionEnum.ExpiredToken => "expired-token",
                ExceptionEnum.Forbidden => "forbidden",
                ExceptionEnum.IdCardAlreadyExists => "id-card-already-exists",
                ExceptionEnum.IncorrectCurrentPassword => "incorrect-current-password",
                ExceptionEnum.InvalidCompanyName => "invalid-company-name",
                ExceptionEnum.InvalidCredential => "invalid-credential",
                ExceptionEnum.NameIsAlreadyExists => "name-is-already-exists",
                ExceptionEnum.InvalidEmail => "invalid-email",
                ExceptionEnum.InvalidEstablishmentName => "invalid-establishment-name",
                ExceptionEnum.InvalidFirstName => "invalid-first-name",
                ExceptionEnum.InvalidIdCard => "invalid-id-card",
                ExceptionEnum.InvalidLastName => "invalid-last-name",
                ExceptionEnum.InvalidMainStreet => "invalid-main-street",
                ExceptionEnum.InvalidPasswordLength => "invalid-password-length",
                ExceptionEnum.InvalidPersonCode => "invalid-person-code",
                ExceptionEnum.InvalidPersonNames => "invalid-person-names",
                ExceptionEnum.InvalidPoliticalDivisionType => "invalid-political-division-type",
                ExceptionEnum.InvalidSocialReason => "invalid-social-reason",
                ExceptionEnum.InvalidToken => "invalid-token",
                ExceptionEnum.InvalidUrl => "invalid-url",
                ExceptionEnum.InvalidVerificationCode => "invalid-verification-code",
                ExceptionEnum.InvalidVerificationId => "invalid-verification-id",
                ExceptionEnum.InvalidName => "invalid-name",
                ExceptionEnum.LockedAccount => "locked-account",
                ExceptionEnum.MainEstablishmentAlreadyExists => "main-establishment-already-exists",
                ExceptionEnum.MainEstablishmentIsRequired => "main-establishment-is-required",
                ExceptionEnum.OperationNotAllowed => "operation-not-allowed",
                ExceptionEnum.PermissionsAreRequired => "permissions-are-required",
                ExceptionEnum.PhoneNumberAlreadyExists => "phone-number-already-exists",
                ExceptionEnum.PreviousPasswordIsIncorrect => "previous-password-is-incorrect",
                ExceptionEnum.ProviderNotAvailable => "provider-not-available",
                ExceptionEnum.TheIdCardIsInUse => "the-id-card-is-in-user",
                ExceptionEnum.RoleNameAlreadyExists => "role-name-already-exists",
                ExceptionEnum.TaxpayerNumberIsRequired => "taxpayer-number-is-required",
                ExceptionEnum.TheRoleIsRequired => "the-role-is-required",
                ExceptionEnum.Unauthorized => "unauthorized",
                ExceptionEnum.UserDisabled => "user-disabled",
                ExceptionEnum.UserIdCardAlreadyExists => "user-id-card-already-exists",
                ExceptionEnum.UserMismatch => "user-mismatch",
                ExceptionEnum.UsernameAlreadyExist => "username-already-exist",
                ExceptionEnum.UserNotFound => "user-not-found",
                ExceptionEnum.WeakPassword => "weak-password",
                ExceptionEnum.WrongPassword => "wrong-password",
                ExceptionEnum.AttributeAlreadyExists => "attribute-already-exists",
                _ => _exceptionEnum.ToString(),
            };
        }
    }
}
