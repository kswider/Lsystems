using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// <summary>
    /// Something that may become Command
    /// </summary>
    [Serializable]
    public class FutureCommand
    {
    [UnityEngine.SerializeField]
    private string commandName;

    public string GetCommandName()
    {
        return commandName;
    }

    [UnityEngine.SerializeField]
    private List<Equation> equations;

    public List<Equation> GetEquations()
    {
        return equations;
    }

    public FutureCommand(string commandName, List<Equation> equations)
        {
            this.commandName = commandName;
            this.equations = equations;
        }

        public Command evaluate(List<Double> args)
        {
            List<Double> nParameters = new List<double>();
            foreach(Equation eq in GetEquations())
            {
                nParameters.Add(eq.apply(args));
            }

            return new Command(GetCommandName(), nParameters);
        }
    }
