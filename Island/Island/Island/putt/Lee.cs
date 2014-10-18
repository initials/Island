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
    class Lee : FlxSprite
    {

        public Lee(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("putt/lee_150x150"), true, false, 150, 150);
            loadAnimationsFromGraphicsGaleCSV("content/putt/lee_150x150.csv", null, null, true);

            boundingBoxOverride = false;


        }

        override public void update()
        {


            base.update();

        }


    }
}
