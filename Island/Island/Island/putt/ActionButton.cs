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
    class ActionButton : FlxSprite
    {

        public ActionButton(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("putt/golfball_50x50"), true, false, 50, 50);

        }

        override public void update()
        {
            if (scale > 1.0f)
            {
                scale -= 0.05f;
            }
            else
            {
                scale = 1;
            }

            base.update();
        }

        protected bool overlapCursor(object Sender, FlxSpriteCollisionEvent e)
        {
            if (FlxG.mouse.pressed())
                scale = 0.9f;
            else 
                scale = 1.1f;
            return true;
        }
    }
}
