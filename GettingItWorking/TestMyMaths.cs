namespace GettingItWorking;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TestMyMaths
{
    [TestMethod]
    public void Square_CanSquareTwo() {
        Assert.AreEqual(4, MyMaths.Square(2));
    }

    [TestMethod]
    public void Square_CanSquareThree() {
        Assert.AreEqual(9, MyMaths.Square(3));
    }

}

