using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace Island
{
    public class LoadRomState : FlxState
    {

        public List<string> games;
        public FlxText gamesDisplay;
        public FlxText status;

        public int selection;

        override public void create()
        {
            base.create();

            selection = 0;

            games = new List<string> ();
            games.Add("Lee Carvallo's Putting Challenge");
            games.Add("Bonestorm");
            games.Add("Bonestorm II");
            games.Add("Bonesquad");
            games.Add("Billy Graham's Bible Blaster");
            games.Add("Bowling 2000");
            games.Add("Chevy Chase Foul Play");
            games.Add("Disembowler IV - The game where condemned criminals dig at each other with rusty hooks.");
            games.Add("Escape from Death Row");
            games.Add("Escape from Grandma's House");
            games.Add("Escape from Grandma's House II");
            games.Add("Larry the Looter");
            games.Add("Polybius");
            games.Add("Super Slugfest");
            games.Add("Tandem Bike Ride With Your Mom");
            games.Add("Triangle Wars");
            
            //games.Add("");


            gamesDisplay = new FlxText(2, 12, 200);
            //text1.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 3, new Color(237, 0, 142), FlxJustification.Center, Color.Black);
            gamesDisplay.setFormat(FlxG.Content.Load<SpriteFont>("initials/SMALL_PIXEL"), 1, Color.White, FlxJustification.Left, Color.Black); //FlxG.Content.Load<SpriteFont> ("")
            add(gamesDisplay);

            status = new FlxText(2, 2, 200);
            status.setFormat(FlxG.Content.Load<SpriteFont>("initials/SMALL_PIXEL"), 1, Color.Red, FlxJustification.Left, Color.Black); //FlxG.Content.Load<SpriteFont> ("")
            add(status);

        }

        override public void update()
        {
            gamesDisplay.text = "";
            int current = 0;

            foreach (var item in games)
            {
                if (current == selection) gamesDisplay.text += ">";

                gamesDisplay.text += item + "\n";
                current++;
            }

            if (FlxControl.UPJUSTPRESSED) selection--;
            if (FlxControl.DOWNJUSTPRESSED) selection++;
            if (FlxControl.ACTIONJUSTPRESSED)
            {
                if (selection == 0)
                {
                    FlxG.state = new MenuState();
                    status.text = "CHECKSUM GOOD! - LOADING ROM.";
                }
                else
                {
                    Console.WriteLine("Cannot load ROM");
                    status.text = "INVALID CHECKSUM - CANNOT LOAD ROM";
                }
            }

            base.update();
        }


    }
}
