using System;
using System.Collections.Generic;

namespace ITC {
    public interface SecurityExchangeTransmissionInterface {
        int CurrentPrice(string symbol);
    }
}

namespace LunEx {
    /// <summary>
    /// UNTOUCHABLE!  You cannot change this file.
    /// @see ITC.SecurityExchangeTransmissionInterface
    /// </summary>
    public class LunExServices : ITC.SecurityExchangeTransmissionInterface {
        private static Random invisibleHand = new Random();

        public virtual int CurrentPrice(string symbol) {
            pauseToEmulateSendReceive();
            if (invisibleHand.Next(100) > 80)
                throw new LunExServiceUnavailableException();
            return 103 + invisibleHand.Next(20);
        }

        private void pauseToEmulateSendReceive() {
            System.Threading.Thread.Sleep(5000);
        }
    }
    public class LunExServiceUnavailableException : Exception {
        public override string Message {
            get {
                return "Sorry, sunspot activity today...please try again later";
            }
        }
    }
}
