using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>//DataResult'ın İşlem Başarılı mesajı ve datayı döndüren hali
    {//Bunlar versiyonlar. Bunlar kullanıcıya bir sürü imkan veriyorsun sadece Data verebilirsin istersen sadece mesaj verebilirsin vs.
        public SuccessDataResult(T data, string message) : base(data, true, message)
        {

        }

        public SuccessDataResult(T data) : base(data, true)
        {

        }

        public SuccessDataResult(string message) : base(default, true, message)//T'nin default hali örneğin int sen birşey döndürmek istemiyorsundur default halinde döndürmek isteyebilirsin.Bu ve aşağıdaki çok kullanılmaz.
        {

        }

        public SuccessDataResult() : base(default, true)
        {

        }


    }
}
