
using Backend.API.Extentions;
using Backend.Common.Contracts.Auth;
using Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controller;

[Route("User")]
public class AuthController(IAuthService authService) : ControllerBase

{
    private readonly IAuthService _authService = authService;
    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IResult> Register([FromBody] RegisterUserRequest request)
    {
        if (!ModelState.IsValid)
        {

            ModelState.HandleValidationError();
        }
        var result = await _authService.Register(request);
        return result.HandleErrorOr();
    }

    [HttpPost("Verify")]
    [AllowAnonymous]
    public async Task<IResult> VErify([FromBody] RegisterUserRequest request)
    {
        if (!ModelState.IsValid)
        {

            ModelState.HandleValidationError();
        }
        var result = await _authService.Register(request);
        return result.HandleErrorOr();
    }
}