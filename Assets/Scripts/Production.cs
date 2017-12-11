using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Production
    {
        public List<Rule> Guards { get; }
        public Char Before { get; }
        public List<SimpleProduction> After { get; }

        public Production(List<Rule> guards, char before, List<SimpleProduction> after)
        {
            Guards = guards;
            Before = before;
            After = after;
        }

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
