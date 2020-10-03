using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ast_gen
{
    class ASTGenerator
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: ast-gen <output_directory>");
                Environment.Exit(64);
            }
            string outputDir = args[0];
            DefineAST(outputDir, "Expression", new List<string>() {
                "Binary:Expression leftHandSide, Token operatorToken, Expression rightHandSide",
                "Grouping:Expression expression",
                "Literal:object value",
                "Unary:Token operatorToken, Expression rightHandSide"
            });
        }

        private static void DefineAST(string outputDir, string baseName, List<string> types)
        {
            string path = Path.Combine(outputDir, baseName + ".cs");
            using (StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Create, FileAccess.Write)))
            {
                writer.WriteLine("namespace cslox");
                writer.WriteLine("{");
                writer.WriteLine($"    internal abstract class {baseName}");
                writer.WriteLine("    {");

                foreach(string type in types)
                {
                    string className = type.Split(':').First().Trim();
                    string fields = type.Split(':').Last().Trim();
                    DefineType(writer, baseName, className, fields);
                }

                writer.WriteLine("    }");
                writer.WriteLine("}");
            }
        }

        private static void DefineType(StreamWriter writer, string baseName, string className, string fieldList)
        {
            writer.WriteLine($"        internal class {className} : {baseName}");
            writer.WriteLine("        {");

            string[] fields = fieldList.Split(',');
            foreach(string field in fields)
            {
                writer.WriteLine($"            private readonly {field.Trim()};");
            }

            writer.WriteLine();

            writer.WriteLine($"            public {className}({fieldList})");
            writer.WriteLine("            {");
            foreach(string field in fields)
            {
                string name = field.Split(' ').Last();
                writer.WriteLine($"                this.{name} = {name};");
            }
            writer.WriteLine("            }");
            writer.WriteLine("        }");
            writer.WriteLine();
        }
    }
}
