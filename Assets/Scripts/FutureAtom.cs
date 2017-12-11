using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class FutureAtom
    {
        public Char Letter { get; }
        public List<Equation> Equations { get; }

        public FutureAtom(char letter, List<Equation> equations)
        {
            Letter = letter;
            Equations = equations;
        }

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
