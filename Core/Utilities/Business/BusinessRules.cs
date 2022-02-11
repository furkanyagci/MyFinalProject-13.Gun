using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules //Javada class içinde static bir metod varsa classında static işaretlenmesi gerekir.Ama C# da ister static ister static yapmadan kullanabiliriz.
    {
        public static IResult Run(params IResult[] logics)//params sayesinde Run metodu içerisine virgülle ayırarak istediğiniz kadar IResult tipinde iş kuralı verebiliyorsunuz. logics iş kuralı demek.
        {
            foreach (var logic in logics)//bütün kuralları gez
            {
                if (!logic.Success)//kurala uymayan varsa
                {
                    return logic;//uymayan kuralı döndürür.
                }
            }
            return null;
        }
    }
}
