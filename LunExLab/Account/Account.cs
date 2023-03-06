using LunEx;

namespace Accounts
{

    public class Account
    {

        public long TotalGain(Lot[] lots, int currentPrice)
        {
            long total = 0;
            foreach (Lot nextLot in lots)
            {
                total += GainForLot(nextLot, currentPrice);
            }

            return total;
        }

        private long GainForLot(Lot lot, int currentPrice)
        {
            return lot.GainAt(currentPrice);
        }
    }
}