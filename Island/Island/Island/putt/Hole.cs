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
    class Hole : FlxSprite
    {

        public Hole(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("putt/hole"), true, false, 15, 40);

            width = 5;
            height = 2;

            setOffset(10, 38);



        }

        override public void update()
        {


            base.update();

        }


    }
}
