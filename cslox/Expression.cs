namespace cslox
{
    public abstract class Expression
    {
        public class Binary : Expression
        {
            public Binary(Expression left, Token token, Expression right)
            {
                this.Left = left;
                this.Token = token;
                this.Right = right;
            }

        public readonly Expression Left;
        public readonly Token Token;
        public readonly Expression Right;
        }
        public class Grouping : Expression
        {
            public Grouping(Expression Expressionession)
            {
                this.Expressionession = Expressionession;
            }

        public readonly Expression Expressionession;
        }
        public class Literal : Expression
        {
            public Literal(object value)
            {
                this.Value = value;
            }

        public readonly object Value;
        }
        public class Unary : Expression
        {
            public Unary(Token token, Expression right)
            {
                this.Token = token;
                this.Right = right;
            }

        public readonly Token Token;
        public readonly Expression Right;
        }
    }
}
