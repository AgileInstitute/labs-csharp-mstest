using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Untouchables {
    /// <summary>
    /// Note:  SampleClient is UNTOUCHABLE!
    /// It represents one of hundreds of Game clients, and should not have to change.
    /// </summary>
    public class SampleClient {
        public static void Main() {
            Console.WriteLine("Simple Star Trek");
            WebGadget wg = new WebGadget("phaser", "1000", new Klingon());
            Game game = new Game();
            game.FireWeapon(wg);
            Console.ReadLine();
        }
    }
}
