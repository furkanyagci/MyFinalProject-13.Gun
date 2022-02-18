using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    public static class ClaimExtensions//Var olan Claim nesnesini Extensions(genişlettik) ettik. Yani yeni metotlar ekledik.
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)//ICollection<Claim>  bu şekilde bir yazım görürseniz var olan bir nesneyi genişletiyoruz demektir yani Extensions yapıyoruz demektir. Bu kod koleksiyonlardan Claim'in içine bu metodu ekle demektir.
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }
}
