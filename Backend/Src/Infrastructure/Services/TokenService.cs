namespace Infrastructure.Services;

public class TokenService(
    IUserRepository userRepository,
    ISessionRepository sessionRepository,
    IConfiguration configuration)
    : ITokenService
{
  public async Task<Result<SignInResponse>> SignInAsync(SignInRequest request)
    {
        var userEntity = await userRepository.GetByPhoneNumberWithRoleAsync(request.PhoneNumber);
        if (userEntity == null)
            return Result<SignInResponse>.Fail("Пользователь с данным номером телефона не найден!");

        var isPasswordVerify = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, userEntity.PasswordHash);
        if (!isPasswordVerify)
            return Result<SignInResponse>.Fail("Непривильный пароль!");
        
        var authToken = GenerateJwtToken(userEntity);
        var refreshToken = GenerateRefreshToken();
        
        var sessionEntity = new SessionEntity {
            UserId = userEntity.Id,
            Token = refreshToken,
            Expires = DateTime.UtcNow.AddDays(7),
        };
        await sessionRepository.CreateAsync(sessionEntity);
        var response = new SignInResponse(
            AuthToken: authToken,
            RefreshToken: refreshToken);
        return Result<SignInResponse>
            .Success(response, "Пользователь успешно авторизирован.");
    }

    public async Task<Result<SignInResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var refreshSessionEntity = await sessionRepository
            .GetByRefreshTokenAsync(request.RefreshToken);
        if (refreshSessionEntity == null)
            return Result<SignInResponse>.Fail("Сессии не существует, необходимо пройти аунтификацию!");
        
        if (refreshSessionEntity.Expires < DateTime.UtcNow)
            return Result<SignInResponse>.Fail("Сессия устарела, необходимо вновь пройти аунтификацию!");
        
        var userEntity = await userRepository.GetByIdWithRoleAsync(refreshSessionEntity.UserId);
        if (userEntity == null)
            return Result<SignInResponse>.Fail("Пользователя не найден!");
        
        await sessionRepository.DeleteAsync(refreshSessionEntity);
        var newAuthToken = GenerateJwtToken(userEntity);
        var newRefreshToken = GenerateRefreshToken();
        var newRefreshSessionEntity = new SessionEntity {
            UserId = userEntity.Id,
            Token = newRefreshToken,
            Expires = DateTime.UtcNow.AddDays(7),
        };
        await sessionRepository.CreateAsync(newRefreshSessionEntity);
        var tokenResponse = new SignInResponse(
            AuthToken: newAuthToken,
            RefreshToken: newRefreshToken);
        return Result<SignInResponse>
            .Success(tokenResponse, "Новая пара токенов успешно получены.");
    }
    
    private string GenerateJwtToken(UserEntity user) =>
        GenerateEncryptedToken(GetSigningCredentials(), GetClaims(user));

    private IEnumerable<Claim> GetClaims(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new(ClaimTypes.Role, user.Role.Name)
        };

        return claims;
    }

    private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var expiresStr = configuration.GetSection("JwtOptions:Expires").Value 
                         ?? throw new ArgumentException("Expires minutes is missing");
        var expiresMinutes = Convert.ToInt32(expiresStr);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
            signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        var encryptedToken = tokenHandler.WriteToken(token);

        return encryptedToken;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var secretKey = configuration.GetSection("JwtOptions:SecretKey").Value 
                        ?? throw new ArgumentException("Secret key is missing");
        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        return new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
    }
}