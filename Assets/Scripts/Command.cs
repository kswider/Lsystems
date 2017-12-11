using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// <summary>
    /// Class representing command recognized by drawing classes
    /// </summary>
    class Command
    {
        public String CommandName { get; }
        public List<Double> parameters { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="commandName">String representing command (must be recognized by drawer classes)</param>
    /// <param name="parameters">Parameters that might be applied to the command e.g. "rotate" 90 degrees</param>
    public Command(string commandName, List<double> parameters)
    {
        CommandName = commandName;
        this.parameters = parameters;
    }
}
