using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{// Hashing Helper hash oluşturmaya ve onu doğrulamaya yarıyor. SecurityKeyHelper bizim elimizde uyduruk string var onu bir byte array haline getirmemiz gerekir onu yapar.Bunlar kısacası bizim JWT(Json Web Token) ihtiyaç duyduğu yapılar. SigningCredentialsHelper JWT sistemini yönetirken güvenlik anahtarın ve şifreleme algoritman budur diye söylüyoruz.




    //BU bizim için bir araç ve static olacak o yüzden buna interface implemente etemk zorunda değiliz(çıplak kalabilir yani).
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)//Gelen şifrenin Hash'ini oluşturacak ve şifrenin Salt'ınıda oluşturacak. Salt : Girilen şifreyi Hashlerken sistemin eklediği değerlerdir. passwordHash dışarıya verilecek değer
        {//Burada .Net kripto sınıflarından yararlanacağız.

            using (var hmac= new System.Security.Cryptography.HMACSHA512())//bu algoritmayı kullanarak passwordh hash ve saltı oluşturacağız.
            {
                passwordSalt = hmac.Key;// new System.Security.Cryptography.HMACSHA512() algoritmasının şifre geldiği zamanda oluşturduğu bir key algoritmasıdır. Her şifre geldiğinde farklı oluşturur o yüzden güvenlidir. Db de bu değeride tutuyoruz. Hash çözerken bu değer lazım olacak.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//password parametresini byte[] halinde göndermemizi istiyor ComputeHash metodu byte[] haline getirmek için yazdık bu kodu.
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)//Doğrula şifre Hash'ini demek.Buradaki password kullanıcı kyıt olduktan sonra sisteme giriş yaparken girdiği şifre ve bu metot veri tabanında sorularken passwordHash vew passwordSalt alıp karşılaştıracak doğruysa true dönderecek.
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))//parametre olarak passwordSalt verilir çözümleme yapacağız.
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//Burada girilen şifrenin hash'ini alıyoruz.
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])//gelen şifreye göre oluşturulan hash ile db deki hash aynı değilse false döner.
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
