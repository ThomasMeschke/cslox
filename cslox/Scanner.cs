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

                case '/':
                    if (FollowedBy('/'))
                    {
                        // a comment goes until the end of the line
                        while (Peek() != '\n' && !IsAtEnd())
                        {
                            Advance();
                        }
                    }
                    else
                    {
                        AddToken(TokenType.Slash);
                    }
                    break;

                case ' ':
                case '\r':
                case '\t':
                    // ignore whitespaces
                    break;

                case '\n':
                    line++;
                    break;

                case '"': HandleString(); break;

                default:
                    if (IsDigit(c))
                    {
                        HandleNumber();
                    }
                    else
                    {
                        CsLox.Error(line, $"Unexpected character '{c}'.");
                    }
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

        private char Peek()
        {
            if (IsAtEnd()) return '\0';
            return source[current];
        }

        private char PeekNext()
        {
            if (current + 1 >= source.Length) return '\0';
            return source[current + 1];
        }

        private void HandleString()
        {
            while (Peek() != '"' && !IsAtEnd())
            {
                if (Peek() == '\n')
                {
                    line++;
                }
                Advance();
            }

            if (IsAtEnd())
            {
                // we've hit the end before we found the closing quotes
                CsLox.Error(line, "Unterminated string");
                return;
            }

            // consume the closing quotes
            Advance();

            // trim the surrounding quotes away
            string value = source.Substring(start + 1, current - 1);
            AddToken(TokenType.String, value);
        }

        private void HandleNumber()
        {
            while (IsDigit(Peek()))
            {
                Advance();
            }

            if (Peek() == '.' && IsDigit(PeekNext()))
            {
                // consume the dot as fraction separator
                Advance();

                while (IsDigit(Peek()))
                {
                    Advance();
                }
            }

            string numberString = source.Substring(start, current);
            AddToken(TokenType.Number, double.Parse(numberString));
        }

        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
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