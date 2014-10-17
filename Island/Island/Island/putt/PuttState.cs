using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;


/*
 * Welcome to Lee Carvallo's Putting Challenge.
 * I am Carvallo. Now, choose a club. 
 * (Beep) You have chosen a three wood. May I suggest a putter? 
 * (Beep) Three wood. Now enter the force of your swing. I suggest feather touch. 
 * (Beep, beep, beep) You have entered "power drive". Now, push seven eight seven to swing.
 * (Beep beep beep) "Ball is in...parking lot. Would you like to play again? 
 * (Beep) You have selected, No
 * 
 
 */
namespace Island
{
    public class PuttState : FlxState
    {
        public FlxSprite game;

        private List<string> clubs;
        private List<string> force;
        private List<string> playAgain;

        private bool suggestionForClubNoted;
        private bool suggestionForForceNoted;


        private Ball ball;
        private Hole hole;

        private FlxText text;

        private int selected;

        private Lee lee;

        public enum GameState
        {
            ChooseClub = 0,
            ChooseForce = 1,
            Swing = 2,
            BallInPlay = 3,
            PlayAgain = 4
        }

        public GameState state = GameState.ChooseClub;

        override public void create()
        {
            base.create();

            selected = 0;
            suggestionForClubNoted = false;
            suggestionForForceNoted = false;

            clubs = new List<string> { "Putter", "1 Wood", "2 Wood", "3 Wood", "4 Wood", "5 Wood", 
                "1 Iron", "2 Iron", "3 Iron", "4 Iron", "5 Iron", "6 Iron", "7 Iron", "8 Iron", "9 Iron", 
                "Sand Wedge" };

            force = new List<string> { "Feather Touch", "Power Drive", "Chip Shot", 
                "Pitch", "Fade", "Draw", 
                "Lay-up", "Knock Down", "Flop" };

            playAgain = new List<string> { "Yes", "No" };

            game = new FlxSprite(0, 0);
            game.loadGraphic("putt/bg", true, false, 256, 224);
            add(game);

            text = new FlxText(2, 2, 200);
            text.setFormat(null, 1, Color.White, FlxJustification.Left, Color.Black);
            text.text = state.ToString();
            add(text);

            ball = new Ball(FlxG.width/2, FlxG.height - 10);
            add(ball);

            hole = new Hole(FlxG.width / 2, FlxG.height / 2);
            add(hole);

            lee = new Lee(FlxG.width / 5, FlxG.height - 140);
            add(lee);

        }

        override public void update()
        {
            text.text = state.ToString();

            if (FlxControl.LEFTJUSTPRESSED)
            {
                selected--;

                if (selected < 0) selected = 0;
            }
            if (FlxControl.RIGHTJUSTPRESSED)
            {
                selected++;
            }

            if (elapsedInState > 1.0f)
            {
                /// Now, choose a club. 
                if (state == GameState.ChooseClub)
                {

                    text.text = clubs[selected].ToString();

                    if (FlxControl.ACTIONJUSTPRESSED && (selected==0 || suggestionForClubNoted))
                    {
                        selected = 0;
                        state = GameState.ChooseForce;
                        return;
                    }
                    else if (FlxControl.ACTIONJUSTPRESSED)
                    {
                        text.text = "May I suggest a Putter";

                        suggestionForClubNoted = true;
                    }

                }
                /// Now enter the force of your swing. I suggest feather touch.  
                if (state == GameState.ChooseForce)
                {
                    text.text = force[selected].ToString();

                    if (FlxControl.ACTIONJUSTPRESSED && (selected == 0 || suggestionForForceNoted))
                    {
                        selected = 0;
                        state = GameState.Swing;
                        return;
                    }
                    else if (FlxControl.ACTIONJUSTPRESSED)
                    {
                        text.text = "I suggest feather touch.";

                        suggestionForForceNoted = true;
                    }
                }
                /// Now, push seven eight seven to swing.
                if (state == GameState.Swing)
                {
                    if (FlxControl.ACTIONJUSTPRESSED)
                    {
                        ball.velocity.Y = -60;

                        state = GameState.BallInPlay;
                        return;
                    }
                }
                if (state == GameState.BallInPlay)
                {
                    FlxU.overlap(ball, hole, ballInHole);

                    if (ball.velocity.Y == 0)
                    {
                        selected = 0;
                        state = GameState.PlayAgain;
                        return;
                    }
                }
                ///Would you like to play again? 
                if (state == GameState.PlayAgain)
                {
                    text.text = playAgain[selected].ToString();

                    if (FlxControl.ACTIONJUSTPRESSED && selected==0)
                    {
                        /// You have selected Yes;
                        state = GameState.ChooseClub;
                        return;
                    }
                    if (FlxControl.ACTIONJUSTPRESSED && selected == 1)
                    {
                        /// You have selected No;
                        FlxG.state = new MenuState();
                        return;
                    }

                }

            }



            base.update();
        }

        protected bool ballInHole(object Sender, FlxSpriteCollisionEvent e)
        {
            state = GameState.PlayAgain;
            ball.visible = false;

            return true;
        }
    }
}
