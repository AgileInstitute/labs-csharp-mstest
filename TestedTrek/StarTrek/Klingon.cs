using System;

public class Klingon {
	private int distance;
	private int energy;

	public Klingon(int distance, int energy = 200)
	{
		this.distance = distance;
		this.energy = energy;
	}
	public Klingon() : 
		this(100 + Game.Rnd(4000), 1000 + Game.Rnd(2000)) {
	}

	public virtual int Distance() {
		return distance;
	}

	public virtual int GetEnergy() {
		return energy;
	}

	public void SetEnergy(int e) {
		energy = e;
	}

	public virtual void Delete() {
		// does nothing...yet!
	}

}
