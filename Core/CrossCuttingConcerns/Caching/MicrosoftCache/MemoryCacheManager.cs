using Base.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Base.CrossCuttingConcerns.Caching.MicrosoftCache
{
    public class MemoryCacheManager : ICacheManager
    {
        private IMemoryCache _cache;
        private readonly List<string> _cacheKeys = new List<string>();  // Anahtar listesini yönetiyoruz.

        public MemoryCacheManager()
        {
            _cache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public void Add(string key, object value, int duration)
        {
            _cache.Set(key, value, TimeSpan.FromMinutes(duration));
            if (!_cacheKeys.Contains(key))
            {
                _cacheKeys.Add(key); // Anahtarları listeye ekliyoruz.
            }
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public bool isAdd(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
            _cacheKeys.Remove(key);  // Anahtar listesinden de siliyoruz.
        }

        public void RemoveByPattern(string pattern)
        {
            // Desenle eşleşen anahtarları bulmak için regex kullanıyoruz.
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = _cacheKeys.Where(k => regex.IsMatch(k)).ToList();

            // Bulunan anahtarları hem cache'den hem de anahtar listesinden siliyoruz.
            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
                _cacheKeys.Remove(key);
            }
        }
    }
}
