using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Simple class representing atom in l-system state
/// </summary>
[Serializable]
    class Atom
    {
    [UnityEngine.SerializeField]
    private char letter;

    public char GetLetter()
    {
        return letter;
    }

    [UnityEngine.SerializeField]
    private List<double> parameters;

    public List<double> GetParameters()
    {
        return parameters;
    }

    public void SetParameters(List<double> value)
    {
        parameters = value;
    }

    /// <summary>
    /// Generates Atom from String with proper pattern
    /// </summary>
    /// <param name="atomStr">String representation of atom e.g. A(12,3.4,5)</param>
    /// <returns>Result Atom or null if pattern doesnt match the string</returns>
    public static Atom GenerateAtomFromString(String atomStr)
    {
        String pattern = @"^[A-Z](\([0-9]+(\.[0-9]+)?(,[0-9]+(\.[0-9]*)?)*\))?$";
        Regex regex = new Regex(pattern, RegexOptions.None);
        Match m = regex.Match(atomStr);

        if (m.Success)
        {
            Group g1 = m.Groups[0];
            Group g2 = m.Groups[1];

            //Atom letter
            CaptureCollection cc1 = g1.Captures;
            Capture letter = cc1[0];

            //Atom args
            List<Double> nParams = new List<Double>();
            CaptureCollection cc2 = g2.Captures;

            Capture c2 = cc2[0];

            String pattern2 = @"[0-9]+(\.[0-9]+)?";
            Regex regex2 = new Regex(pattern2, RegexOptions.IgnoreCase);
            Match m2 = regex2.Match(c2.ToString());
            while (m2.Success)
            {
                Group g3 = m2.Groups[0];
                CaptureCollection cc3 = g3.Captures;
                for (int l = 0; l < cc3.Count; l++)
                {
                    Capture c3 = cc3[l];
                    nParams.Add(Convert.ToDouble(c3.ToString()));
                }

                m2 = m2.NextMatch();
            }
            return new Atom(letter.ToString()[0], nParams);
        }
        else return null;
    }


    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="letter">Letter used to define atom</param>
    /// <param name="parameters">List of arguments applied to atom</param>
    public Atom(char letter, List<double> parameters)
        {
        this.letter = letter;
        SetParameters(parameters);
        }

        /// <summary>
        /// Method that changes atom according to given producton
        /// </summary>
        /// <param name="p">Production which will be applied to atom. It' important to check if atom passesGuard of Production before applying production to atom</param>
        /// <returns>Atoms created by the Production p</returns>
        public List<Atom> apply(Production p)
        {
            return p.getAfter(GetParameters());
        }
    }
