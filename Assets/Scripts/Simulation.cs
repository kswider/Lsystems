using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// <summary>
    /// Class responsible for simulating L-Systems
    /// </summary>
    class Simulation
    {
        public List<Atom> currState;
        public List<Production> productions;
        public Dictionary<Char, FutureCommand> dictionary;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="currState">Starting string exported to format of List of Atoms</param>
        /// <param name="productions">List of defined productions</param>
        /// <param name="dictionary">Dictionary used to translating atoms to commands</param>
        public Simulation(List<Atom> currState, List<Production> productions, Dictionary<char, FutureCommand> dictionary)
        {
            this.currState = currState;
            this.productions = productions;
            this.dictionary = dictionary;
        }

        /// <summary>
        /// Method aplying productions to current state of the system n times
        /// </summary>
        /// <param name="n">Defines how many steps of productions will be done</param>
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
                            foreach(Atom atom in currState[i].apply(prod))
                            {
                                nextState.Add(atom);
                            }
                            break;
                        }
                    }
                }
            }
            currState = nextState;
        }

        /// <summary>
        /// Method for translating current state of system to list of commands
        /// </summary>
        /// <returns></returns>
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
