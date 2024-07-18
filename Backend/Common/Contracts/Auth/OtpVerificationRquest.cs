using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Backend.Common.Contracts.Auth;

public record OtpVerificationRequest(string Email, string Otp);