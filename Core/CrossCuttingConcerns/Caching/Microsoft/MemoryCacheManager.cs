using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{//Microsoft'un kendi Cache sistemini kullanıyoruz burada
    class MemoryCacheManager : ICacheManager
    {
        //Adapter Pattern : Adaptasyon patternı.

        IMemoryCache _memoryCache; //using Microsoft.Extensions.Caching.Memory; bu kütüphaneden geliyor IMemoryCache interface'i

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>(); // CoreModule.cs de serviceCollection.AddMemoryCache(); yazarak instance yapılan değeri bu şekilde atamasını yapmış oluyoruz. Buradaki hata VisualStudio düzeltemedi elle using Microsoft.Extensions.DependencyInjection; ekleyince düzeldi.
        }


        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));//duration ne zaman biteceği  TimeSpan.FromMinutes(duration) ise duration parametresi ile verilen kadar dk demek.
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);//out _  : bellekte sadece böyle bir değer varmı yokmu onu bulmak istiyorum out parametresi ile değer almak istemiyorum demektir.
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);//bellekte Cache ile ilgili yapıyı çekiyor. Bu kodun açıklamsı için 15.Gün videosunda 2:02:00 dk den sonrasını izle
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
