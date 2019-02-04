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
        public IBehaviour Get(string id)
        {
            return All.First(b => b.Id == id);
        }

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