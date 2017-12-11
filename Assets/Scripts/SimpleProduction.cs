using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class SimpleProduction
    {
        public List<FutureAtom> After;
        public Double Probability { get; }

        public SimpleProduction(List<FutureAtom> after, Double probability)
        {
            After = after;
            Probability = probability;
        }
    }