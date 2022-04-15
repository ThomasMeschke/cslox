using System;
using System.Text;

namespace cslox;

public class ASTPrinter : Expression.IVisitor<string>
{
    public string Print(Expression expression)
    {
        return expression.accept(this);
    }
    
    public string visitBinaryExpression(Expression.Binary expression)
    {
        return Parenthesize(expression.Token.Lexeme, expression.Left, expression.Right);
    }

    public string visitGroupingExpression(Expression.Grouping expression)
    {
        return Parenthesize("group", expression.Expression);
    }

    public string visitLiteralExpression(Expression.Literal expression)
    {
        if (null == expression.Value) 
        {
            return "nil";
        }
        return expression.Value.ToString();
    }

    public string visitUnaryExpression(Expression.Unary expression)
    {
        return Parenthesize(expression.Token.Lexeme, expression.Right);
    }

    private string Parenthesize(string name, params Expression[] expressions)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("(").Append(name);
        foreach(Expression expression in expressions)
        {
            builder.Append(" ");
            builder.Append(expression.accept(this));
        }
        builder.Append(")");

        return builder.ToString();
    }
}