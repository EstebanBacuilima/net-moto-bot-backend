﻿namespace net_moto_bot.Domain.Enums.Custom;

public enum ExceptionEnum : short
{
    /// <summary>
    /// 
    /// </summary>
    IdCardRequired,
    NameIsAlreadyExists,
    /// <summary>
    ///
    /// </summary>
    FirstNameRequired,
    /// <summary>
    /// 
    /// </summary>
    LastNameRequired,
    /// <summary>
    /// 
    /// </summary>
    EmailRequired,
    /// <summary>
    /// 
    /// </summary>
    InvalidExtensionType,
    /// <summary>
    /// Thrown if there already exists an account with the email address asserted by the credential.
    /// </summary>
    AccountExistsWithDifferentCredential,
    /// <summary>
    /// Thrown if exists company with unique person id card.
    /// </summary>
    AlreadyExistsCompanyWithThatIdCard,
    /// <summary>
    /// Thrown if there already exists an account with the given email address.
    /// </summary>
    EmailAlreadyExists,
    /// <summary>
    /// Thrown if the establishment does not contains an address.
    /// </summary>
    EstablishmentAddressIsRequired,
    /// <summary>
    /// Thrown if account expired.
    /// </summary>
    ExpiredAccount,
    /// <summary>
    /// Thrown if OTP in email link expires.
    /// </summary>
    ExpiredActionCode,
    /// <summary>
    /// Thrown if credentials expired.
    /// </summary>
    ExpiredCredential,
    /// <summary>
    /// Thrown if the token has expired.
    /// </summary>
    ExpiredToken,
    /// <summary>
    /// Thrown if user try access to forbidden resource.
    /// </summary>
    Forbidden,
    /// <summary>
    /// Thrown if the id card already exists.
    /// </summary>
    IdCardAlreadyExists,
    /// <summary>
    /// Thrown if the company name is invalid.
    /// </summary>
    IncorrectCurrentPassword,
    /// <summary>
    /// Thrown if the company name is invalid.
    /// </summary>
    InvalidCompanyName,
    /// <summary>
    /// Thrown if the credential is malformed or has expired.
    /// </summary>
    InvalidCredential,
    /// <summary>
    /// Thrown if the email address is not valid.
    /// </summary>
    InvalidEmail,
    /// <summary>
    /// Thrown if the establishment name is invalid.
    /// </summary>
    InvalidEstablishmentName,
    /// <summary>
    /// Thrown if person first name is invalid.
    /// </summary>
    InvalidFirstName,
    /// <summary>
    /// Thrown if person id card is invalid.
    /// </summary>
    InvalidIdCard,
    /// <summary>
    /// Thrown if person last name is invalid.
    /// </summary>
    InvalidLastName,
    /// <summary>
    /// Thrown if address main street is invalid.
    /// </summary>
    InvalidMainStreet,
    /// <summary>
    /// Thrown if the password length is invalid.
    /// </summary>
    InvalidPasswordLength,
    /// <summary>
    /// Thrown if the person code is invalid.
    /// </summary>
    InvalidPersonCode,
    /// <summary>
    /// Thrown if the person first name or last name or social reason are invalid.
    /// </summary>
    InvalidPersonNames,
    /// <summary>
    /// Thrown if the political division type is invalid.
    /// </summary>
    InvalidPoliticalDivisionType,
    /// <summary>
    /// Thrown if the person social reason is invalid.
    /// </summary>
    InvalidSocialReason,
    /// <summary>
    /// Thrown if the token is invalid.
    /// </summary>
    InvalidToken,
    /// <summary>
    /// Thrown if verification Url is not valid
    /// </summary>
    InvalidUrl,
    /// <summary>
    /// Thrown if verification code of the credential is not valid.
    /// </summary>
    InvalidVerificationCode,
    /// <summary>
    /// Thrown if verification ID of the credential is not valid.
    /// </summary>
    InvalidVerificationId,
    InvalidName,
    AttributeAlreadyExists,
    /// <summary>
    /// Thrown if locked account.
    /// </summary>
    LockedAccount,
    /// <summary>
    /// Thrown if company already contains a main establishment.
    /// </summary>
    MainEstablishmentAlreadyExists,
    /// <summary>
    /// Thrown if company does not contains a main establishment.
    /// </summary>
    MainEstablishmentIsRequired,
    /// <summary>
    /// Thrown if operation is not allowed.
    /// </summary>
    OperationNotAllowed,
    /// <summary>
    /// Thrown if role permissions is empty.
    /// </summary>
    PermissionsAreRequired,
    /// <summary>
    /// Thrown if phone number already exists.
    /// </summary>
    PhoneNumberAlreadyExists,
    /// <summary>
    /// Thrown if the previous password is invalid.
    /// </summary>
    PreviousPasswordIsIncorrect,
    /// <summary>
    /// Thrown if provider not available.
    /// </summary>
    ProviderNotAvailable,
    /// <summary>
    /// Thrown if role name already exists.
    /// </summary>
    RoleNameAlreadyExists,
    /// <summary>
    /// Thrown if taxpayer number is null when company is a special taxpayer.
    /// </summary>
    TaxpayerNumberIsRequired,
    /// <summary>
    ///  Thrown id card use by another person.
    /// </summary>
    TheIdCardIsInUse,
    /// <summary>
    ///  Thrown if role does not sent.
    /// </summary>
    TheRoleIsRequired,
    /// <summary>
    /// Thrown if unauthorized user.
    /// </summary>
    Unauthorized,
    /// <summary>
    /// Thrown if the user corresponding to the given email has been disabled.
    /// </summary>
    UserDisabled,
    /// <summary>
    /// Thrown if exists a user with id card.
    /// </summary>
    UserIdCardAlreadyExists,
    /// <summary>
    /// Thrown if the credential given does not correspond to the user.
    /// </summary>
    UserMismatch,
    /// <summary>
    /// Thrown if username already exists.
    /// </summary>
    UsernameAlreadyExist,
    /// <summary>
    /// Thrown if there is no user corresponding to the given email or username.
    /// </summary>
    UserNotFound,
    /// <summary>
    /// Thrown if the password is not strong enough.
    /// </summary>
    WeakPassword,
    /// <summary>
    /// Thrown if the password is invalid for the given email, or the account corresponding to the email doesn't have a password set.
    /// </summary>
    WrongPassword,
}
