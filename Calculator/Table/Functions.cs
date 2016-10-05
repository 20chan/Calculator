using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Table
{
    public class Functions
    {
        private Dictionary<string, Func<double[], double>> _funcs;

        public Functions()
        {
            _funcs = new Dictionary<string, Func<double[], double>>();
        }

        public double Calc(string name, double[] args)
        {
            return _funcs[name](args);
        }

        public void AddFunction(string name, Func<double[], double> func)
        {
            _funcs.Add(name, func);
        }
    }
}
