namespace Shared.Contracts;

public record SignInResponse(
    [Required] string AuthToken,
    [Required] string RefreshToken);