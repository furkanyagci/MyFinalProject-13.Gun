using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }// Sizin Apinizdeki appsettings.json'ı okumanıza yarıyor.
        private TokenOptions _tokenOptions; //Üst satırda okudumuz ayarları bu nesneye atacağız. TokenOption bu tokenın değerleri anlamında verilen bir isimdir. Bu nesneyi githubdan kopyaladık.
        private DateTime _accessTokenExpiration;//AccessToken ne zaman geçersizleşecek.
        public JwtHelper(IConfiguration configuration)// buradaki configuration .Net Core veriyor. Sen beni enjecte et ben API'nin Configurasyon nesnesini enjecte ederim diyor.
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();//Section appsetting.json içine yazdığımız TokenOptions'ın tümüdür. Bu setting'i al TokenOptions.cs kullanarak map'le kısacası class içindeki property'lere atıyor. Configuration.GetSection("TokenOptions") => Konfigurasyondaki Alanı bul TokenOptions isimli alanı TokenOptions.cs'ye atama yap. Configuration görünce appsetting.json aklına gelsin.
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);//AccessToken süresini ekliyoruz. 10 dk idi.
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);//_tokenOptions.SecurityKey kullanarak Security key oluştur demek.
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);//  CreateSigningCredentials oluştur
             var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);// 
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            //.Net te bize ait olmayan bir nesneye yeni metotlar eklenebiliyor buna Extensions deniyor.

            //buradaki yapı için githubdan kod kopyaladık. github > NetCoreBackend/Core/Extensions/ bu klasörü ekleyince burası hata vermeyi bıraktı.
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
