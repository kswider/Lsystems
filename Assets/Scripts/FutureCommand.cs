using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// <summary>
    /// Something that may become Command
    /// </summary>
    class FutureCommand
    {
        public String CommandName { get;  }
        public List<Equation> Equations { get; }

        public FutureCommand(string commandName, List<Equation> equations)
        {
            CommandName = commandName;
            Equations = equations;
        }

        public Command evaluate(List<Double> args)
        {
            List<Double> nParameters = new List<double>();
            foreach(Equation eq in Equations)
            {
                nParameters.Add(eq.apply(args));
            }

            return new Command(CommandName, nParameters);
        }
    }
