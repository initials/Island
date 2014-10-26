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

        public RollIndicator(int xPos, int yPos, int Style)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("putt/rollAnim_8x8"), true, false, 8, 8);

            addAnimation("flow1", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 4);
            addAnimation("flow2", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 8);
            addAnimation("flow3", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 12);
            addAnimation("flow4", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 16);

            
            boundingBoxOverride = false;
            

            //power = 1;

            //Console.WriteLine("Roll indicator {0}", Style);

            power = ((Style / 10) + 1);
            int direction = Style % 10;
            //float multiplier = 1;
            //Console.WriteLine("Adjusting speed for Tile Power {0} Direction l,u,r,d: {1}", power, direction);

            if (power == 1)
            {
                color = Color.White;
            }
            else if (power == 2)
            {
                color = Color.Yellow;
            }
            else if (power == 3)
            {
                color = Color.Orange;
            }
            else if (power >= 4)
            {
                color = Color.Red;
            }
            play("flow" + power.ToString() );

            // Left
            if (direction == 0)
            {

            }
            // Up
            if (direction == 2)
            {
                angle = 90;
            }
            // Right
            if (direction == 4)
            {
                angle = 180;
            }
            // Down
            if (direction == 6)
            {
                angle = 270;
            }
            // Left/Up
            if (direction == 1)
            {
                angle = 45;
            }
            // Up/Right
            if (direction == 3)
            {
                angle = 135;
            }
            // Right/Down
            if (direction == 5)
            {
                angle = 215;
            }
            // Down/Left
            if (direction == 7)
            {
                angle = 315;
            }




        }

        override public void update()
        {


            base.update();

        }


    }
}
