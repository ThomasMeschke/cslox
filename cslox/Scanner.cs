using System;
using System.Collections.Generic;

namespace cslox
{
    internal class Scanner
    {
        private readonly string source;
        private readonly List<Token> tokens = new List<Token>();

        private int start = 0;
        private int current = 0;
        private int line = 1;

        public Scanner(string source)
        {
            this.source = source;
        }

        public List<Token> ScanTokens()
        {
            while (!IsAtEnd())
            {
                start = current;
                ScanToken();
            }

            tokens.Add(new Token(TokenType.EOF, string.Empty, null, line));
            return tokens;
        }

        private void ScanToken()
        {
            char c = Advance();
            switch (c)
            {
                case '(': AddToken(TokenType.LeftParen); break;
                case ')': AddToken(TokenType.RightParen); break;
                case '{': AddToken(TokenType.LeftBrace); break;
                case '}': AddToken(TokenType.RightBrace); break;
                case ',': AddToken(TokenType.Comma); break;
                case '.': AddToken(TokenType.Dot); break;
                case '-': AddToken(TokenType.Minus); break;
                case '+': AddToken(TokenType.Plus); break;
                case ';': AddToken(TokenType.Semicolon); break;
                case '*': AddToken(TokenType.Star); break;

                case '!': AddToken(FollowedBy('=') ? TokenType.BangEqual : TokenType.Bang); break;
                case '=': AddToken(FollowedBy('=') ? TokenType.EqualEqual : TokenType.Equal); break;
                case '<': AddToken(FollowedBy('=') ? TokenType.LessEqual : TokenType.Less); break;
                case '>': AddToken(FollowedBy('=') ? TokenType.GreaterEqual : TokenType.Greater); break;

                default:
                    CsLox.Error(line, $"Unexpected character '{c}'.");
                    break;
            }
        }

        private char Advance()
        {
            current++;
            return source[current - 1];
        }

        private bool FollowedBy(char expected)
        {
            if (IsAtEnd()) return false;
            if (source[current] != expected) return false;

            current++;
            return true;
        }

        private void AddToken(TokenType type, object literal = null)
        {
            string text = source.Substring(start, current);
            tokens.Add(new Token(type, text, literal, line));
        }

        private bool IsAtEnd()
        {
            return current >= source.Length;
        }
    }
}