﻿using System.Text;
using TypeCobol.Compiler.Scanner;

namespace TypeCobol.Compiler.CodeElements.Expressions
{
    
    public abstract class Expression
    {
        public abstract string TextValue();
    }
    public abstract class ArithmeticExpression : Expression { }

    public class Addition : ArithmeticExpression
    {
        public Expression left { get; set; }
        public Expression right { get; set; }

        public Addition(Expression left, Expression right)
        {
            this.left = left;
            this.right = right;
        }

        public override string TextValue()
        {
            return new StringBuilder(left.TextValue()).Append(" ").Append(right.TextValue()).ToString();
        }

        public override string ToString() { //RPN
            return left+" "+right+" +";
        }
    }
    public class Identifier : Expression
    {
        public Token token { get; set; }
        public bool rounded { get; set; }
        public Identifier(Token token, bool rounded = false)
        {
            this.token = token;
            this.rounded = rounded;
        }

        public override string TextValue()
        {
            if (token == null)
            {
                return base.ToString();
            }
            else
            {
                return token.Text;
            }
        }

        public override string ToString()
        {
            return TextValue();
        }

    }
    public class Number : ArithmeticExpression
    {
        public SyntaxNumber number { get; set; }
        public Number(SyntaxNumber number)
        {
            this.number = number;
        }

        public override string TextValue()
        {
            if (number == null)
            {
                return base.ToString();
            }
            else
            {
                return number.ToString();
            }
        }

        public override string ToString()
        {
            return TextValue();
        }


    }


    //TODO this class is only use for display literals for now.
    //osmedile: I'm not sure if literal must inherits from Expression (so Literal and identifier can be seen as the same object) 
    //if this class can inherits from Expression, rename it to "Literal"
    public class LiteralForDisplay : Expression
    {
        public Token token { get; set; }
        public LiteralForDisplay(Token token)
        {
            this.token = token;
        }
        public override string ToString() { return  TextValue();}

        public override string TextValue()
        {
            return token.Text; 
        }
    }
}