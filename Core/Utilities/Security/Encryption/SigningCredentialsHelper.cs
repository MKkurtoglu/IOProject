using Microsoft.IdentityModel.Tokens;

namespace Base.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        // asp.net core hangi algoritmayı kullanacğaını burada belritiyoruz
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
