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
            loadGraphic(FlxG.Content.Load<Texture2D>("putt/hole_25x50"), true, false, 25, 50);
            loadAnimationsFromGraphicsGaleCSV("content/putt/hole_25x50.csv", null, null, true);
            play("flapInWind");

            width = 6;
            height = 3;

            setOffset(15, 47);



        }

        override public void update()
        {


            base.update();

        }


    }
}
