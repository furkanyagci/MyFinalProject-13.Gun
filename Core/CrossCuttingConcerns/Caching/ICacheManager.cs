using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    //bu ICacheManager interface bütün alternatiflerimin interface'i olacak.
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value, int duration );//gelecek data bütün veri tipleri olabilir int, string vb. o yüzden value'yi object value olarak tanımladık.
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);

    }
}
