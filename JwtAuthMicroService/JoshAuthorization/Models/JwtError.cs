namespace JoshAuthorization.Models;

public enum JwtError
{
    Successful = 0,
    InvalidToken,
    InvalidType,
    InvalidIssuer,
    InvalidAudience,
    InvalidJTI,
    ExpiredToken,
    MissingToken,
    UntimelyToken,
    UnsyncToken,
    InvalidHtm,
    InvalidHtu,
    InvalidBinding,
    MissingScheme,
    InvalidAth,
    UnexpectedError
}