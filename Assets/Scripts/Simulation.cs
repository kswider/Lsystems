using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

    public Simulation(int egNum)
    {
        if (egNum == 1)
        {
            currState = new List<Atom> { new Atom('F', new List<double>()) };

            Production p1 = new Production(
                new List<Rule> { new Rule("true") },
                'F',
                new List<SimpleProduction> {  new SimpleProduction(
                        new List<FutureAtom> {
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom('[', new List<Equation>()),
                            new FutureAtom('+', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom(']', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom('[', new List<Equation>()),
                            new FutureAtom('-', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom(']', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()) },
                        1.0) });

            productions = new List<Production>();
            productions.Add(p1);

            dictionary = new Dictionary<char, FutureCommand>();
            dictionary.Add('F', new FutureCommand("Forward", new List<Equation> { new Equation("10") }));
            dictionary.Add('+', new FutureCommand("Rotate left", new List<Equation> { new Equation("26") }));
            dictionary.Add('-', new FutureCommand("Rotate right", new List<Equation> { new Equation("26") }));
            dictionary.Add('[', new FutureCommand("Push position", new List<Equation>()));
            dictionary.Add(']', new FutureCommand("Pull position", new List<Equation>()));
        }else if(egNum == 2)
        {
            currState = new List<Atom> { new Atom('X', new List<double>()) };

            Production p1 = new Production(
                new List<Rule> { new Rule("true") },
                'X',
                new List<SimpleProduction> {  new SimpleProduction(
                        new List<FutureAtom> {
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom('[', new List<Equation>()),
                            new FutureAtom('+', new List<Equation>()),
                            new FutureAtom('X', new List<Equation>()),
                            new FutureAtom(']', new List<Equation>()),
                            new FutureAtom('[', new List<Equation>()),
                            new FutureAtom('-', new List<Equation>()),
                            new FutureAtom('X', new List<Equation>()),
                            new FutureAtom(']', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()), },
                        1.0) });

            Production p2 = new Production(
                new List<Rule> { new Rule("true") },
                'F',
                new List<SimpleProduction> {  new SimpleProduction(
                        new List<FutureAtom> {
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()), },
                        1.0) });

            productions = new List<Production>();
            productions.Add(p1);
            productions.Add(p2);

            dictionary = new Dictionary<char, FutureCommand>();
            dictionary.Add('F', new FutureCommand("Forward", new List<Equation> { new Equation("10") }));
            dictionary.Add('+', new FutureCommand("Rotate left", new List<Equation> { new Equation("26") }));
            dictionary.Add('-', new FutureCommand("Rotate right", new List<Equation> { new Equation("26") }));
            dictionary.Add('[', new FutureCommand("Push position", new List<Equation>()));
            dictionary.Add(']', new FutureCommand("Pull position", new List<Equation>()));
            dictionary.Add('X', new FutureCommand("Do nothing", new List<Equation>()));
        }
        else if (egNum == 3)
        {
            currState = new List<Atom> { new Atom('X', new List<double> { 20 }) };

            Production p1 = new Production(
                new List<Rule> { new Rule("true") },
                'X',
                new List<SimpleProduction> {  new SimpleProduction(
                        new List<FutureAtom> {
                            new FutureAtom('F', new List<Equation> { new Equation("t0") }),
                            new FutureAtom('[', new List<Equation>()),
                            new FutureAtom('+', new List<Equation>()),
                            new FutureAtom('X', new List<Equation> { new Equation("t0") }),
                            new FutureAtom(']', new List<Equation>()),
                            new FutureAtom('[', new List<Equation>()),
                            new FutureAtom('-', new List<Equation>()),
                            new FutureAtom('X', new List<Equation> { new Equation("t0") }),
                            new FutureAtom(']', new List<Equation>()),
                            new FutureAtom('F', new List<Equation> { new Equation("t0") }),
                            new FutureAtom('F', new List<Equation> { new Equation("t0") }), },
                        1.0) });

            Production p2 = new Production(
                new List<Rule> { new Rule("true") },
                'F',
                new List<SimpleProduction> {
                    new SimpleProduction(
                        new List<FutureAtom> {
                            new FutureAtom('F', new List<Equation> { new Equation("t0") }),
                            new FutureAtom('F', new List<Equation> { new Equation("t0") }), },
                        0.5),
                    new SimpleProduction(
                        new List<FutureAtom> {
                            new FutureAtom('F', new List<Equation> { new Equation("t0/2") }),},
                        0.5),
                });

            productions = new List<Production>();
            productions.Add(p1);
            productions.Add(p2);

            dictionary = new Dictionary<char, FutureCommand>();
            dictionary.Add('F', new FutureCommand("Forward", new List<Equation> { new Equation("t0") }));
            dictionary.Add('+', new FutureCommand("Rotate left", new List<Equation> { new Equation("26") }));
            dictionary.Add('-', new FutureCommand("Rotate right", new List<Equation> { new Equation("26") }));
            dictionary.Add('[', new FutureCommand("Push position", new List<Equation>()));
            dictionary.Add(']', new FutureCommand("Pull position", new List<Equation>()));
            dictionary.Add('X', new FutureCommand("Do nothing", new List<Equation>()));
            }
            else
            {
                currState = null;
                productions = null;
                dictionary = null;
            }
        }

        public static List<Atom> generateStateFromSting(String state)
        {
            List<Atom> ret = new List<Atom>();

            String pattern = @"(\w)(\(([0-9]+(\.[0-9]+)?)(,([0-9]+(\.[0-9]+))?)*\))?";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = regex.Match(state);

            while (m.Success)
            {
                Group g1 = m.Groups[1];
                Group g2 = m.Groups[2];

                //Atom letter
                CaptureCollection cc1 = g1.Captures;
                Capture letter = cc1[0];

                //Atom args
                List<Double> nParams = new List<Double>();
                CaptureCollection cc2 = g2.Captures;
                for (int j = 0; j < cc2.Count; j++)
                {
                    Capture c2 = cc2[j];

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

                        ret.Add(new Atom(letter.ToString()[0], nParams));
                        m2 = m2.NextMatch();
                    }
                }

                m = m.NextMatch();
            }

        return ret;
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
