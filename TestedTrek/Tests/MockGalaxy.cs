using System;
using System.Collections.Generic;
using System.Collections;

using System.Text;

public class MockGalaxy : Galaxy {
    private Hashtable stuff = new Hashtable();
    private StringBuilder buffer = new StringBuilder();
    public MockGalaxy()
        : base(null) {
    }

    public override string Parameter(string parameterName) {
        return (string)stuff[parameterName];
    }

    public override object Variable(string variableName) {
        return stuff[variableName];
    }

    public override void WriteLine(string message) {
        const string fakeNewLine = " || ";
        buffer.Append(message + fakeNewLine);
    }

    internal void SetValueForTesting(string key, object value) {
        stuff[key] = value;
    }

    internal string GetAllOutput() {
        return buffer.ToString();
    }
}
