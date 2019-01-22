using System.Collections;
using System.Collections.Generic;

namespace Repetite
{
    public class BasicValueBag : IValueBag, IEnumerable<KeyValuePair<string, object>>
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        public void Add(string key, object givenValue)
        {
            _values.Add(key, givenValue);
        }
        
        public void AddOrUpdate(string key, object givenValue)
        {
            _values[key] = givenValue;
        }

        public bool TryGet<T>(string key, out T result)
        {
            if (!_values.ContainsKey(key))
            {
                result = default(T);
                return false;
            }

            var value = _values[key];
            if (!(value is T))
            {
                result = default(T);
                return false;
            }

            result = (T) value;
            return true;
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _values.GetEnumerator();
        }
    }
}