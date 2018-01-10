using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class OldRule
    {
        public char before { get; set; }
        public string after { get; set; }
        public OldRule(char before, string after)
        {
            this.before = before;
            this.after = after;
        }
}
