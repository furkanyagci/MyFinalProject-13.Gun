using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{//Results klasörü içinde Abstract ve Concrete klasörlemesi yapılabilir overdesign diye birşey var aşırı tasarım Burası sadece Result old. için gerek yok
    public class Result : IResult
    {
        
        public Result(bool success, string message):this(success)//:this(success) Result'ın tek parametreli olan Constructer'ına success yolla demek bu kodla aşağıdaki Constructer'da çalışır.2 parametreli gönderirsen otomatik olacak aşağıdaki Constructer'da çalışır.
        {
            Message = message;//Message sadece get idi ama burada set ettik.get ReadOnly'dir Readonly'ler Constructor'da set edilebilir.Constructer dışında set etmeyeceğimiz için set yazmadık aşağıda. *** NOT: Sektörde eksik kalan bir konudur.
            //Success = success; bunu sildi hoca aşağıdaki Constructer'ıda çalıştırdı
        }

        //Hem yukarıdaki Constructor da hemde aşağıdakinde Success = success; yazarak Dont Repeat Yourself(kendimi tekrar etme) kuralını ihlal etmiş oldum. 
        public Result(bool success)//Sadece True dönsün mesaj istemioyrum derseniz Constructor Overload edebiliriz.
        {
            Success = success;
        }

        public bool Success { get; }//IResult implement edince => throw new NotImplementedException(); böyle gelir silip get yazdık

        public string Message { get; }
    }
}
