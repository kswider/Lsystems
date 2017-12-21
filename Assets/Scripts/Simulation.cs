using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

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
            dictionary.Add('F', new FutureCommand("Forward", new List<Equation> { new Equation("10.0") }));
            dictionary.Add('+', new FutureCommand("Rotate U", new List<Equation> { new Equation("26.0") }));
            dictionary.Add('-', new FutureCommand("Rotate U", new List<Equation> { new Equation("-26.0") }));
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
            currState = new List<Atom> { new Atom('A', new List<double> {1,0.5 }) };

            //variables used in productions
            double r1 = 0.9;
            double r2 = 0.6;
            double a0 = 45;
            double a2 = 45;
            foreach(KeyValuePair<string,double> parameter in Scenes.getSceneParameters())
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

        public void toLog()
        {
        String tmp = "";
            foreach(Atom atom in currState)
            {
                tmp += atom.GetLetter();
            }
        Debug.Log("Result: " + tmp);
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
