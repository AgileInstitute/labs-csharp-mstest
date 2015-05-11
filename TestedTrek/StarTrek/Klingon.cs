using System;

public class Klingon {
	private int distance;
	private int energy;
	
	public Klingon() {
		Random x = new Random();
		distance = 100 + x.Next(4000);
		energy = 1000 + x.Next(2000);
		
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
	}

}
