using System;

public class StubRandom : Random {
    private readonly int[] values;
    private int index = 0;

    public StubRandom(int[] valuesToReturnInOrder)
    {
        this.values = valuesToReturnInOrder;
    } 
    public override int Next(int maxValueExclusive)
    {
        int nextValue = values[index++];
        if (nextValue >= maxValueExclusive) throw new Exception($"Oops! Stubbed return {nextValue} >= {maxValueExclusive}!!!");

        return nextValue;
    }
}
