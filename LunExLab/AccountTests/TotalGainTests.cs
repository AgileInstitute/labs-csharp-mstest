
using Accounts;
using LunEx;

namespace AccountTests
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;


    [TestClass]
    public class TotalGainTests
    {
        [TestMethod]
        public void WhenUsingLunEx()
        {
            // given
            var firstLot = new Lot(100, 3000L);
            var latestLot = new Lot(10, 400L);
            Lot[] lots = { firstLot, latestLot };
            Account account = new Account();
            string symbol = "HE3";
            // Current price of HE3 was 42 when we wrote this...

            // when/then
            Assert.AreEqual(1220L, account.TotalGain(lots, symbol, new LunExServices()));
        }

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
}