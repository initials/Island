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

        override public void create()
        {
            base.create();

            for (int i = 0; i < Globals.scoreCard.Count; i++)
            {
                Console.WriteLine("Score for hole {0}: {1}", i, Globals.scoreCard[i]);
            }

        }

        override public void update()
        {




            base.update();
        }


    }
}
