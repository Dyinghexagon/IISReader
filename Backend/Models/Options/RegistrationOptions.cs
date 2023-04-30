using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Backend.Models.Options
{
    public static class RegistrationOptions
    {
        public const String Secret = "eyJhbGciOiQssGts53G1";

        public const String Issuer = "IISReaderServer";

        public const String Audience = "IISReaderClient";

        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(Secret));
    }
}