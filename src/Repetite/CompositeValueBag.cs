using System.Collections.Generic;

namespace Repetite
{
    public class CompositeValueBag : IValueBag
    {
        private readonly IEnumerable<IValueBag> _bags;

        public CompositeValueBag(params IValueBag[] bags)
        {
            _bags = bags;
        }
        
        public CompositeValueBag(IEnumerable<IValueBag> bags)
        {
            _bags = bags;
        }

        public bool TryGet<T>(string key, out T result)
        {
            foreach (var bag in _bags)
            {
                if (bag.TryGet(key, out result))
                {
                    return true;
                }
            }

            result = default(T);
            return false;
        }
    }
}