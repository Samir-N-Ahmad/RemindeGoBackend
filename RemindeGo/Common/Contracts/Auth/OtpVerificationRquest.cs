using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RemindeGo.Common.Contracts.Auth;

public record OtpVerificationRequest(string Email, string Otp);
public record ResendOtpRequest(string Email);