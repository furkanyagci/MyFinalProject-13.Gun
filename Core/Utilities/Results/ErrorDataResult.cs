﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {//Bunlar versiyonlar. Bunlar kullanıcıya bir sürü imkan veriyorsun sadece Data verebilirsin istersen sadece mesaj verebilirsin vs.
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }

        public ErrorDataResult(T data) : base(data, false)
        {

        }

        public ErrorDataResult(string message) : base(default, false, message)//T'nin default hali örneğin int sen birşey döndürmek istemiyorsundur default halinde döndürmek isteyebilirsin.Bu ve aşağıdaki çok kullanılmaz.
        {

        }

        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
