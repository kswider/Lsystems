using System;
using NCalc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Class used to evaluate values of equations given in string format
/// </summary>
[Serializable]
public class Equation
    {
        [SerializeField]
        public String equation;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="pattern">Pattern representing equation. Result of equation must be double. Variables must be represented by t0,t1...t9 </param>
        public Equation(String pattern)
        {
            equation = pattern;
        }

        /// <summary>
        /// Method that applies variables from list to equation and evaluates its value
        /// </summary>
        /// <param name="t">List of parameters. Position in list matches parameter number. Its imoprtant to keep list length equal to largest parameter number (e.g. t9 -> List.Count() == 9 )</param>
        /// <returns>Evaluated double value</returns>
        public Double apply(List<Double> t)
        {
            String replacedPattern = equation;
            int i = 0;
            foreach (Double param in t)
            {
                String strParam = "t" + i.ToString();
                replacedPattern = replacedPattern.Replace(strParam, String.Format("{0:0.000}", param));
                i++;
            }

            Debug.Log(replacedPattern);
            Expression e = new Expression(replacedPattern);
            return Convert.ToDouble(e.Evaluate()); 
        }
    }