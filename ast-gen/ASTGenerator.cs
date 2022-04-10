using System;
using System.IO;
using System.Collections.Generic;

namespace ast_gen
{
    class ASTGenerator
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: ast-gen <output directory>");
                Environment.Exit(64);
            }
            string outputDir = args[0];
            DefineAST(outputDir, "Expression", new List<string>(){
                "Binary     : Expression left, Token token, Expression right",
                "Grouping   : Expression Expressionession",
                "Literal    : object value",
                "Unary      : Token token, Expression right"
            });
        }

        private static void DefineAST(string outputDir, string baseName, List<string> types)
        {
            string path = Path.Combine(outputDir, baseName + ".cs");
            StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Create), System.Text.Encoding.UTF8);

            writer.WriteLine("namespace cslox");
            writer.WriteLine("{");
            writer.WriteLine("    public abstract class " + baseName);
            writer.WriteLine("    {");

            foreach(string type in types)
            {
                string className = type.Split(":")[0].Trim();
                string fieldList = type.Split(":")[1].Trim();
                DefineType(writer, baseName, className, fieldList);
            }

            writer.WriteLine("    }");
            writer.WriteLine("}");

            writer.Close();
        }

        private static void DefineType(StreamWriter writer, string baseName, string className, string fieldList)
        {
            writer.WriteLine("        public class " + className + " : " + baseName);
            writer.WriteLine("        {");

            // Constructor.
            writer.WriteLine("            public " + className + "(" + fieldList + ")");
            writer.WriteLine("            {");

            // Store parameters in fields.
            string[] fields = fieldList.Split(new string[]{", "}, StringSplitOptions.None);
            foreach(string field in fields)
            {
                string name = field.Split(" ")[1];
                writer.WriteLine("                this." + name.ToUpperFirst() + " = " + name + ";");
            }
            writer.WriteLine("            }");

            // Fields. 
            writer.WriteLine();
            foreach(string field in fields)
            {
                string type = field.Split(" ")[0];
                string name = field.Split(" ")[1];
                writer.WriteLine("        public readonly " + type + " " + name.ToUpperFirst() + ";");
            }

            writer.WriteLine("        }");
        }
    }

    internal static class StringExtensions
    {
        public static string ToUpperFirst(this string value)
        {
            return value[0].ToString().ToUpper() + value.Substring(1);
        }
    }
}
