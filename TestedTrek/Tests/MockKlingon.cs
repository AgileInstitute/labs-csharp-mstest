using System;

public class MockKlingon : Klingon {
    private int overrideDistance;
    private bool deleteCalled = false;

    public MockKlingon(int distance) {
        overrideDistance = distance;
    }

    public MockKlingon(int distance, int energy) {
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
