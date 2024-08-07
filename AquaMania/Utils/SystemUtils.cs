using AquaMania.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace AquaMania.Utils;

public class SystemUtils
{
    public string GenerateToken(string secretKey, Guid id, string name, string email)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        byte[] bytes = Encoding.ASCII.GetBytes(secretKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(bytes),
            SecurityAlgorithms.HmacSha256Signature);
        SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor();
        securityTokenDescriptor.Subject = new ClaimsIdentity(new Claim[3]
        {
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", email),
            new Claim("name", name),
            new Claim("id", id.ToString())
        });
        securityTokenDescriptor.Expires = DateTime.UtcNow.AddHours(3);
        securityTokenDescriptor.SigningCredentials = credentials;
        SecurityTokenDescriptor tokenDescriptor = securityTokenDescriptor;


        SecurityToken token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
        return jwtSecurityTokenHandler.WriteToken(token);
    }

    public DecodeTokenModel GetDecodeToken(string token, string secret)
    {
        DecodeTokenModel decodedToken = new DecodeTokenModel();
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtSecurityToken = jwtSecurityTokenHandler.ReadToken(token) as JwtSecurityToken;
        if (IsValidToken(token, secret))
        {
            foreach (Claim claim in jwtSecurityToken.Claims)
            {
                if (claim.Type == "email")
                {
                    decodedToken.Email = claim.Value;
                }
                else if (claim.Type == "name")
                {
                    decodedToken.Name = claim.Value;
                }
                else if (claim.Type == "id")
                {
                    decodedToken.Id = new Guid(claim.Value);
                }
            }

            return decodedToken;
        }

        throw new Exception("invalidToken");
    }
    public bool IsValidToken(string token, string secret)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("emptyToken");
        }
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters();
        tokenValidationParameters.ValidateIssuer = false;
        tokenValidationParameters.ValidateAudience = false;
        tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Base64UrlEncoder.Encode(secret)));

        try
        {
            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public string HashPassword(string password)
    {
        return new PasswordHasher<object?>().HashPassword(null, password);
    }
    public bool VerifyPasswordHash(string password, string hashedPassword)
    {
        var passwordVerificationResult = new PasswordHasher<object?>().VerifyHashedPassword(null, hashedPassword, password);
        switch (passwordVerificationResult)
        {
            case PasswordVerificationResult.Failed:
                return false;
            case PasswordVerificationResult.Success:
                return true;
            case PasswordVerificationResult.SuccessRehashNeeded:
                return true;
            default:
                return false;
        }
    }
}
