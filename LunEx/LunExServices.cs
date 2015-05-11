using System;
using System.Collections.Generic;

namespace ITC {
    public interface SecurityExchangeTransmissionInterface {
        double CurrentPrice(string symbol);
    }
}

namespace LunEx {
    /// <summary>
    /// UNTOUCHABLE!  You cannot change this file.
    /// @see ITC.SecurityExchangeTransmissionInterface
    /// </summary>
    public class LunExServices : ITC.SecurityExchangeTransmissionInterface {
        private static Random invisibleHand = new Random();

        public virtual double CurrentPrice(string symbol) {
            pause();
            if (invisibleHand.Next(100) > 80)
                throw new LunExServiceUnavailableException();
            double randomPrice = 42.0 + (invisibleHand.NextDouble() * 2.1);
            return truncate(randomPrice);
        }

        private void pause() {
            System.Threading.Thread.Sleep(5000);
        }

        private double truncate(double original) {
            string originalAsString = original.ToString();
            string truncatedString = originalAsString.Substring(0, 7);
            return Double.Parse(truncatedString);
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
