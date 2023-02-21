using System;

public class StubKlingon : Klingon {
    private bool deleteCalled = false;

    public StubKlingon(int distance, int energy = 200) : base(distance, energy) {
    }
    
    public override void Delete() {
        deleteCalled = true;
    }

    internal bool DeleteWasCalled() {
        return deleteCalled;
    }
}
