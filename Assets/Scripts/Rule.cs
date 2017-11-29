using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Rule
    {
        public char before { get; set; }
        public string after { get; set; }
        public Rule(char before, string after)
        {
            this.before = before;
            this.after = after;
        }
    }
}
