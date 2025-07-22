using Inteia.Api.Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var success = await _authService.RegisterAsync(request);
        return success ? Ok("Registrado con éxito") : BadRequest("El correo ya está registrado");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.LoginAndGenerateTokenAsync(request);

        if (token == null)
            return Unauthorized("Credenciales inválidas");

        var cookieOptions = new CookieOptions
        {
            HttpOnly = false, 
            Secure = Request.IsHttps,
            SameSite = SameSiteMode.Lax, 
            Expires = DateTime.UtcNow.AddDays(1),
            Path = "/"
        };

        Response.Cookies.Append("token", token, cookieOptions);

        return Ok(new
        {
            message = "Login exitoso"
        });
    }
}
