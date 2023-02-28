
using Accounts;

namespace AccountTests;

using Microsoft.VisualStudio.TestTools.UnitTesting;


[TestClass]
public class TotalGainTests
{
    [TestMethod]
    public void WhenUsingInteger()
    {
        // given
        var firstLot = new Lot(100, 3000L);
        var latestLot = new Lot(10, 400L);
        Lot[] lots = { firstLot, latestLot };
        Account account = new Account();
        
        // when/then
        Assert.AreEqual(1220L, account.TotalGain(lots, 42));
    }
}