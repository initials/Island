using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using org.flixel;

using System.Linq;
using System.Xml.Linq;


/*
 * Welcome to Lee Carvallo's Putting Challenge. I am Carvallo.
 * Now, choose a club. 
 * (Beep) You have chosen a three wood. May I suggest a putter? 
 * (Beep) Three wood. 
 * Now enter the force of your swing. I suggest feather touch. 
 * (Beep, beep, beep) You have entered "power drive". 
 * Now, push seven eight seven to swing.
 * (Beep beep beep) "Ball is in...parking lot. 
 * Would you like to play again? 
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

        private Aim aim;
        private FlxBar power;
        private int framesElapsed;

        private FlxSound sound;


        public enum GameState
        {
            Introduction = -1,
            ChooseClub = 0,
            ChooseForce = 1,
            Swing = 2,
            Power = 3,
            BallInPlay = 4,
            PlayAgain = 5,
            Reset = 6
        }

        public GameState state = GameState.Introduction;

        override public void create()
        {
            base.create();

            framesElapsed = 0;

            selected = 0;
            suggestionForClubNoted = false;
            suggestionForForceNoted = false;

            clubs = new List<string> { "Putter", "1 Wood", "3 Wood", "5 Wood", 
                "1 Iron", "2 Iron", "3 Iron", "4 Iron", "5 Iron", "6 Iron", "7 Iron", "8 Iron", "9 Iron", 
                "Sand Wedge" };

            force = new List<string> { "Feather Touch", "Power Drive", "Chip Shot", 
                "Pitch", "Fade", "Draw", 
                "Lay-up", "Knock Down", "Flop" };

            playAgain = new List<string> { "Yes", "No" };

            game = new FlxSprite(0, 0);
            game.loadGraphic("putt/bg", true, false, 256, 224);
            game.boundingBoxOverride = false;

            add(game);

            text = new FlxText(2, 2, 200);
            text.setFormat(null, 1, Color.White, FlxJustification.Left, Color.Black);
            //text.text = state.ToString();
            add(text);

            ball = new Ball(FlxG.width/2, FlxG.height - 10);
            add(ball);

            hole = new Hole(FlxG.width / 2, FlxG.height / 2);
            add(hole);

            lee = new Lee(FlxG.width / 6, FlxG.height - 170);
            add(lee);

            aim = new Aim(1, FlxG.height / 2);
            
            add(aim);

            power = new FlxBar(5, FlxG.height - 20, FlxBar.FILL_LEFT_TO_RIGHT, 50, 4, null, "", 0, 50, true);
            add(power);
            power.visible = false;

            log("Welcome to Lee Carvallo's Putting Challenge. I am Carvallo.");
            //FlxG.play("putt/sfx/welcome", 1.0f, false);

            sound = new FlxSound();
            sound.loadEmbedded("putt/sfx/welcome", false);
            sound.play();
            

            //s = FlxG.Content.Load<SoundEffect>("putt/sfx/welcome");

            //s.Play();

        }

        public void log(string Log)
        {
            Console.WriteLine("Voice: " + Log);
        }

        public void resetSelections()
        {
            selected = 0;
            framesElapsed = 0;
        }

        public void everyAction()
        {
            FlxG.play("putt/sfx/blip");
        }
        public void chooseClub()
        {
            if (selected > clubs.Count - 1) selected = 0;
            lee.debugName = "";

            if (framesElapsed == 3)
            {
                log("Now, choose a club. ");
                
                playSound("chooseaclub");

            }

            ball.reset(FlxG.width / 2 - 7, FlxG.height - 30);
            ball.visible = true;
            ball.velocity.Y = 0;
            ball.velocity.X = 0;
            ball.play("size2");

            text.text = clubs[selected].ToString();

            if (FlxControl.ACTIONJUSTPRESSED && (selected == 0 || suggestionForClubNoted))
            {
                if (selected == 0) {
                    playSound("putter");
                    lee.club="putter";
                }
                else if (selected == 1) {
                    playSound("onewood"); 
                    lee.club="wood";
                }
                else if (selected == 2) {
                    playSound("threewood");
                    lee.club="wood";
                }
                else if (selected == 3) {
                    playSound("fivewood");
                    lee.club="wood";
                }
                else if (selected == 4) {
                    playSound("oneiron");
                    lee.club="iron";
                }
                else if (selected == 5)
                {
                    playSound("twoiron");
                    lee.club = "iron";
                }

                log(clubs[selected].ToString());
                
                state = GameState.ChooseForce;
                resetSelections();
                return;
            }
            else if (FlxControl.ACTIONJUSTPRESSED)
            {
                log("May I suggest a Putter");

                playSound("mayisuggestaputter");

                suggestionForClubNoted = true;
            }
        }

        public void chooseForce()
        {
            if (framesElapsed == 3)
            {
                log("Now enter the force of your swing.");
                playSound("nowenter");
            }

            text.text = force[selected].ToString();

            if (FlxControl.ACTIONJUSTPRESSED && (selected == 0 || suggestionForForceNoted))
            {
                log("You have entered " + force[selected].ToString() );

                if (selected == 0) playSound("feathertouch");
                else if (selected == 1) playSound("powerdrive");
                else if (selected == 2) playSound("chipshot");
                else if (selected == 3) playSound("pitch");
                else if (selected == 4) playSound("fade");
                else if (selected == 5) playSound("draw");

                resetSelections();
                state = GameState.Swing;
                return;
            }
            else if (FlxControl.ACTIONJUSTPRESSED)
            {
                log("I suggest feather touch.");
                playSound("isuggestfeathertouch");

                suggestionForForceNoted = true;
            }
        }

        public void chooseSwing()
        {
            if (framesElapsed == 3)
            {
                log("Now aim your shot.");
            }

            aim.startAim = true;

            if (FlxControl.ACTIONJUSTPRESSED)
            {
                resetSelections();
                aim.startAim = false;
                state = GameState.Power;
                return;
            }
        }

        public void choosePower()
        {

            

            if (framesElapsed == 3)
            {
                log("Now enter the power of your swing.");
                
            }

            power.visible = true;
            power.setValue(aim.health);

           


            if (FlxControl.ACTIONJUSTPRESSED)
            {
                float an = FlxU.getAngle(new Vector2(ball.x + (ball.width / 2), ball.y + (ball.height / 2)), new Vector2(aim.x + (aim.width / 2), aim.y + (aim.height / 2)));

                float an2 = FlxU.getAngle(new Vector2(ball.x, ball.y), new Vector2(aim.x, aim.y));
                float an3 = FlxU.getAngle(new Vector2(ball.x + (ball.width / 1), ball.y + (ball.height / 1)), new Vector2(aim.x + (aim.width / 1), aim.y + (aim.height / 1)));

                Console.WriteLine("Angle to shoot is {0} , {1} , {2}", an, an2, an3);

                ball.angle = an + 90;
                ball.setVelocityFromAngle(aim.health * 2);
                ball.angle = 0;
                aim.startAim = false;

                FlxG.play("putt/sfx/putt", 1.0f, false);

                resetSelections();
                state = GameState.BallInPlay;
                return;
            }
        }

        public void ballInPlay()
        {
            lee.play("swing_wood");

            FlxU.overlap(ball, hole, ballInHole);

            if (ball.velocity.Y == 0)
            {
                resetSelections();
                state = GameState.PlayAgain;
                return;
            }
        }

        public void choosePlayAgain()
        {
            if (framesElapsed == 3)
            {
                log("Would you like to play again?");
                playSound("wouldyouliketoplayagain");

            }

            text.text = playAgain[selected].ToString();

            if (FlxControl.ACTIONJUSTPRESSED && selected == 0)
            {
                log("You have selected Yes");
                playSound("youhaveselectedyes");

                state = GameState.Reset;
                return;
            }
            if (FlxControl.ACTIONJUSTPRESSED && selected == 1)
            {
                playSound("youhaveselectedno");
                log("You have selected No");
                
                state = GameState.Reset;
                return;
            }
        }

        public void chooseReset()
        {

            if (sound.getState() == SoundState.Stopped)
            {
                if (selected == 0)
                {
                    resetSelections();
                    hole.reset(FlxU.random(20, FlxG.width - 20), hole.y);

                    suggestionForClubNoted = false;
                    suggestionForForceNoted = false;

                    state = GameState.ChooseClub;
                    return;
                }
                else if (selected == 1)
                {
                    resetSelections();
                    FlxG.state = new MenuState();
                    return;

                }
            }

        }
        public void playSound(string Sound)
        {
            sound.loadEmbedded("putt/sfx/" + Sound, false);
            sound.play();
        }

        override public void update()
        {
            
            if (FlxG.keys.justPressed(Keys.B))
            {
                FlxG.showBounds = !FlxG.showBounds;
            }

            if (FlxControl.ACTIONJUSTPRESSED)
            {
                everyAction();
            }

            //------------------------------------------------------------------
            //Console.WriteLine(sound.getName());

            if (state == GameState.Introduction)
            {
                if (sound.getState() == SoundState.Stopped)
                {
                    lee.debugName = "introduction";

                    playSound("iamcarvallo");

                    state = GameState.ChooseClub;

                    return;
                }
            }

            if (sound.getState() == SoundState.Stopped)
            {
                if (lee.club == "")
                    lee.play("idle");
                else
                    lee.play( "idle_" + lee.club );

                //lee.play("idle");
                if (FlxControl.LEFTJUSTPRESSED)
                {
                    FlxG.play("putt/sfx/blip");

                    selected--;
                    if (selected < 0) selected = 0;
                }
                if (FlxControl.RIGHTJUSTPRESSED || FlxG.mouse.justPressed())
                {
                    FlxG.play("putt/sfx/blip");

                    selected++;
                }


                framesElapsed++;

                /// Now, choose a club. 
                if (state == GameState.ChooseClub)
                {
                    chooseClub();
                }
                /// Now enter the force of your swing. I suggest feather touch.  
                else if (state == GameState.ChooseForce)
                {
                    chooseForce();
                }
                /// Now, push seven eight seven to swing.
                else if (state == GameState.Swing)
                {
                    chooseSwing();
                }
                else if (state == GameState.Power)
                {
                    choosePower();
                }
                else if (state == GameState.BallInPlay)
                {
                    ballInPlay();
                }
                ///Would you like to play again? 
                else if (state == GameState.PlayAgain)
                {
                    choosePlayAgain();
                }
                else if (state == GameState.Reset)
                {
                    chooseReset();
                }

            }
            else
            {
                if (lee.debugName == "")
                    lee.play("talk");
                else
                    lee.play(lee.debugName + "_talk");

            }

            base.update();
        }

        protected bool ballInHole(object Sender, FlxSpriteCollisionEvent e)
        {
            if (ball.visible)
            {
                FlxG.play("putt/sfx/ballinhole");
            }
            state = GameState.PlayAgain;
            ball.visible = false;
            resetSelections();
            return true;
        }
    }
}
