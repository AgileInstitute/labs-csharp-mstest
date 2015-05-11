using System;
using System.Collections.Generic;

namespace Untouchables {

    public sealed class WebGadget {

        private string commandParameter;
        private string commandArgument;
        private object targetVariable;

        public WebGadget(string commandParameter, string commandArgument, object targetVariable) {
            this.commandParameter = commandParameter;
            this.commandArgument = commandArgument;
            this.targetVariable = targetVariable;
        }

        public string Parameter(string parameterName) {
            if (parameterName.Equals("command"))
                return commandParameter;
            else
                return commandArgument;
        }

        public object Variable(string variableName) {
            return targetVariable;
        }

        public void WriteLine(string message) {
            Console.WriteLine(message);
        }

    }
}