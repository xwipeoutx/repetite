using System;
using System.Collections.Generic;
using System.Linq;

namespace Repetite
{
    public class Node
    {
        public readonly IBehaviour Behaviour;
        private BasicValueBag _valueBag;

        public Node(IBehaviour behaviour)
        {
            Behaviour = behaviour;
            _valueBag = new BasicValueBag();
            
            foreach (var input in Behaviour.Inputs)
            {
                _valueBag.Add(input.Name, input.DefaultValue);
            }
        }

        public Input Input(string key)
        {
            return Behaviour.Inputs.FirstOrDefault(i => i.Name == key)
                   ?? throw new KeyNotFoundException();
        }

        public Output Output(string key)
        {
            return Behaviour.Outputs.FirstOrDefault(i => i.Name == key)
                   ?? throw new KeyNotFoundException();
        }

        public bool TryGetValue<T>(string key, out T result)
        {
            var input = Input(key);
            if (_valueBag.TryGet(key, out T value))
            {
                result = value;
                return true;
            }

            result = (T) input.DefaultValue;
            return true;
        }

        public void SetValue<T>(string key, T value)
        {
            var input = Input(key);
            if (!input.CanReceive(value))
            {
                throw new ArgumentException();
            }

            _valueBag.AddOrUpdate(key, value);
        }

        public IValueBag Execute(IValueBag externalValues)
        {
            var compositeBag = new CompositeValueBag(externalValues, _valueBag);
            return Behaviour.Execute(compositeBag);
        }
    }
}