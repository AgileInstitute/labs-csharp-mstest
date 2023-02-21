using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Untouchables;

[TestClass]
public class PhotonCharacterizationTests {
    private Game game;
    private StubGalaxy context;

    [TestCleanup]
    public void RemoveTheMockRandomGeneratorForOtherTests_IReallyWantToRefactorThatStaticVariableSoon() {
        Game.generator = new Random();
    }

    [TestInitialize]
    public void SetUp() {
        game = new Game();
        context = new StubGalaxy();
        context.SetValueForTesting("command", "photon");
    }

    [TestMethod]
    public void NotifiedIfNoTorpedoesRemain() {
        game.Torpedoes = 0;
        context.SetValueForTesting("target", new Klingon(2000, 200));
        game.FireWeapon(context);
        Assert.AreEqual("No more photon torpedoes! || ",
            context.GetAllOutput());
    }

    [TestMethod]
    public void TorpedoMissesDistantKlingon_DueToRandomNumber_LuckyKlingon() {
        int distanceWhereRandomFactorsHoldSway = 2500;
        int randomValueNudgingTheTorpedoOffCourse = 2;
        context.SetValueForTesting("target", new Klingon(distanceWhereRandomFactorsHoldSway, 200));
        Game.generator = new StubRandom(new int[] { randomValueNudgingTheTorpedoOffCourse });
        game.FireWeapon(context);
        Assert.AreEqual("Torpedo missed Klingon at 2500 sectors... || ",
            context.GetAllOutput());
        Assert.AreEqual(7, game.Torpedoes);
    }
    
    [TestMethod]
    public void TorpedoDoesNOTMissDistantKlingon_DueToRandomNumber_UnluckyKlingon() {
        int maxPhotonRange = 3499;
        int randomValueThatDoesNotAffectCourse = 0;
        int defaultTorpedoDamage = 800;
        int randomDamage = 3;
        int totalDamage = defaultTorpedoDamage + randomDamage;
        context.SetValueForTesting("target",
            new Klingon(maxPhotonRange, totalDamage));
        Game.generator = new StubRandom(
            new int[] { randomValueThatDoesNotAffectCourse, randomDamage });
        
        game.FireWeapon(context);
        
        Assert.AreEqual(
            $"Photons hit Klingon at {maxPhotonRange} sectors with {totalDamage} units || Klingon destroyed! || ",
            context.GetAllOutput());
        Assert.AreEqual(7, game.Torpedoes);
    }

    [TestMethod]
    public void TorpedoMissesDueToDistanceAndCleverKlingonEvasiveActions() {
        int distanceWhereTorpedoesAlwaysMiss = 3500;
        context.SetValueForTesting("target", new Klingon(distanceWhereTorpedoesAlwaysMiss, 1234));
        Game.generator = new StubRandom(
            new int[] { 3, 45 }); // stub added just to avoid real Random - values mostly harmless
        
        game.FireWeapon(context);
        
        Assert.AreEqual($"Torpedo missed Klingon at {distanceWhereTorpedoesAlwaysMiss} sectors... || ",
            context.GetAllOutput());
        Assert.AreEqual(7, game.Torpedoes);
    }

    [TestMethod]
    public void TorpedoDestroysKlingon() {
        StubKlingon klingon = new StubKlingon(500, 200);
        context.SetValueForTesting("target", klingon);
        Game.generator = new StubRandom(new int[] { 2, 25 });
        
        game.FireWeapon(context);
        
        Assert.AreEqual("Photons hit Klingon at 500 sectors with 825 units || Klingon destroyed! || ",
            context.GetAllOutput());
        Assert.AreEqual(7, game.Torpedoes);
        Assert.IsTrue(klingon.DeleteWasCalled());

    }

    [TestMethod]
    public void TorpedoDamagesKlingon() {
        context.SetValueForTesting("target", new Klingon(500, 2000));
        Game.generator = new StubRandom(new int[] { 2, 25 });
        game.FireWeapon(context);
        Assert.AreEqual("Photons hit Klingon at 500 sectors with 825 units || Klingon has 1175 remaining || ",
            context.GetAllOutput());
        Assert.AreEqual(7, game.Torpedoes);
    }

}
