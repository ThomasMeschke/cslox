namespace cslox
{
    internal abstract class Expression
    {
        protected abstract TEntity Accept<TEntity>(Visitor<TEntity> visitor);

        internal interface Visitor<TEntity>
        {
            TEntity VisitBinaryExpression(Binary expression);
            TEntity VisitGroupingExpression(Grouping expression);
            TEntity VisitLiteralExpression(Literal expression);
            TEntity VisitUnaryExpression(Unary expression);
        }
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

            protected override TEntity Accept<TEntity>(Visitor<TEntity> visitor)
            {
                return visitor.VisitBinaryExpression(this);
            }
        }

        internal class Grouping : Expression
        {
            private readonly Expression expression;

            public Grouping(Expression expression)
            {
                this.expression = expression;
            }

            protected override TEntity Accept<TEntity>(Visitor<TEntity> visitor)
            {
                return visitor.VisitGroupingExpression(this);
            }
        }

        internal class Literal : Expression
        {
            private readonly object value;

            public Literal(object value)
            {
                this.value = value;
            }

            protected override TEntity Accept<TEntity>(Visitor<TEntity> visitor)
            {
                return visitor.VisitLiteralExpression(this);
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

            protected override TEntity Accept<TEntity>(Visitor<TEntity> visitor)
            {
                return visitor.VisitUnaryExpression(this);
            }
        }

    }
}
