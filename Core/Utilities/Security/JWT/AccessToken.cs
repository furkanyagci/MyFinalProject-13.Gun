using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    // Kullanıcı sisteme istekte bulunurken eğer yetki gerektiren birşeyse elinde bir tane Token vardır o tokenıda paket içine koyup gönderir buna AccessToken denir buna erişin anahtarı deniyor.
    public class AccessToken
    {
        public string Token { get; set; }//JWT değerinin kendisi. Postman den kullanıcı adı ve parolsını verecek bizde ona Token vereceğiz. ne zaman sonlanacağınıda aşağıdaki propertyde vereceğiz.
        public DateTime Expiration { get; set; }// Tokenın bitiş zamanı
    }
}
