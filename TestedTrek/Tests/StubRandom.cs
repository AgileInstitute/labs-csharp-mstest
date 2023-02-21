using System;

public class StubRandom : Random {
    public override int Next(int maxValue) {
        // always return 1/2 max:  It isn't random, and that's the point!
        return maxValue / 2;
    }
}
