using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// <summary>
    /// Class representing command recognized by drawing classes
    /// </summary>
    [Serializable]
    public class Command
    {

    [UnityEngine.SerializeField]
    private string commandName;

    public string GetCommandName()
    {
        return commandName;
    }

    [UnityEngine.SerializeField]
    private List<double> parameters;

    public List<double> GetParameters()
    {
        return parameters;
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="commandName">String representing command (must be recognized by drawer classes)</param>
    /// <param name="parameters">Parameters that might be applied to the command e.g. "rotate" 90</param>
    public Command(string commandName, List<double> parameters)
    {
        this.commandName = commandName;
        this.parameters = parameters;
    }
}
