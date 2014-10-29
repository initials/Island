using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Island
{
    class Carrot : FlxSprite
    {

        public Carrot(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("putt/carrot_9x9", true, false, 9, 9);
            addAnimation("play", new int[] { 0, 1, 2, 3, 4 }, 8);

            addAnimation("pulse", new int[] { 5, 6, 7, 8, 9, 10 }, 8);

            play("play");

        }

        override public void update()
        {


            base.update();

        }


    }
}
