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

            addAnimation("flow", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 12);

            play("flow");

            power = 1;

            Console.WriteLine("Roll indicator {0}", Style);




        }

        override public void update()
        {


            base.update();

        }


    }
}
