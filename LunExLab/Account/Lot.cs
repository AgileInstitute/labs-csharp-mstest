namespace Accounts;

public class Lot
{
    private int shares;
    private long cost;

    public Lot(int shares, long cost)
    {
        this.shares = shares;
        this.cost = cost;
    }

    public long GainAt(int currentPrice)
    {
        return (shares * currentPrice) - cost;
    }
}