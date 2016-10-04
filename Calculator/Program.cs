using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                string code = Console.ReadLine();
                Console.WriteLine(string.Join(" ", Parse.Parser.Separate(code)
                    .Select(t => t.Value)));
                Console.WriteLine(string.Join(" ", Parse.Parser.ToPostFix(Parse.Parser.Separate(code))
                     .Select(t => t.Value)));
            }
        }
    }
}
