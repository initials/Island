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
    class RollIndicator : FlxSprite
    {

        public int power;

        public RollIndicator(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("putt/rollIndicators"), true, false, 8, 8);
            power = 1;

        }

        override public void update()
        {


            base.update();

        }


    }
}
