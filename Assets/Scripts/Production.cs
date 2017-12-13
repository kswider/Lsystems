using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// <summary>
    /// Class representing production with features such as probability, guard and parameters.
    /// </summary>
    class Production
    {
        public List<Rule> Guards { get; }
        public Char Before { get; }
        public List<SimpleProduction> After { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="guards">Defines rules when atom can or can not be applied to production</param>
        /// <param name="before">Defines letter by which we recognize atoms that can be applied to production</param>
        /// <param name="after">Defines list of possible results of productions. Sum of probabilities in this lists must be equal to 1.0</param>
        public Production(List<Rule> guards, char before, List<SimpleProduction> after)
        {
            Guards = guards;
            Before = before;
            After = after;
        }


        
        /// <summary>
        /// Method returning effect of production
        /// </summary>
        /// <param name="parameters">parameters of atom applied to production</param>
        /// <returns>List of Atoms produced by this porduction</returns>
        public List<Atom> getAfter(List<Double> parameters)
        {
            Random random = new Random();
            Double randDouble = random.NextDouble();
            Double acc = 0;
            List<Double> nParameters = new List<Double>();
            List<Atom> ret = new List<Atom>();

            for (int i=0; i < After.Count; i++)
            {
                acc += After[i].Probability;
                if(randDouble <= acc)
                {
                    foreach(FutureAtom nAtom in After[i].After)
                    {
                        ret.Add(nAtom.evaluate(parameters));
                    }
                    return ret;
                }
            }
            return ret;
        }

        /// <summary>
        /// Method for checking if Atom can be applied to production
        /// </summary>
        /// <param name="atom">Atom to check</param>
        /// <returns>true if atom can be applied to production, false otherwise</returns>
        public Boolean passesGuards(Atom atom)
        {
            Boolean isOK = false;

            for(int i=0; i < Guards.Count(); i++)
            {
                if (atom.Letter == Before && Guards[i].apply(atom.Parameters))
                {
                    isOK = true;
                }
            }
            return isOK;
        }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    