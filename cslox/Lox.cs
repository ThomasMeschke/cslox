using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cslox
{
    class Lox
    {
        private static bool hadError = false;

        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: cslox [script]");
                Environment.Exit(ResultCode.USED_INCORRECT);
            } 
            else if (args.Length == 1) 
            {
                RunFile(args[0]);
            }
            else
            {
                RunPrompt();
            }
        }

        private static void RunFile(string path)
        {
            string fullpath = Path.GetFullPath(path);
            if (! File.Exists(fullpath))
            {
                Environment.Exit(ResultCode.IO_ERROR);
            }
            byte[] bytes = File.ReadAllBytes(fullpath);
            Run(Encoding.Default.GetString(bytes));
            if (hadError)
            {
                Environment.Exit(ResultCode.DATA_ERROR);
            }
        }

        private static void RunPrompt()
        {
            using(StreamReader sr = new StreamReader(Console.OpenStandardInput()))
            {
                for(;;)
                {
                    Console.Write("> ");
                    string line = sr.ReadLine();
                    if(string.IsNullOrEmpty(line)) break;
                    Run(line);
                    hadError = false;
                }
            }
        }

        private static void Run(string source)
        {
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.ScanTokens();

            foreach(Token token in tokens)
            {
                Console.WriteLine(token);
            }
        }

        private static void Error(int line, string error)
        {
            Report(line, "", error);
        }

        private static void Report(int line, string where, string message)
        {
            string output = string.Format("[line {0}] ERROR {1}: {2}", line, where, message);
            Console.Error.WriteLine(output);
            hadError = true;
        }
    }
}
