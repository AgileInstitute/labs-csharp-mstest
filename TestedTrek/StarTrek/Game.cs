using System;
using System.Collections.Generic;
using Untouchables;

public class Game {

	private int e = 10000;
	private int t = 8;

    public int EnergyRemaining() {
        return e;
    }

    public int Torpedoes {
        set {
            t = value;
        }
        get {
            return t;
        }
    }

    public void FireWeapon(WebGadget wg) {
        FireWeapon(new Galaxy(wg));
    }

    public void FireWeapon(Galaxy wg) {
        if (wg.Parameter("command").Equals("phaser")) {
			int amount = int.Parse(wg.Parameter("amount"));
			Klingon enemy = (Klingon) wg.Variable("target");
			if (e >= amount) {
				int distance = enemy.Distance();
				if (distance > 4000) {
					wg.WriteLine("Klingon out of range of phasers at " + distance + " sectors...");
				} else {
					int damage = amount - (((amount /20)* distance /200) + Rnd(200));
					if (damage < 1)
						damage = 1;
					wg.WriteLine("Phasers hit Klingon at " + distance + " sectors with " + damage + " units");
					if (damage < enemy.GetEnergy()) {
						enemy.SetEnergy(enemy.GetEnergy() - damage);
						wg.WriteLine("Klingon has " + enemy.GetEnergy() + " remaining");
					} else {
						wg.WriteLine("Klingon destroyed!");
						enemy.Delete();
					}
				}
				e -= amount;

			} else {
				wg.WriteLine("Insufficient energy to fire phasers!");
			}

		} else if (wg.Parameter("command").Equals("photon")) {
			Klingon enemy = (Klingon) wg.Variable("target");
			if (t  > 0) {
				int distance = enemy.Distance();
				if ((Rnd(4) + ((distance / 500) + 1) > 7)) {
					wg.WriteLine("Torpedo missed Klingon at " + distance + " sectors...");
				} else {
					int damage = 800 + Rnd(50);
					wg.WriteLine("Photons hit Klingon at " + distance + " sectors with " + damage + " units");
					if (damage < enemy.GetEnergy()) {
						enemy.SetEnergy(enemy.GetEnergy() - damage);
						wg.WriteLine("Klingon has " + enemy.GetEnergy() + " remaining");
					} else {
						wg.WriteLine("Klingon destroyed!");
						enemy.Delete();
					}
				}
				t -= 1;

			} else {
				wg.WriteLine("No more photon torpedoes!");
			}
		}
	}


    // note we made generator public in order to mock it
    // it's ugly, but it's telling us something about our *design!* ;-)
	public static Random generator = new Random();
	private static int Rnd(int maximum) {
		return generator.Next(maximum);
	}


}
