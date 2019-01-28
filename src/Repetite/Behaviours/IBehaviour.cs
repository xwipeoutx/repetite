using System;
using System.Linq;

namespace Repetite
{
    public interface IBehaviour
    {
        string Id { get; }
        string Name { get; }
        
        Input[] Inputs { get; }
        Output[] Outputs { get; }
        IValueBag Execute(IValueBag inputs);
    }

    public class BehaviourStore 
    {
        public IBehaviour[] All 
        {
            get
            {
                return GetType().Assembly.ExportedTypes
                    .Where(t => t.GetInterfaces().Contains(typeof(IBehaviour)))
                    .Select(Activator.CreateInstance)
                    .Cast<IBehaviour>()
                    .ToArray();
            }
        }
    
    }
}