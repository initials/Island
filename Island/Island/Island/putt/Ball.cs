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
        public float heightOffGround;
        private FlxSprite shadow;
        public bool rise;

        public Ball(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("putt/ball_8x8", false, false, 8, 8);
            loadAnimationsFromGraphicsGaleCSV("content/putt/ball_8x8.csv");
            play("size2");


            setDrags(10, 10);
            width = 4;
            height = 2;
            setOffset(2, 6);

            heightOffGround = 0;

            shadow = new FlxSprite(xPos, yPos);
            shadow.loadGraphic("putt/ball_8x8", false, false, 8, 8);
            shadow.color = Color.Black;
            shadow.alpha = 0.1f;
            rise = false;

        }

        override public void update()
        {
            //shadow

            if (rise)
            {
                velocity.Y -= 30;
                heightOffGround += 0.2f;
            }

            shadow.at(this);
            shadow.y += heightOffGround;


            scale = (float)Convert.ToDouble((y / FlxG.height));
            if (scale > 0.75f)
            {
                play("size2");
                width = 4;
                height = 2;
                setOffset(2, 6);
            }

            else if (scale > 0.5f) { 
                play("size3");
                width = 4;
                height = 4;
                setOffset(2, 4);
            }
            else if (scale > 0.4f)
            {
                play("size4");
                width = 2;
                height = 2;
                setOffset(3, 3);

            }
            else play("size5");


            //Console.WriteLine(scale);

            if (y < FlxG.height / 2)
            {
                setDrags(44, 44);
            }
            else if (y < (FlxG.height / 2) - 40)
            {
                setDrags(144, 144);
                
            }

            else
            {
                setDrags(10, 10);
            }

            scale = 1;


            if (y < 0)
            {
                setDrags(50000, 50000);
            }

            base.update();

        }

        public override void render(SpriteBatch spriteBatch)
        {
            if (rise && y > FlxG.height/2)
                shadow.render(spriteBatch);
            base.render(spriteBatch);
        }

        public void adjustSpeedForTile(int Tile)
        {

            int power = (Tile / 10) + 1;
            int direction = Tile % 10;
            float multiplier = 1;
            Console.WriteLine("Adjusting speed for Tile Power {0} Direction l,u,r,d: {1}", power, direction);
            
            // Left
            if (direction == 0)
            {
                velocity.X -= (power * multiplier);
            }
            // Up
            if (direction == 2)
            {
                velocity.Y -= (power * multiplier);
            }
            // Right
            if (direction == 4)
            {
                velocity.X += (power * multiplier);
            }
            // Down
            if (direction == 6)
            {
                velocity.Y += (power * multiplier);
            }
            // Left/Up
            if (direction == 1)
            {
                velocity.X -= (power * (multiplier/3));
                velocity.Y -= (power * (multiplier/3));
            }
            // Up/Right
            if (direction == 3)
            {
                velocity.Y -= (power * (multiplier/3));
                velocity.X += (power * (multiplier/3));
            }
            // Right/Down
            if (direction == 5)
            {
                velocity.X += (power * (multiplier/3));
                velocity.Y += (power * (multiplier/3));
            }
            // Down/Left
            if (direction == 7)
            {
                velocity.Y += (power * (multiplier/3));
                velocity.X -= (power * (multiplier/3));
            }



        }

    }
}
