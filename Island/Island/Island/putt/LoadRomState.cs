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
        public int selection;

        override public void create()
        {
            base.create();

            selection = 0;

            games = new List<string> ();
            games.Add("Bonestorm");
            games.Add("Bonestorm II");
            games.Add("Bonesquad");
            games.Add("Billy Graham's Bible Blaster");
            games.Add("Lee Carvallo's Putting Challenge");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");
            games.Add("");


            


            gamesDisplay = new FlxText(2, 2, 200);
            gamesDisplay.setFormat(null, 1, Color.White, FlxJustification.Left, Color.Black);
            add(gamesDisplay);



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


            base.update();
        }


    }
}
