using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Repetite.Behaviours.IO
{
    public class ReadTextFileBehaviour : IBehaviour
    {
        public string Id => "IO.ReadTextFile";
        public string Name => "Read Text File";

        public Input[] Inputs => new[]
        {
            DefaultInputs.String("Path")
        };

        public Output[] Outputs => new[]
        {
            new Output(OutputType.String, "Full Path"),
            new Output(OutputType.String, "Filename"),
            new Output(OutputType.String, "Extension"),
            new Output(OutputType.String, "Contents")
        };

        public IValueBag Execute(IValueBag inputs)
        {
            if (!inputs.TryGet("Path", out string path))
                throw new MissingInputException("Path");

            var fullPath = Path.GetFullPath(path);
            var filename = Path.GetFileName(path);
            var extension = Path.GetExtension(path);

            var contents = File.ReadAllText(fullPath);

            return new BasicValueBag()
            {
                {"Full Path", fullPath},
                {"Filename", filename},
                {"Extension", extension},
                {"Contents", contents}
            };
        }

        public T Default<T>(string value)
        {
            var input = Inputs.FirstOrDefault(i => i.Name == value)
                        ?? throw new KeyNotFoundException();

            return (T) input.DefaultValue;
        }
    }
}