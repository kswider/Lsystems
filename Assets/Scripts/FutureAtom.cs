using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// <summary>
    /// Something that may become Atom
    /// </summary>
    class FutureAtom
    {
        public Char Letter { get; }
        public List<Equation> Equations { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="letter">Letter used to define atom.</param>
    /// <param name="equations">List of equations. According to them we can evaluate new Atom.</param>
    public FutureAtom(char letter, List<Equation> equations)
        {
            Letter = letter;
            Equations = equations;
        }

        /// <summary>
        /// Method evaluating new Atom
        /// </summary>
        /// <param name="args">Arguments that will be passed to Equations to evaluate new Atom parameters</param>
        /// <returns>created Atom</returns>
        public Atom evaluate(List<Double> args)
        {
            List<Double> nParameters = new List<double>();
            for (int i = 0; i < Equations.Count(); i++)
            {
                nParameters.Add(Equations[i].apply(args));
            }

            return new Atom(this.Letter, nParameters);
        }
    }
