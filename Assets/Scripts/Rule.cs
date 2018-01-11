using System;
using NCalc;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// <summary>
    /// Class representing a rule which can be evaluated to boolean value
    /// </summary>
    [Serializable]
    public class Rule
    {

    [UnityEngine.SerializeField]
    private String rule;

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="pattern">Pattern representing rule. Result of equation must be double. Variables must be represented by t0,t1...t9 </param>
    public Rule(String pattern)
        {
            rule = pattern;
        }

    /// <summary>
    /// Method that applies variables from list to rule and evaluates its value
    /// </summary>
    /// <param name="t">List of parameters. Position in list matches parameter number. Its imoprtant to keep list length equal to largest parameter number (e.g. t9 -> List.Count() == 9 )</param>
    /// <returns>Evaluated boolean value</returns>
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
