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
            loadGraphic("putt/ball_8x8", false, false, 8, 8);
            loadAnimationsFromGraphicsGaleCSV("content/putt/ball_8x8.csv");
            play("size2");


            setDrags(10, 10);
            width = 4;
            height = 4;
            setOffset(2, 2);


        }

        override public void update()
        {
            scale = (float)Convert.ToDouble((y / FlxG.height));
            if (scale < 0.75f) play("size3");
            else if (scale < 0.5f) play("size4");


            if (y < FlxG.height / 2)
            {
                setDrags(44, 44);
            }
            else
            {
                setDrags(10, 10);
            }

            scale = 1;




            base.update();

        }


    }
}
