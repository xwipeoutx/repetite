using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Repetite.Behaviours.IO
{
    public class WriteTextFileBehaviour : IBehaviour
    {
        public string Id => "IO.WriteTextFile";
        public string Name => "Write Text File";

        public Input[] Inputs => new[]
        {
            DefaultInputs.String("Path"),
            DefaultInputs.String("Contents")
        };

        public Output[] Outputs => Array.Empty<Output>();

        public IValueBag Execute(IValueBag inputs)
        {
            if (!inputs.TryGet("Path", out string path))
                throw new MissingInputException("Path");
            
            if (!inputs.TryGet("Contents", out string contents))
                throw new MissingInputException("Contents");

            var fullPath = Path.GetFullPath(path);
            return new BasicValueBag();
        }

        public T Default<T>(string value)
        {
            var input = Inputs.FirstOrDefault(i => i.Name == value)
                        ?? throw new KeyNotFoundException();

            return (T) input.DefaultValue;
        }
    }
}