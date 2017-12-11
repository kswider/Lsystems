using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Atom
    {
        public Char Letter { get; }
        public List<Double> Parameters { get; set; }
        

        public Atom(char letter, List<double> parameters)
        {
            Letter = letter;
            Parameters = parameters;
        }

        public List<FutureAtom> apply(Production p)
        {
            return p.getAfter(Parameters);
        }
    }
