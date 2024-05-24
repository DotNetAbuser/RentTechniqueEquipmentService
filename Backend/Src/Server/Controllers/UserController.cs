namespace Server.Controllers;

[ApiController]
[Route("api/identity/user")]
public class UserController(
    IUserService userService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(SignUpRequest request)
    {
        return Ok(await userService.CreateAsync(request));
    }
}