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
    class Ball : FlxSprite
    {

        public Ball(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("diagnostic/testpalette", false, false, 8, 8);
            setDrags(10, 10);
        }

        override public void update()
        {
            scale = (float)Convert.ToDouble((y / FlxG.height));

            base.update();

        }


    }
}
