namespace cslox
{
    internal abstract class Expression
    {
        internal class Binary : Expression
        {
            private readonly Expression leftHandSide;
            private readonly Token operatorToken;
            private readonly Expression rightHandSide;

            public Binary(Expression leftHandSide, Token operatorToken, Expression rightHandSide)
            {
                this.leftHandSide = leftHandSide;
                this.operatorToken = operatorToken;
                this.rightHandSide = rightHandSide;
            }
        }

        internal class Grouping : Expression
        {
            private readonly Expression expression;

            public Grouping(Expression expression)
            {
                this.expression = expression;
            }
        }

        internal class Literal : Expression
        {
            private readonly object value;

            public Literal(object value)
            {
                this.value = value;
            }
        }

        internal class Unary : Expression
        {
            private readonly Token operatorToken;
            private readonly Expression rightHandSide;

            public Unary(Token operatorToken, Expression rightHandSide)
            {
                this.operatorToken = operatorToken;
                this.rightHandSide = rightHandSide;
            }
        }

    }
}
