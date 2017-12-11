using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Simulation
    {
        public List<Atom> currState;
        public List<Production> productions;
        public Dictionary<Char, FutureCommand> dictionary;

        public Simulation(List<Atom> currState, List<Production> productions, Dictionary<char, FutureCommand> dictionary)
        {
            this.currState = currState;
            this.productions = productions;
            this.dictionary = dictionary;
        }

        public void evaluate(int n)
        {
            List<Atom> nextState = new List<Atom>();
            for(int i = 0; i < n; i++)
            {
                for(int j = 0;j < currState.Count; j++)
                {
                    foreach(Production prod in productions)
                    {
                        if (prod.passesGuards(currState[i]))
                        {
                            foreach(FutureAtom atom in currState[i].apply(prod))
                            {
                                nextState.Add(atom.evaluate(currState[i].Parameters));
                            }
                            break;
                        }
                    }
                }
            }
            currState = nextState;
        }

        public List<Command> translate()
        {
            List<Command> ret = new List<Command>();
            foreach (Atom atom in currState)
            {
                FutureCommand listing = dictionary[atom.Letter];
                ret.Add(listing.evaluate(atom.Parameters));
            }

            return ret;
        }
    }
