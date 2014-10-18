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
    class Aim : FlxSprite
    {

        public bool startAim;
        private int speed;
        private bool direction;
        private bool hdirection;

        public Aim(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("initials/crosshair");

            direction = false;
            hdirection = false;
            speed = 2;
            startAim = false;

        }

        override public void update()
        {
            if (startAim)
            {
                visible = true;
                if (direction) x += speed;
                else x -= speed;

                if (x < 0)
                {
                    x = 1;
                    direction = !direction;
                }
                if (x > FlxG.width)
                {
                    x = FlxG.width - 2;
                    direction = !direction;
                }

            }
            else
            {
                visible = false;
            }

            if (hdirection) health += 1;
            else health -= 1;
            if (health < 0)
            {
                health = 1;
                hdirection = !hdirection;
            }
            if (health > 50 )
            {
                health = 50 - 1;
                hdirection = !hdirection;
            }



            base.update();

        }


    }
}
