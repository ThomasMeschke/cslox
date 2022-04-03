using System.Collections.Generic;

namespace cslox
{
    class Scanner
    {
        public readonly string Source;

        public Scanner(string source)
        {
            this.Source = source;
        }

        public List<Token> ScanTokens()
        {
            return new List<Token>();
        }
    }
}