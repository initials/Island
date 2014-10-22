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
    class CarPark : FlxSprite
    {
        private FlxSprite ball;
        private List<Vector2> ballPositions;
        private int ballPositionCounter;
        private bool record;

        private FlxPath _path;

        public CarPark(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("putt/carPark", true, false, 256, 224);

            _path = new FlxPath();
            _path.add(0, 0);
            _path.add(13,28);
            _path.add(39,19);
            _path.add(63,55);
            _path.add(84,104);
            _path.add(83,144);
            _path.add(103,175);
            _path.add(105,130);
            _path.add(110,103);
            _path.add(132,81);
            _path.add(156,70);
            _path.add(165,81);
            _path.add(173,102);
            _path.add(176,124);
            _path.add(179,141);
            _path.add(181,139);
            _path.add(186,115);
            _path.add(201,76);
            _path.add(225,61);
            _path.add(800, 33);
            
            
            
            

            record = false;

            ball = new FlxSprite(0, 0);
            ball.loadGraphic("putt/ball_8x8", true, false, 8, 8);
            ball.frame = 2;

            ballPositionCounter = 0;

            ball.pathCornering = 100;
            


            ballPositions = new List<Vector2>();

            ballPositions.Add(new Vector2(8, 31));
            ballPositions.Add(new Vector2(8, 31));
            ballPositions.Add(new Vector2(8, 31));
            ballPositions.Add(new Vector2(7, 31));
            ballPositions.Add(new Vector2(7, 31));
            ballPositions.Add(new Vector2(7, 31));
            ballPositions.Add(new Vector2(7, 31));
            ballPositions.Add(new Vector2(7, 30));
            ballPositions.Add(new Vector2(7, 29));
            ballPositions.Add(new Vector2(11, 27));
            ballPositions.Add(new Vector2(17, 24));
            ballPositions.Add(new Vector2(20, 23));
            ballPositions.Add(new Vector2(23, 22));
            ballPositions.Add(new Vector2(27, 22));
            ballPositions.Add(new Vector2(31, 23));
            ballPositions.Add(new Vector2(37, 24));
            ballPositions.Add(new Vector2(43, 26));
            ballPositions.Add(new Vector2(48, 28));
            ballPositions.Add(new Vector2(53, 30));
            ballPositions.Add(new Vector2(58, 32));
            ballPositions.Add(new Vector2(64, 36));
            ballPositions.Add(new Vector2(73, 44));
            ballPositions.Add(new Vector2(79, 49));
            ballPositions.Add(new Vector2(86, 55));
            ballPositions.Add(new Vector2(92, 62));
            ballPositions.Add(new Vector2(97, 70));
            ballPositions.Add(new Vector2(101, 80));
            ballPositions.Add(new Vector2(107, 95));
            ballPositions.Add(new Vector2(109, 106));
            ballPositions.Add(new Vector2(113, 118));
            ballPositions.Add(new Vector2(116, 130));
            ballPositions.Add(new Vector2(121, 140));
            ballPositions.Add(new Vector2(126, 149));
            ballPositions.Add(new Vector2(132, 161));
            ballPositions.Add(new Vector2(136, 167));
            ballPositions.Add(new Vector2(138, 170));
            ballPositions.Add(new Vector2(139, 172));
            ballPositions.Add(new Vector2(139, 173));
            ballPositions.Add(new Vector2(139, 169));
            ballPositions.Add(new Vector2(137, 166));
            ballPositions.Add(new Vector2(135, 161));
            ballPositions.Add(new Vector2(134, 154));
            ballPositions.Add(new Vector2(133, 146));
            ballPositions.Add(new Vector2(132, 138));
            ballPositions.Add(new Vector2(131, 125));
            ballPositions.Add(new Vector2(130, 117));
            ballPositions.Add(new Vector2(130, 108));
            ballPositions.Add(new Vector2(130, 100));
            ballPositions.Add(new Vector2(131, 92));
            ballPositions.Add(new Vector2(137, 87));
            ballPositions.Add(new Vector2(141, 80));
            ballPositions.Add(new Vector2(144, 78));
            ballPositions.Add(new Vector2(146, 77));
            ballPositions.Add(new Vector2(149, 76));
            ballPositions.Add(new Vector2(152, 76));
            ballPositions.Add(new Vector2(156, 76));
            ballPositions.Add(new Vector2(161, 78));
            ballPositions.Add(new Vector2(163, 82));
            ballPositions.Add(new Vector2(164, 88));
            ballPositions.Add(new Vector2(165, 95));
            ballPositions.Add(new Vector2(166, 103));
            ballPositions.Add(new Vector2(166, 116));
            ballPositions.Add(new Vector2(166, 123));
            ballPositions.Add(new Vector2(168, 128));
            ballPositions.Add(new Vector2(169, 130));
            ballPositions.Add(new Vector2(171, 130));
            ballPositions.Add(new Vector2(171, 130));
            ballPositions.Add(new Vector2(172, 128));
            ballPositions.Add(new Vector2(172, 122));
            ballPositions.Add(new Vector2(173, 116));
            ballPositions.Add(new Vector2(175, 110));
            ballPositions.Add(new Vector2(177, 103));
            ballPositions.Add(new Vector2(179, 96));
            ballPositions.Add(new Vector2(182, 85));
            ballPositions.Add(new Vector2(184, 79));
            ballPositions.Add(new Vector2(186, 73));
            ballPositions.Add(new Vector2(189, 67));
            ballPositions.Add(new Vector2(192, 61));
            ballPositions.Add(new Vector2(199, 54));
            ballPositions.Add(new Vector2(201, 51));
            ballPositions.Add(new Vector2(204, 48));
            ballPositions.Add(new Vector2(207, 45));
            ballPositions.Add(new Vector2(211, 44));
            ballPositions.Add(new Vector2(213, 44));
            ballPositions.Add(new Vector2(216, 44));
            ballPositions.Add(new Vector2(220, 45));
            ballPositions.Add(new Vector2(224, 49));
            ballPositions.Add(new Vector2(227, 53));
            ballPositions.Add(new Vector2(230, 60));
            ballPositions.Add(new Vector2(232, 68));
            ballPositions.Add(new Vector2(234, 75));
            ballPositions.Add(new Vector2(235, 77));
            ballPositions.Add(new Vector2(235, 77));
            ballPositions.Add(new Vector2(235, 77));
            ballPositions.Add(new Vector2(235, 75));
            ballPositions.Add(new Vector2(235, 69));
            ballPositions.Add(new Vector2(234, 62));
            ballPositions.Add(new Vector2(233, 56));
            ballPositions.Add(new Vector2(233, 50));
            ballPositions.Add(new Vector2(235, 44));
            ballPositions.Add(new Vector2(238, 37));
            ballPositions.Add(new Vector2(243, 31));
            ballPositions.Add(new Vector2(252, 21));
            ballPositions.Add(new Vector2(262, 15));
            ballPositions.Add(new Vector2(272, 10));
            ballPositions.Add(new Vector2(280, 7));
            ballPositions.Add(new Vector2(287, 5));
            ballPositions.Add(new Vector2(292, 4));

            ballPositionCounter = 0;
            
        }

        override public void update()
        {

            if (FlxG.mouse.justPressed())
            {
                
            }
            if (record)
            {
                if (visible && FlxG.mouse.justPressed())
                {
                    
                    //Console.WriteLine("ballPositions.Add(new Vector2(" + FlxG.mouse.x + "," + FlxG.mouse.y + "));");
                    //_path.add(0,0);


                    Console.WriteLine("_path.add(" + FlxG.mouse.x + "," + FlxG.mouse.y + ");");

                }
            }
            else
            {

                if (ball.x > 224)
                {
                    visible = false;
                }

                //ball.x = ballPositions[ballPositionCounter].X;
                //ball.y = ballPositions[ballPositionCounter].Y;
                //if (visible)
                //{
                //    ballPositionCounter++;
                //}

                //if (ballPositionCounter > ballPositions.Count - 1)
                //{
                //    ballPositionCounter = 0;
                //    visible = false;
                //}
            }

            ball.update();

            base.update();

        }

        public void setToVisible()
        {
            ball.followPath(_path, 275, PATH_LOOP_FORWARD, true);
            visible = true;
        }

        public override void render(SpriteBatch spriteBatch)
        {

            base.render(spriteBatch);
            ball.render(spriteBatch);

        }


    }
}
