using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Class responsible for simulating L-Systems
/// </summary>
[Serializable]
public class Simulation
    {
        [SerializeField]
        public List<Atom> currState;
        [SerializeField]
        public List<Production> productions;
        [SerializeField]
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
            dictionary.Add('F', new FutureCommand("Forward", new List<Equation> { new Equation("10.0") }));
            dictionary.Add('+', new FutureCommand("Rotate U", new List<Equation> { new Equation("26.0") }));
            dictionary.Add('-', new FutureCommand("Rotate U", new List<Equation> { new Equation("-26.0") }));
            dictionary.Add('[', new FutureCommand("Push position", new List<Equation>()));
            dictionary.Add(']', new FutureCommand("Pull position", new List<Equation>()));
        }
        else if (egNum == 2)
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
            dictionary.Add('F', new FutureCommand("Forward", new List<Equation> { new Equation("10.0") }));
            dictionary.Add('+', new FutureCommand("Rotate U", new List<Equation> { new Equation("26.0") }));
            dictionary.Add('-', new FutureCommand("Rotate U", new List<Equation> { new Equation("-26.0") }));
            dictionary.Add('[', new FutureCommand("Push position", new List<Equation>()));
            dictionary.Add(']', new FutureCommand("Pull position", new List<Equation>()));
            dictionary.Add('X', new FutureCommand("Do nothing", new List<Equation>()));
        }
        else if (egNum == 3)
        {
            currState = new List<Atom> { new Atom('X', new List<double> { 20.0 }) };

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
            dictionary.Add('+', new FutureCommand("Rotate U", new List<Equation> { new Equation("26.0") }));
            dictionary.Add('-', new FutureCommand("Rotate U", new List<Equation> { new Equation("-26.0") }));
            dictionary.Add('[', new FutureCommand("Push position", new List<Equation>()));
            dictionary.Add(']', new FutureCommand("Pull position", new List<Equation>()));
            dictionary.Add('X', new FutureCommand("Do nothing", new List<Equation>()));
        }
        else if (egNum == 4)
        {
            currState = new List<Atom> { new Atom('F', new List<double>()) };

            Production p1 = new Production(
                new List<Rule> { new Rule("true") },
                'F',
                new List<SimpleProduction> {  new SimpleProduction(
                        new List<FutureAtom> {
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom('-', new List<Equation>()),
                            new FutureAtom('[', new List<Equation>()),
                            new FutureAtom('-', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom('+', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom('+', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom(']', new List<Equation>()),
                            new FutureAtom('+', new List<Equation>()),
                            new FutureAtom('[', new List<Equation>()),
                            new FutureAtom('+', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom('-', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom('-', new List<Equation>()),
                            new FutureAtom('F', new List<Equation>()),
                            new FutureAtom(']', new List<Equation>()) },
                        1.0) });

            productions = new List<Production>();
            productions.Add(p1);

            dictionary = new Dictionary<char, FutureCommand>();
            dictionary.Add('F', new FutureCommand("Forward", new List<Equation> { new Equation("1.0") }));
            dictionary.Add('+', new FutureCommand("Rotate U", new List<Equation> { new Equation("22.5") }));
            dictionary.Add('-', new FutureCommand("Rotate U", new List<Equation> { new Equation("-22.5") }));
            dictionary.Add('[', new FutureCommand("Push position", new List<Equation>()));
            dictionary.Add(']', new FutureCommand("Pull position", new List<Equation>()));
        }
        else if (egNum == 5)
        {
            currState = new List<Atom> { new Atom('A', new List<double> { 1, 0.5 }) };

            //variables used in productions
            double r1 = 0.9;
            double r2 = 0.6;
            double a0 = 45;
            double a2 = 45;
            if (Scenes.Parameters != null)
            {
                foreach (KeyValuePair<string, double> parameter in Scenes.Parameters)
                {
                    switch (parameter.Key)
                    {
                        case "r1":
                            r1 = parameter.Value;
                            break;
                        case "r2":
                            r2 = parameter.Value;
                            break;
                        case "a0":
                            a0 = parameter.Value;
                            break;
                        case "a2":
                            a2 = parameter.Value;
                            break;
                    }
                }
            }
            double d = 137.5;
            double wr = 0.707;


            Production p1 = new Production(
                new List<Rule> { new Rule("true") },
                'A',
                new List<SimpleProduction> {  new SimpleProduction(
                        new List<FutureAtom> {
                            new FutureAtom('!', new List<Equation> {new Equation("t1") }),
                            new FutureAtom('F', new List<Equation>{ new Equation("t0") }),
                            new FutureAtom('[', new List<Equation>()),
                            new FutureAtom('&', new List<Equation>{ new Equation(String.Format("{0:0.000}", a0)) }),
                            new FutureAtom('B', new List<Equation>{ new Equation("t0 * " + String.Format("{0:0.000}", r1)), new Equation("t1 * " + String.Format("{0:0.000}",wr)) }),
                            new FutureAtom(']', new List<Equation>()),
                            new FutureAtom('/', new List<Equation> { new Equation(String.Format("{0:0.000}", d)) }),
                            new FutureAtom('A', new List<Equation> { new Equation("t0 * " + String.Format("{0:0.000}", r1)), new Equation("t1 *" + String.Format("{0:0.000}", wr)) }) },
                        1.0)});

            Production p2 = new Production(
                new List<Rule> { new Rule("true") },
                'B',
                new List<SimpleProduction> {  new SimpleProduction(
                        new List<FutureAtom> {
                            new FutureAtom('!', new List<Equation> {new Equation("t1") }),
                            new FutureAtom('F', new List<Equation>{ new Equation("t0") }),
                            new FutureAtom('[', new List<Equation>()),
                            new FutureAtom('-', new List<Equation> { new Equation(String.Format("{0:0.000}", a2)) }),
                            new FutureAtom('$', new List<Equation>()),
                            new FutureAtom('C', new List<Equation>{ new Equation("t0 * " + String.Format("{0:0.000}",r1)), new Equation("t1 * " + String.Format("{0:0.000}", wr)) }),
                            new FutureAtom(']', new List<Equation>()),
                            new FutureAtom('C', new List<Equation>{ new Equation("t0 * " + String.Format("{0:0.000}", r1)), new Equation("t1 * " + String.Format("{0:0.000}", wr)) }) },
                        1.0)});

            Production p3 = new Production(
                new List<Rule> { new Rule("true") },
                'C',
                new List<SimpleProduction> {  new SimpleProduction(
                        new List<FutureAtom> {
                            new FutureAtom('!', new List<Equation> {new Equation("t1") }),
                            new FutureAtom('F', new List<Equation>{ new Equation("t0") }),
                            new FutureAtom('[', new List<Equation>()),
                            new FutureAtom('+', new List<Equation> { new Equation(String.Format("{0:0.000}", a0)) }),
                            new FutureAtom('$', new List<Equation>()),
                            new FutureAtom('B', new List<Equation>{ new Equation("t0 * " + String.Format("{0:0.000}", r2)), new Equation("t1 * " + String.Format("{0:0.000}", wr)) }),
                            new FutureAtom(']', new List<Equation>()),
                            new FutureAtom('B', new List<Equation>{ new Equation("t0 * " + String.Format("{0:0.000}", r1)), new Equation("t1 * " + String.Format("{0:0.000}", wr)) }) },
                        1.0)});



            productions = new List<Production>();
            productions.Add(p1);
            productions.Add(p2);
            productions.Add(p3);

            dictionary = new Dictionary<char, FutureCommand>();
            dictionary.Add('F', new FutureCommand("Forward", new List<Equation> { new Equation("t0") }));
            dictionary.Add('!', new FutureCommand("Change width", new List<Equation> { new Equation("t0") }));
            dictionary.Add('+', new FutureCommand("Rotate U", new List<Equation> { new Equation("t0") }));
            dictionary.Add('-', new FutureCommand("Rotate U", new List<Equation> { new Equation("(-1)*t0") }));
            dictionary.Add('[', new FutureCommand("Push position", new List<Equation>()));
            dictionary.Add(']', new FutureCommand("Pull position", new List<Equation>()));
            dictionary.Add('$', new FutureCommand("Dollar rotation", new List<Equation>()));
            dictionary.Add('&', new FutureCommand("Rotate L", new List<Equation> { new Equation("t0") }));
            dictionary.Add('/', new FutureCommand("Rotate H", new List<Equation> { new Equation("(-1)*t0") }));
            dictionary.Add('C', new FutureCommand("Do nothing", new List<Equation>()));
            dictionary.Add('A', new FutureCommand("Do nothing", new List<Equation>()));
            dictionary.Add('B', new FutureCommand("Do nothing", new List<Equation>()));
        }
        else if (egNum == 6)
        {
            currState = Scenes.StartingSequence;
            productions = Scenes.Productions;

            dictionary = new Dictionary<char, FutureCommand>();
            dictionary.Add('F', new FutureCommand("Forward", new List<Equation> { new Equation("t0") }));
            dictionary.Add('!', new FutureCommand("Change width", new List<Equation> { new Equation("t0") }));
            dictionary.Add('+', new FutureCommand("Rotate U", new List<Equation> { new Equation("t0") }));
            dictionary.Add('-', new FutureCommand("Rotate U", new List<Equation> { new Equation("(-1)*t0") }));
            dictionary.Add('[', new FutureCommand("Push position", new List<Equation>()));
            dictionary.Add(']', new FutureCommand("Pull position", new List<Equation>()));
            dictionary.Add('$', new FutureCommand("Dollar rotation", new List<Equation>()));
            dictionary.Add('&', new FutureCommand("Rotate L", new List<Equation> { new Equation("t0") }));
            dictionary.Add('/', new FutureCommand("Rotate H", new List<Equation> { new Equation("(-1)*t0") }));
            dictionary.Add('C', new FutureCommand("Do nothing", new List<Equation>()));
            dictionary.Add('A', new FutureCommand("Do nothing", new List<Equation>()));
            dictionary.Add('B', new FutureCommand("Do nothing", new List<Equation>()));
        }
        else
        {
            currState = null;
            productions = null;
            dictionary = null;
        }
        }

    /// <summary>
    /// Generates List of Atom from String with proper pattern
    /// </summary>
    /// <param name="atomStr">String representation of 'state' e.g. A(1,2,3)B(4)C(5)DF</param>
    /// <returns>Result List of Atom or empty List if string is not valid </returns>
    public static List<Atom> GenerateStateFromSting(String state)
        {
            List<Atom> ret = new List<Atom>();

            String pattern = @".(\([0-9]+(\.[0-9]*)?(,[0-9]+(\.[0-9]*)?)*\))?";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = regex.Match(state);

            while (m.Success)
            {
                Group g1 = m.Groups[0];

                ret.Add(Atom.GenerateAtomFromString(g1.ToString()));

                m = m.NextMatch();
            }

        return ret;
        }

    /// <summary>
    /// Generates List of FutureAtom from String with proper pattern
    /// </summary>
    /// <param name="atomStr">String representation of 'future state' e.g. A(t1,t2)B(5)CD(t3-1)</param>
    /// <returns>Result List of FutureAtom or empty List if string is not valid </returns>
    public static List<FutureAtom> GenerateFutureStateFromSting(String state)
    {
        List<FutureAtom> ret = new List<FutureAtom>();

        for(int i = 0; i < state.Length; i++)
        {
            if(i != state.Length-1 && state[i+1] == '(')
            {
                int length = 2;
                int parentesisCount = 1;
                int start = i;

                for(int j = i+2; parentesisCount != 0; j++)
                {
                    if(j >= state.Length) return new List<FutureAtom>();
                    if (state[j] == '(') parentesisCount++;
                    else if (state[j] == ')') parentesisCount--;

                    length++;
                    i = j;
                }

                ret.Add(FutureAtom.GenerateFutureAtomFromString(state.Substring(start, length)));
            } else
            {
                ret.Add(FutureAtom.GenerateFutureAtomFromString(state.Substring(i,1)));
            }
        }
        
        return ret;
    }

        /// <summary>
        /// Method aplying productions to current state of the system n times
        /// </summary>
        /// <param name="n">Defines how many steps of productions will be done</param>
        public void evaluate(int n)
        {

            for(int i = 0; i < n; i++)
            {
                List<Atom> nextState = new List<Atom>();

                foreach (Atom currStateAtom in currState)
                {
                    Boolean hasPassed = false;
                    foreach (Production prod in productions)
                    {
                        if (prod.passesGuards(currStateAtom))
                        {
                            foreach(Atom nextAtom in currStateAtom.apply(prod))
                            {
                                nextState.Add(nextAtom);
                            }
                            hasPassed = true;
                            break;
                        }
                    }
                if (!hasPassed) nextState.Add(currStateAtom);
                }

                currState = nextState;
        }
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
                Debug.Log(atom.GetLetter());
                FutureCommand listing = dictionary[atom.GetLetter()];
                ret.Add(listing.evaluate(atom.GetParameters()));
            }

            return ret;
        }
    }
