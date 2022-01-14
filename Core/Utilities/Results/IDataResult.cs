using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult //T : Generic hangi tip isteniyorsa o yazılır.
    {
        T Data { get; }
    }
}
