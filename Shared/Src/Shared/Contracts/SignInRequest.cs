namespace Shared.Contracts;

public class SignInRequest
{
    [Required(ErrorMessage = "Поле номер телефона не должно быть пустым!")] [Phone(ErrorMessage = "Поле не соответсвует номеру телефона!")] public string PhoneNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Поле пароль не должно быть пустым!")] public string Password { get; set; } = string.Empty;
}