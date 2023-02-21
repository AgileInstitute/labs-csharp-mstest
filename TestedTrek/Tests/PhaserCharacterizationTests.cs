using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Untouchables;

[TestClass]
public class PhaserCharacterizationTests {
    private Game game;
    private StubGalaxy context;

    const int EnergyInNewGame = 10000;

    [TestCleanup]
    public void RemoveTheMockRandomGeneratorForOtherTests_IReallyWantToRefactorThatStaticVariableSoon() {
        Game.generator = new Random();
    }

    [TestInitialize]
    public void SetUp() {
        game = new Game();
        context = new StubGalaxy();
        context.SetValueForTesting("command", "phaser");
    }

    [TestMethod]
    public void PhasersNotFiredWithInsufficientEnergy() {
        // delete the following line at your own peril!
        // some (not all) reasonable implementation refactorings could fetch and access the Klingon target earlier;
        // without a Klingon, they get a null reference exception from this test.
        context.SetValueForTesting("target", new Klingon(1234));
        
        context.SetValueForTesting("amount", (EnergyInNewGame + 1).ToString());
        game.FireWeapon(context);
        Assert.AreEqual("Insufficient energy to fire phasers! || ",
            context.GetAllOutput());
    }

    [TestMethod]
    public void PhasersFiredWhenKlingonOutOfRange_AndEnergyExpendedAnyway() {
        int maxPhaserRange = 4000;
        int outOfRange = maxPhaserRange + 1;
        int amountToFire = 1000;
        context.SetValueForTesting("amount", $"{amountToFire}");
        context.SetValueForTesting("target", new Klingon(outOfRange));
        
        game.FireWeapon(context);
        
        Assert.AreEqual($"Klingon out of range of phasers at {outOfRange} sectors... || ",
            context.GetAllOutput());
        Assert.AreEqual(EnergyInNewGame - amountToFire, game.EnergyRemaining());
    }

    [TestMethod]
    public void PhasersFiredKlingonDestroyed() {
        StubKlingon klingon = new StubKlingon(2000, 301);
        int amountToFire = 1000;
        context.SetValueForTesting("amount", $"{amountToFire}");
        context.SetValueForTesting("target", klingon);
        Game.generator = new StubRandom(new int[] { 199 });

        game.FireWeapon(context);
        
        Assert.AreEqual("Phasers hit Klingon at 2000 sectors with 301 units || Klingon destroyed! || ",
            context.GetAllOutput());
        Assert.AreEqual(EnergyInNewGame - amountToFire, game.EnergyRemaining());
        Assert.IsTrue(klingon.DeleteWasCalled());
    }
    
    [TestMethod]
    public void PhaserDamageDisplaysKlingonRemainingEnergy() {
        int amountToFire = 500;
        context.SetValueForTesting("amount", $"{amountToFire}");
        context.SetValueForTesting("target", new Klingon(2000, 3200));
        Game.generator = new StubRandom(new int[] { 102 });
        game.FireWeapon(context);
        Assert.AreEqual(
            $"Phasers hit Klingon at 2000 sectors with 148 units || Klingon has 3052 remaining || ",
            context.GetAllOutput());
        Assert.AreEqual(EnergyInNewGame - amountToFire, game.EnergyRemaining());
    }

    [TestMethod]
    public void PhasersDamageOfZeroStillHits_BUG31415()
    {
        // It's a bug!  I *ask* to fire zero, and I still hit with 1 point.
        // Acknowledge it, log it (Bug #31415), but don't fix it yet! -- Rob
        int minimalFired = 0;
        int minimalHit = 1;

        int maxPhaserRange = 4000;
        int leastAmountOfRandomDamage = 0;
        context.SetValueForTesting("amount", $"{minimalFired}");
        context.SetValueForTesting("target", new Klingon(maxPhaserRange, 201));
        Game.generator = new StubRandom(new int[] { leastAmountOfRandomDamage });
        
        game.FireWeapon(context);
        
        Assert.AreEqual(
            $"Phasers hit Klingon at {maxPhaserRange} sectors with {minimalHit} units || Klingon has 200 remaining || ",
            context.GetAllOutput());
    }

    [TestMethod]
    public void MakeSureAPIDoes_NOT_Change_See_SampleClient_in_StarTrekConsoleApp()
    {
        const String requiredSignature = "public Void FireWeapon(WebGadget)";
        bool found = false;

        TypeInfo gameInfo = typeof(Game).GetTypeInfo();
        IEnumerable<MethodInfo> methods = gameInfo.DeclaredMethods;
        foreach (MethodInfo nextMethod in methods)
        {
            if (requiredSignature == Signature(nextMethod))
                found = true;
        }
        
        Assert.IsTrue(found, 
            $"To keep SampleClient happy, Game must retain method with signature '{requiredSignature}'");
    }
    
    private static string Signature(MethodInfo methodInfo)
    {
        String[] parameters = methodInfo.GetParameters()
            .Select(p => String.Format("{0}",p.ParameterType.Name))
            .ToArray();

        return String.Format("{0} {1} {2}({3})", methodInfo.IsPublic ? "public" : "", methodInfo.ReturnType.Name, methodInfo.Name, String.Join(",", parameters));
    }

}

