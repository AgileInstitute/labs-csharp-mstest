namespace GettingItWorking;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TestIDEIntegration
{
    [TestMethod]
    public void FirstExample() {
        Assert.AreEqual("happy?", "HAPPY?".ToLower());
        // no I'm not happy right now
    }

}