using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {//Credential Bir sisteme girmek için elinizde olanlardır.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);//ASP.NET diiyorsun ki anahtar olarak securityKey kullan şifreleme olarak da güvenlik algoritmalarından SecurityAlgorithms.HmacSha512Signature kullan.
        }
    }
}
