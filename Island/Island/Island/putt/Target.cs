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
    class Target : FlxSprite
    {

        public Target(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("putt/target_8x8"), true, false, 8, 8);

            addAnimation("animation", new int[] { 0,1,2,3 }, 12, true);

            play("animation");
        }

        override public void update()
        {


            base.update();

        }


    }
}
