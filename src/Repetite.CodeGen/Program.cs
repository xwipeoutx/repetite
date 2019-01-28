using System;
using System.IO;
using System.Linq;
using System.Reflection;
using EnumGenie.Sources;
using EnumGenie.TypeScript;
using EnumGenie.Writers;
using Typescriptr;
using Typescriptr.Formatters;
using Repetite.API;

namespace Repetite.CodeGen
{
    class Program
    {
        public static void Main()
        {
            var generator = TypeScriptGenerator.CreateDefault()
                .WithEnumFormatter(EnumFormatter.ValueNumberEnumFormatter, (type, style) => type.Name)
                .WithTypeMembers(MemberType.PropertiesAndFields);

            var typesToGenerate = 
                RepetiteApi.Assembly.ExportedTypes
                    .Where(type => type.Namespace.StartsWith("Repetite.API.Models"));

            var result = generator.Generate(typesToGenerate);
            
            using(var fs = File.Create("../Repetite.UI/src/models/types.d.ts"))
            using(var tw = new StreamWriter(fs)) {
                tw.Write(result.Enums.Replace("enum ", "declare enum ") + result.Types);
            }

            new EnumGenie.EnumGenie()
                .SourceFrom.Assembly(RepetiteApi.Assembly)
                .WriteTo.File("../Repetite.UI/src/models/enums.ts", cfg => cfg.TypeScript())
                .Write();
        }    }
}