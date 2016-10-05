using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Parse;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            VarNode.Vars.SetVar("x", 10);
            FunctionNode.Funcs.AddFunction("sin", x => Math.Sin(x[0]));
            while (true)
            {
                string code = Console.ReadLine();
                //Console.WriteLine(string.Join(" ", Parser.ToPostFix(Parser.Separate(code))
                //    .Select(t => t.Value)));
                
                var fix = Parser.BuildAST(Parser.Separate(code));
                Console.WriteLine(fix.Calc());
            }
        }
    }
}
