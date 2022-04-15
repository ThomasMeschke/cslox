namespace cslox
{
    public abstract class Expression
    {
        public interface IVisitor<T>
        {
            T visitBinaryExpression(Binary expression);
            T visitGroupingExpression(Grouping expression);
            T visitLiteralExpression(Literal expression);
            T visitUnaryExpression(Unary expression);
        }

        public class Binary : Expression
        {
            public Binary(Expression left, Token token, Expression right)
            {
                this.Left = left;
                this.Token = token;
                this.Right = right;
            }

            public override T accept<T>(IVisitor<T> visitor)
            {
                return visitor.visitBinaryExpression(this);
            }

        public readonly Expression Left;
        public readonly Token Token;
        public readonly Expression Right;
        }
        public class Grouping : Expression
        {
            public Grouping(Expression Expression)
            {
                this.Expression = Expression;
            }

            public override T accept<T>(IVisitor<T> visitor)
            {
                return visitor.visitGroupingExpression(this);
            }

        public readonly Expression Expression;
        }
        public class Literal : Expression
        {
            public Literal(object value)
            {
                this.Value = value;
            }

            public override T accept<T>(IVisitor<T> visitor)
            {
                return visitor.visitLiteralExpression(this);
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

            public override T accept<T>(IVisitor<T> visitor)
            {
                return visitor.visitUnaryExpression(this);
            }

        public readonly Token Token;
        public readonly Expression Right;
        }

        public abstract T accept<T>(IVisitor<T> visitor);
    }
}
