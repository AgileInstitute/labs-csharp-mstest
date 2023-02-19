using System;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Untouchables;

[TestClass]
public class PhaserPinningTests {
    private Game game;
    private MockGalaxy context;

    const int EnergyInNewGame = 10000;

    [TestCleanup]
    public void RemoveTheMockRandomGeneratorForOtherTests_IReallyWantToRefactorThatStaticVariableSoon() {
        Game.generator = new Random();
    }

    [TestInitialize]
    public void SetUp() {
        game = new Game();
        context = new MockGalaxy();
        context.SetValueForTesting("command", "phaser");
    }

    [TestMethod]
    public void PhasersFiredWithInsufficientEnergy() {
        context.SetValueForTesting("amount", (EnergyInNewGame + 1).ToString());
        game.FireWeapon(context);
        Assert.AreEqual("Insufficient energy to fire phasers! || ",
            context.GetAllOutput());
    }

    [TestMethod]
    public void PhasersFiredWhenKlingonOutOfRange_AndEnergyExpendedAnyway() {
        int maxPhaserRange = 4000;
        int outOfRange = maxPhaserRange + 1;
        context.SetValueForTesting("amount", "1000");
        context.SetValueForTesting("target", new MockKlingon(outOfRange));
        game.FireWeapon(context);
        Assert.AreEqual("Klingon out of range of phasers at " + outOfRange + " sectors... || ",
            context.GetAllOutput());
        Assert.AreEqual(EnergyInNewGame - 1000, game.EnergyRemaining());
    }

    [TestMethod]
    public void PhasersFiredKlingonDestroyed() {
        MockKlingon klingon = new MockKlingon(2000, 200);
        context.SetValueForTesting("amount", "1000");
        context.SetValueForTesting("target", klingon);
        Game.generator = new MockRandom();
        game.FireWeapon(context);
        Assert.AreEqual("Phasers hit Klingon at 2000 sectors with 400 units || Klingon destroyed! || ",
            context.GetAllOutput());
        Assert.AreEqual(EnergyInNewGame - 1000, game.EnergyRemaining());
        Assert.IsTrue(klingon.DeleteWasCalled());
    }

    [TestMethod]
    public void PhasersDamageOfZeroStillHits_AndNondestructivePhaserDamageDisplaysRemaining() {
        string minimalFired = "0";
        string minimalHit = "1";
        context.SetValueForTesting("amount", minimalFired);
        context.SetValueForTesting("target", new MockKlingon(2000, 200));
        Game.generator = new MockRandom();
        game.FireWeapon(context);
        Assert.AreEqual("Phasers hit Klingon at 2000 sectors with " +
            minimalHit + " units || Klingon has 199 remaining || ",
            context.GetAllOutput());
        // Isn't this also a bug?  I *ask* to fire zero, and I still hit?
        // Acknowledge it, log it, but don't fix it yet!
    }

}

