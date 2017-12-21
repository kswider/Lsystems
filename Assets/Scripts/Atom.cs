using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
