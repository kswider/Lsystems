using System;
using NCalc;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Equation
    {
        private String equation;

        public Equation(String pattern)
        {
            equation = pattern;
        }


        public Double apply(List<Double> t)
        {
            String replacedPattern = equation;
            int i = 0;
            foreach (Double param in t)
            {
                String strParam = "t" + i.ToString();
                replacedPattern = replacedPattern.Replace(strParam, param.ToString());
                i++;
            }
            Expression e = new Expression(replacedPattern);
            return (Double)e.Evaluate();
        }
    }