using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Parse
{
    public struct Token
    {
        public static bool IsSplitChar(char c)
        {
            switch (c)
            {
                case ' ':
                case '+':
                case '-':
                case '*':
                case '/':
                case '%':
                case '(':
                case ')':
                case ',':
                    return true;
            }
            return false;
        }

        public TokenType Type;
        public int Level; //Level of operator
        public string Value;

        public Token(TokenType type, string val)
        {
            Type = type;
            Value = val;
            Level = 1;
            if (type == TokenType.PLUS || type == TokenType.MINUS)
                Level = 2;
            if (type == TokenType.ASTERISK || type == TokenType.DIVIDE || type == TokenType.PERCENT)
                Level = 3;
            if (type == TokenType.CARET)
                Level = 4;
            if (type == TokenType.FUNCTION)
                Level = 5;
        }
    }

    public enum TokenType
    {
        NONE,
        NUMBER,
        FUNCTION,
        PLUS, // +
        MINUS, // -
        ASTERISK, // *
        DIVIDE, // /
        PERCENT, // %
        CARET, // ^
        LPAREN, // (
        RPAREN, // )
        COMMA, // ,
        ERROR
    }
}
