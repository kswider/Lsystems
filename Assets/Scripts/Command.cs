using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Command
    {
        public String CommandName { get; }
        public List<Double> parameters { get; }

    public Command(string commandName, List<double> parameters)
    {
        CommandName = commandName;
        this.parameters = parameters;
    }
}
