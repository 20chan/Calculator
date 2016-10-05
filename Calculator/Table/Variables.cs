using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Table
{
    public class Variables
    {
        private Dictionary<string, double> _vars;

        public Variables()
        {
            _vars = new Dictionary<string, double>();
        }

        public double GetVar(string name)
        {
            return _vars[name];
        }

        public void SetVar(string name, double value = 0)
        {
            if (_vars.ContainsKey(name))
                _vars[name] = value;
            else
                _vars.Add(name, value);
        }
    }
}
