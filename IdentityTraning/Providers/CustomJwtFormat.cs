using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace IdentityTraning.Providers
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _issuer;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            string symmetricKeyAsBase64 = ConfigurationManager.AppSettings["as:AudienceSecret"];
            byte[] keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(keyByteArray);
            SigningCredentials signingKey =
                new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            DateTimeOffset? issuer = data.Properties.IssuedUtc;
            DateTimeOffset? expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims,
                issuer.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);


            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}