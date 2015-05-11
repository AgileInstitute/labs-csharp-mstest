using System;

using Untouchables;

/// <summary>
/// A simple Proxy/wrapper class for Untouchables.WebGadget,
/// allowing us to create, modify, and subclass (or mock) Galaxy.
/// Note that this object currently has no tests, because it does nothing but delegate,
/// and the delegation is not unit-testable.
/// Note also that it is production code.
/// </summary>
public class Galaxy {
    private WebGadget webContext;

    public Galaxy(WebGadget webContext) {
        this.webContext = webContext;
    }

    public virtual string Parameter(string parameterName) {
        return webContext.Parameter(parameterName);
    }

    public virtual object Variable(string variableName) {
        return webContext.Variable(variableName);
    }

    public virtual void WriteLine(string message) {
        webContext.WriteLine(message);
    }
}
