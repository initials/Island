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
        private float hspeed;

        private bool direction;
        private bool hdirection;
        
        private FlxGroup points;

        public Vector2 ballPosition;

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

            points = new FlxGroup();
            for (int i = 0; i < 3; i++)
            {
                Target point = new Target(4 * i, 4 * i);
                points.add(point);

            }

            setOffset(-4, -4);

            ballPosition = new Vector2(0, 0);
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
            else if (Globals.hole < 19)
            {
                speed = 3;
            }

            if (Globals.hole < 6)
            {
                hspeed = 1;
            }
            else if (Globals.hole < 14)
            {
                hspeed = 1.5f;
            }
            else if (Globals.hole < 19)
            {
                hspeed = 2;
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


            List<Vector2> l = new List<Vector2>();

            if (this.x + (width / 2) < ballPosition.X)
            {
                //Console.WriteLine("Less Than");
                l = ExtendPoints(new Vector2(this.x + (width / 2), this.y + (height / 2)), ballPosition, 10);
            }
            else
            {
                //Console.WriteLine("More Than");
                l = ExtendPoints2(new Vector2(this.x + (width / 2), this.y + (height / 2) ), new Vector2(ballPosition.X, ballPosition.Y ), 10);
            }

            int i = 0;

            //Console.WriteLine("This item {0}, X {1} Y {2} L Count = {3}, ", i, this.x, this.y, l.Count);

            //foreach (var item in l)
            //{
            //    points.members[i].x = item.X;
            //    points.members[i].y = item.Y;
            //    i++;
            //}

            Vector2 mp = getMidpoint(ballPosition);
            ((FlxSprite)(points.members[0])).x = mp.X;
            ((FlxSprite)(points.members[0])).y = mp.Y;

            Vector2 mp2 = getMidpoint(mp);
            ((FlxSprite)(points.members[1])).x = mp2.X;
            ((FlxSprite)(points.members[1])).y = mp2.Y;

            // Get midpoint between ball and mp

            Vector2 mp3 = FlxU.getMidpoint(mp, ballPosition);
            ((FlxSprite)(points.members[2])).x = mp3.X;
            ((FlxSprite)(points.members[2])).y = mp3.Y;



            foreach (var item in points.members)
            {
                item.update();
            }
            base.update();

        }

        private static List<Vector2> ExtendPoints(Vector2 pt1, Vector2 pt4, int numberOfPoints)
        {
            List<Vector2> extendedPoints = new List<Vector2>();
            extendedPoints.Add(pt1);

            for (double d = 1; d < numberOfPoints - 1; d++)
            {
                double a = (Math.Max(pt1.X, pt4.X) - Math.Min(pt1.X, pt4.X)) * d / (double)(numberOfPoints - 1) + Math.Min(pt1.X, pt4.X);
                double b = (Math.Max(pt1.Y, pt4.Y) - Math.Min(pt1.Y, pt4.Y)) * d / (double)(numberOfPoints - 1) + Math.Min(pt1.Y, pt4.Y);
                var pt2 = new Vector2((float)a, (float)b);
                extendedPoints.Add(pt2);
            }

            extendedPoints.Add(pt4);
            return extendedPoints;
        }

        private static List<Vector2> ExtendPoints2(Vector2 pt1, Vector2 pt4, int numberOfPoints)
        {
            List<Vector2> extendedPoints = new List<Vector2>();
            extendedPoints.Add(pt1);

            for (double d = 1; d < numberOfPoints - 1; d++)
            {
                double a = (Math.Max(pt1.X, pt4.X) - Math.Min(pt1.X, pt4.X)) * d / (double)(numberOfPoints - 1) + Math.Min(pt1.X, pt4.X);
                double b = (Math.Max(pt1.Y, pt4.Y) - Math.Min(pt1.Y, pt4.Y)) * d / (double)(numberOfPoints - 1) + Math.Min(pt1.Y, pt4.Y);
                var pt2 = new Vector2((float)a, (float)b);
                extendedPoints.Add(pt2);
            }

            extendedPoints.Add(pt4);
            return extendedPoints;
        }

        public override void render(SpriteBatch spriteBatch)
        {
            base.render(spriteBatch);
            foreach (var item in points.members)
            {
                item.render(spriteBatch);
            }

        }


    }
}
