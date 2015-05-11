using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MSTestIDEIntegration {
    [TestMethod]
    public void FirstExample() {
        Assert.AreEqual("happy?", "HAPPY?".ToLower());
    }
}
