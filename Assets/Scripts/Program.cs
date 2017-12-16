using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// <summary>
    /// Class for some simple tests 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            List<Atom> start = new List<Atom>();
            start.Add(new Atom('f', new List<Double>()));
            start.Add(new Atom('f', new List<Double>()));

            List<FutureAtom> tmp1 = new List<FutureAtom>();
            tmp1.Add(new FutureAtom('f', new List<Equation>()));
            SimpleProduction sp1 = new SimpleProduction(tmp1, 1.0);
            List<SimpleProduction> spl1 = new List<SimpleProduction>();
            spl1.Add(sp1);
            List<Rule> rl1 = new List<Rule>();
            rl1.Add(new Rule("true"));

            List<FutureAtom> tmp2 = new List<FutureAtom>();
            tmp2.Add(new FutureAtom('F', new List<Equation>()));
            tmp2.Add(new FutureAtom('F', new List<Equation>()));
            SimpleProduction sp2 = new SimpleProduction(tmp2, 1.0);
            List<SimpleProduction> spl2 = new List<SimpleProduction>();
            spl2.Add(sp2);
            List<Rule> rl2 = new List<Rule>();
            rl2.Add(new Rule("true"));

            List<Production> productions = new List<Production>();
            productions.Add(new Production(rl1, 'F',spl1));
            productions.Add(new Production(rl2, 'f', spl2));

            Dictionary<Char, FutureCommand> dictionary = new Dictionary<char, FutureCommand>();
            List<Equation> empty = new List<Equation>();

            dictionary.Add('F', new FutureCommand("F", empty));
            dictionary.Add('f', new FutureCommand("f", empty));

            Simulation sim = new Simulation(start, productions, dictionary);

            foreach (Command obj in sim.translate())
            {
                Console.WriteLine(obj.CommandName);
            }
  //          Console.ReadKey();

            sim.evaluate(1);

            foreach (Command obj in sim.translate())
            {
                Console.WriteLine(obj.CommandName);
            }
         //   Console.ReadKey();

            sim.evaluate(1);

            foreach (Command obj in sim.translate())
            {
                Console.WriteLine(obj.CommandName);
            }
           // Console.ReadKey();
        }
    }
