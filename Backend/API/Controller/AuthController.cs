
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

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            ModelState.HandleValidationError();
        }
        var result = await _authService.Login(request);
        return result.HandleErrorOr();
    }

    [HttpGet("Verify")]
    [AllowAnonymous]
    public async Task<IResult> Verify([FromQuery] OtpVerificationRequest request)
    {
        if (!ModelState.IsValid)
        {

            ModelState.HandleValidationError();
        }
        var result = await _authService.OtpVerification(request);
        return result.HandleErrorOr();
    }

    [HttpPost("SendOtpEmail")]
    [AllowAnonymous]
    public async Task<IResult> SendOtpVerificationEmail([FromBody] ResendOtpRequest request)
    {
        if (!ModelState.IsValid)
        {

            ModelState.HandleValidationError();
        }
        var result = await _authService.ResendVerificationEmail(request.Email);
        return result.HandleErrorOr();
    }
}