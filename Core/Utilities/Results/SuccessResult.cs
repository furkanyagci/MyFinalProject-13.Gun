using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult:Result
    {
        public SuccessResult(string message):base(true,message) // Base demek Result Demek burada
        {

        }

        public SuccessResult():base(true)//Default olarak true vermiş olduk.
        {

        }
    }
}
