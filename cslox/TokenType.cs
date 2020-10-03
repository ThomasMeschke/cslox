namespace cslox
{
    internal enum TokenType
    {
        // single character tokens
        LeftParen,
        RightParen,
        LeftBrace,
        RightBrace,
        Comma,
        Dot,
        Minus,
        Plus,
        Semicolon,
        Slash,
        Star,

        // one or two character tokens
        Bang,
        BangEqual,
        Equal,
        EqualEqual,
        Greater,
        GreaterEqual,
        Less,
        LessEqual,

        // literals
        Identifier,
        String,
        Number,

        //Keywords
        And,
        Or,
        Var,
        Fun,
        Class,
        If,
        Else,
        True,
        False,
        For,
        While,
        Return,
        Nil,
        Print,
        Super,
        This,

        EOF
    }
}
