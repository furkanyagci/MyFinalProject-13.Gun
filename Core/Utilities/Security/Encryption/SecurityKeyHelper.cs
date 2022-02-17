using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper//Şifreleme olan sistemlerde bize herşeyi bir byte[] formatında veriyor olmamız gerekiyor format byte array olmalı. ASP.NET Json Web Token servislerinin anlayacağı hale getirmemiz gerekiyor.
    {

        public static SecurityKey CreateSecurityKey(string securityKey)//Buraları daha sonra arzu edenler araştırabilir.
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }

    }
}
