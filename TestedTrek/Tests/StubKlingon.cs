using System;

public class StubKlingon : Klingon {
    private int overrideDistance;
    private bool deleteCalled = false;

    public StubKlingon(int distance) {
        overrideDistance = distance;
    }

    public StubKlingon(int distance, int energy) {
        overrideDistance = distance;
        SetEnergy(energy);
    }

    public override int Distance() {
        return overrideDistance;
    }

    public override void Delete() {
        deleteCalled = true;
    }

    internal bool DeleteWasCalled() {
        return deleteCalled;
    }
}
