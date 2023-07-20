using Gss.Application.Services.Authentication;
using Gss.Contracts.Authentification;
using Microsoft.AspNetCore.Mvc;
namespace Gss.Api.Controllers ;

[ApiController]
[Route("[controller]")]

public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticatioService _authenticatioService;

    public AuthenticationController(IAuthenticatioService authenticatioService)
    {
        _authenticatioService = authenticatioService;
    }

    


    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticatioService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token);

        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {

        var authResult = _authenticatioService.Login(
            request.Email,
            request.Password);

        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token);

        return Ok(response);

    }

}


