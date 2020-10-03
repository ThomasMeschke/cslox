using cslox.ErrorHandling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cslox
{
    class CsLox
    {
        private static bool errorOccured = false;
        public static void Main(string[] args)
        {
            if(args.Length > 1)
            {
                Console.Error.WriteLine("Usage: cslox.exe [script]");
                Environment.Exit(ExitCode.USED_INCORRECTLY);
            } 
            else if(args.Length == 1)
            {
                RunFile(args[0]);
            }
            else
            {
                RunPrompt();
            }
        }

        private static void RunFile(string filePath)
        {
            byte[] fileContentBytes = File.ReadAllBytes(filePath);
            string fileContentString = Encoding.UTF8.GetString(fileContentBytes);
            Run(fileContentString);
            
            if (errorOccured) 
            {
                Environment.Exit(ExitCode.DATA_ERROR);
            }
        }

        private static void RunPrompt()
        {
            for(; ; )
            {
                Console.Write("> ");
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }
                Run(line);
                errorOccured = false;
            }
        }

        private static void Run(string source)
        {
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.ScanTokens();

            foreach (Token token in tokens)
            {
                //Just print out tokens for now
                Console.WriteLine(token);
            }
        }

        public static void Error(int line, string message)
        {
            Report(line, string.Empty, message);
        }

        private static void Report(int line, string where, string message)
        {
            Console.Error.WriteLine($"[Line {line}] Error {where}: {message}");
            errorOccured = true;
        }
    }
}
