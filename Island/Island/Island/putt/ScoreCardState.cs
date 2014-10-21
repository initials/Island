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
    public class ScoreCardState : FlxState
    {
        private FlxText text;
        private bool toStart;


        override public void create()
        {
            FlxG.backColor = Color.White;

            base.create();

            

            text = new FlxText(2, 2, 200);
            text.setFormat(FlxG.Content.Load<SpriteFont>("initials/SMALL_PIXEL"), 1, Color.Black, FlxJustification.Left, Color.White);
            add(text);
            text.text = "Scorecard:\n";

            int total = 0;

            for (int i = 0; i < 18; i++)
            {
                Console.WriteLine("Score for hole {0}: {1}", i+1, Globals.scoreCard[i]);

                text.text += string.Format("{0}: {1}, ", i+1, Globals.scoreCard[i]);

                if (Globals.scoreCard[i] == 1) total++;

                if (i % 3==2) text.text += "\n";

            }

            if (total < 9) text.text += "\nSix time national champion Carvello urges\nyou to keep practicing\nand improve your\nputting game.";
            else if (total < 18) text.text += "\nYour putting game is improving\nrapidly.\nKeep it up, kid. ";
            else if (total < 100) text.text += "\nBonestorm.rom unlocked.";

        }

        override public void update()
        {


            if (FlxControl.ACTIONJUSTPRESSED || FlxG.mouse.pressed())
            {
                if (elapsedInState > 0.15f)
                {
                    FlxG.play("putt/sfx/blip");
                    toStart = true;
                    elapsedInState = 0;
                }
            }

            if (toStart)
            {
                text.color = FlxColor.randomColor();

                if (elapsedInState > 0.25f)
                {
                    FlxG.stopMp3();
                    Globals.canSkip = false;
                    Globals.hole = 1;
                    FlxG.state = new MenuState();
                }
            }


            base.update();
        }


    }
}
