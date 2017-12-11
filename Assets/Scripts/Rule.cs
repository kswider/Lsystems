using System;
using NCalc;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Rule
    {
        private String rule;

        public Rule(String pattern)
        {
            rule = pattern;
        }

        public Boolean apply(List<Double> t)
        {
            String replacedPattern = rule;
            int i = 0;
            foreach (Double param in t)
            {
                String strParam = "t" + i.ToString();
                replacedPattern = replacedPattern.Replace(strParam, param.ToString());
                i++;
            }
            Expression e = new Expression(replacedPattern);
            return (Boolean) e.Evaluate();
        }
    }
