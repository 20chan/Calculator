using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Parse
{
    public class ExprNode
    {
        public Token Tok { get; private set; }
        public ExprNode Left { get; private set; }
        public ExprNode Right { get; private set; }

        public bool IsLast { get { return Left == null; } }

        public ExprNode(Token tok, ExprNode left = null, ExprNode right = null)
        {
            Tok = tok;
            Left = left;
            Right = right;
        }

        public void Print()
        {
            if(IsLast)
            {
                Console.Write(Tok.Value);
            }
            else
            {
                Left.Print();
                Console.Write(Tok.Value);
                Right.Print();
            }
        }

        public double Calc()
        {
            if (Tok.Type == TokenType.NUMBER)
                return Convert.ToDouble(Tok.Value);
            else if (Tok.Type == TokenType.PLUS)
                return Right.Calc() + Left.Calc();
            else if (Tok.Type == TokenType.MINUS)
                return Right.Calc() - Left.Calc();
            else if (Tok.Type == TokenType.ASTERISK)
                return Right.Calc() * Left.Calc();
            else if (Tok.Type == TokenType.DIVIDE)
                return Right.Calc() / Left.Calc();
            else if (Tok.Type == TokenType.PERCENT)
                return Right.Calc() % Left.Calc();
            else if (Tok.Type == TokenType.CARET)
                return Math.Pow(Right.Calc(), Left.Calc());
            throw new Exception();
        }
    }
}
