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
        public bool startHealth;

        private int speed;
        private int hspeed;

        private bool direction;
        private bool hdirection;

        public Aim(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("initials/crosshair");

            direction = false;
            hdirection = false;
            speed = 1;
            hspeed = 1;
            startAim = false;
            startHealth = false;

        }

        override public void update()
        {

            if (Globals.hole < 3)
            {
                speed = 1;
            }
            else if (Globals.hole < 12)
            {
                speed = 2;
            }
            else if (Globals.hole < 18)
            {
                speed = 3;
            }

            if (Globals.hole < 6)
            {
                hspeed = 1;
            }
            else if (Globals.hole < 14)
            {
                hspeed = 2;
            }
            else if (Globals.hole < 18)
            {
                hspeed = 3;
            }

            if (startAim)
            {
                visible = true;
                if (direction) x += speed;
                else x -= speed;

                if (x < 0)
                {
                    FlxG.play("putt/sfx/select");

                    x = 1;
                    direction = !direction;
                }
                if (x > FlxG.width)
                {
                    FlxG.play("putt/sfx/select");


                    x = FlxG.width - 2;
                    direction = !direction;
                }

            }
            else
            {
                visible = false;
            }

            if (startHealth)
            {
                if (hdirection) health += hspeed;
                else health -= hspeed;
                if (health < 0)
                {
                    FlxG.play("putt/sfx/select");
                    health = 1;
                    hdirection = !hdirection;
                }
                if (health > 50)
                {
                    FlxG.play("putt/sfx/select");
                    health = 50 - 1;
                    hdirection = !hdirection;
                }
            }



            base.update();

        }


    }
}
