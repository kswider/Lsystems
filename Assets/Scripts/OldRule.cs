using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class OldRule
    {
        public char before { get; set; }
        public string after { get; set; }
        public OldRule(char before, string after)
        {
            this.before = before;
            this.after = after;
        }
    }
}
