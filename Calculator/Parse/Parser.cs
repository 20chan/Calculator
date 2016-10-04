using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Parse
{
    public static class Parser
    {
        public static List<Token> Separate(string code)
        {
            TokenType stage = TokenType.NONE;
            int marker = 0;
            List<Token> result = new List<Token>();
            StringBuilder sb = new StringBuilder();

            for (int it = 0; it < code.Length; it++)
            {
                switch (stage)
                {
                    case TokenType.NONE:
                        {
                            switch (code[it])
                            {
                                case ' ':
                                    break;
                                case '0':
                                case '1':
                                case '2':
                                case '3':
                                case '4':
                                case '5':
                                case '6':
                                case '7':
                                case '8':
                                case '9':
                                case '.':
                                    {
                                        stage = TokenType.NUMBER;
                                        marker = it;

                                        if (it == code.Length - 1)
                                        {
                                            stage = TokenType.NONE;
                                            result.Add(new Token(TokenType.NUMBER, code[it].ToString()));
                                        }
                                        break;
                                    }
                                case '+':
                                    result.Add(new Token(TokenType.PLUS, "+"));
                                    break;
                                case '-':
                                    result.Add(new Token(TokenType.MINUS, "-"));
                                    break;
                                case '*':
                                    result.Add(new Token(TokenType.ASTERISK, "*"));
                                    break;
                                case '/':
                                    result.Add(new Token(TokenType.DIVIDE, "/"));
                                    break;
                                case '%':
                                    result.Add(new Token(TokenType.PERCENT, "%"));
                                    break;
                                case '(':
                                    result.Add(new Token(TokenType.LPAREN, "("));
                                    break;
                                case ')':
                                    result.Add(new Token(TokenType.RPAREN, ")"));
                                    break;
                                case ',':
                                    result.Add(new Token(TokenType.COMMA, ","));
                                    break;
                                default:
                                    stage = TokenType.FUNCTION;
                                    marker = it;
                                    break;
                            }
                            break;
                        }
                    case TokenType.NUMBER:
                        {
                            if (Token.IsSplitChar(code[it]))
                            {
                                stage = TokenType.NONE;
                                result.Add(new Token(TokenType.NUMBER, subString(code, marker, it--)));
                            }
                            else if (!char.IsDigit(code[it]) && code[it] != '.')
                            {
                                stage = TokenType.NONE;
                                result.Add(new Token(TokenType.ERROR, subString(code, marker, it--)));
                            }
                            else if (it == code.Length - 1)
                            {
                                stage = TokenType.NONE;
                                result.Add(new Token(TokenType.NUMBER, subString(code, marker, it + 1)));
                            }
                            break;
                        }
                    case TokenType.FUNCTION:
                        {
                            if (Token.IsSplitChar(code[it]))
                            {
                                stage = TokenType.NONE;
                                string cur = subString(code, marker, it--);
                                result.Add(new Token(TokenType.FUNCTION, cur));
                            }
                            break;
                        }
                }
            }

            return result;
        }

        public static List<Token> ToPostFix(List<Token> toks)
        {
            Queue<Token> output = new Queue<Token>();
            Stack<Token> ops = new Stack<Token>();
            for(int i = 0; i < toks.Count; i++)
            {
                Token cur = toks[i];
                
                switch(cur.Type)
                {
                    case TokenType.NUMBER:
                        {
                            output.Enqueue(cur);
                            break;
                        }
                    case TokenType.FUNCTION:
                        {
                            ops.Push(cur);
                            break;
                        }
                    case TokenType.COMMA:
                        {
                            while(ops.Peek().Type != TokenType.LPAREN)
                            {
                                output.Enqueue(ops.Pop());
                            }
                            break;
                        }
                    case TokenType.PLUS:
                    case TokenType.MINUS:
                    case TokenType.ASTERISK:
                    case TokenType.DIVIDE:
                    case TokenType.PERCENT:
                    case TokenType.CARET:
                        {
                            if (ops.Count == 0)
                            {
                                ops.Push(cur);
                                break;
                            }
                            while (ops.Count != 0)
                            {
                                if (cur.Type != TokenType.CARET && cur.Level <= ops.Peek().Level
                                    || cur.Type == TokenType.CARET && cur.Level < ops.Peek().Level)
                                    output.Enqueue(ops.Pop());
                                else
                                {
                                    ops.Push(cur);
                                    break;
                                }
                            }
                            break;
                        }
                    case TokenType.LPAREN:
                        {
                            ops.Push(cur);
                            break;
                        }
                    case TokenType.RPAREN:
                        {
                            while (ops.Peek().Type != TokenType.LPAREN)
                                output.Enqueue(ops.Pop());
                            ops.Pop();
                            break;
                        }
                }
            }

            while(ops.Count > 0)
            {
                if (ops.Peek().Type == TokenType.LPAREN || ops.Peek().Type == TokenType.RPAREN)
                    throw new Exception(); // Error
                output.Enqueue(ops.Pop());
            }

            return output.ToList();
        }
        
        private static string subString(string str, int mark, int cur)
        {
            return str.Substring(mark, cur - mark);
        }
    }
}
