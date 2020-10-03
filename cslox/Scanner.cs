using System;
using System.Collections.Generic;

namespace cslox
{
    internal class Scanner
    {
        private readonly string source;

        public Scanner(string source)
        {
            this.source = source;
        }

        public List<Token> ScanTokens()
        {
            throw new NotImplementedException();
        }
    }
}