


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RemindeGo.DataAccess.Entity;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace RemindeGo.Common.Utilities;

public class TokenGenerator : ITokenGenerator
{

    private readonly JWtSettings _jwtSettings;

    public TokenGenerator(IOptions<JWtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateJWtToken(AppUser user)
    {



        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKeys));
        var credentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);


        ICollection<Claim> claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName?? ""),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())

        };

        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
        {
            Issuer = _jwtSettings.ValidIssuer,
            Audience = _jwtSettings.ValidAudeinces.First(),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credentials,



        };


        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SecurityToken token = handler.CreateToken(descriptor);


        return handler.WriteToken(token);


        // token.Claims 

    }
}
