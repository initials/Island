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
 * Lee Carvallo: Don't do it, son.  How's that game going to help your putting?
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
        private int suggestionForClubStatus;
        private int suggestionForForceStatus;
        private int ballEndedStatus;
        private int selectedPower;

        private Ball ball;
        private Hole hole;

        private FlxText text;

        private int selected;

        private Lee lee;

        private Aim aim;
        private FlxBar power;
        private int framesElapsed;

        private FlxSound sound;
        private FlxGroup rollIndicators;

        private FlxTilemap rollTiles;

        public enum GameState
        {
            Introduction = -1,
            ChooseClub = 0,
            ChooseForce = 1,
            Swing = 2,
            Power = 3,
            BallInPlay = 4,
            BallEnded = 5,
            PlayAgain = 6,
            Reset = 7
        }

        public GameState state = GameState.Introduction;

        override public void create()
        {
            base.create();

            rollIndicators = new FlxGroup();

            framesElapsed = 0;

            selected = 0;
            suggestionForClubNoted = false;
            suggestionForForceNoted = false;
            suggestionForClubStatus = 0;
            suggestionForForceStatus = 0;

            clubs = new List<string> { "Putter", "1 Wood", "3 Wood", "5 Wood", 
                "1 Iron", "2 Iron", "3 Iron", "4 Iron", "5 Iron", "6 Iron", "7 Iron", "8 Iron", "9 Iron", 
                "Sand Wedge" };

            force = new List<string> { "Feather Touch", "Medium Putt", "Firm Putt", "Power Drive", "Chip Shot", 
                "Pitch", "Fade", "Draw", 
                "Lay-up", "Knock Down", "Flop" };

            playAgain = new List<string> { "Yes", "No" };

            game = new FlxSprite(0, 0);
            game.loadGraphic("putt/bg", true, false, 256, 224);
            game.boundingBoxOverride = false;

            add(game);

            text = new FlxText(2, 2, 200);
            text.setFormat(FlxG.Content.Load<SpriteFont>("initials/SMALL_PIXEL"), 1, Color.White, FlxJustification.Left, Color.Black);
            //text.text = state.ToString();
            add(text);



            ball = new Ball(FlxG.width / 2 - 8, FlxG.height - 24);
            add(ball);

            hole = new Hole(FlxG.width / 2, FlxG.height / 2);
            add(hole);

            // load the level.
            loadOgmo();

            lee = new Lee(FlxG.width / 6, FlxG.height - 170);
            add(lee);

            aim = new Aim(1, FlxG.height / 2);

            add(aim);

            power = new FlxBar(5, FlxG.height - 20, FlxBar.FILL_LEFT_TO_RIGHT, 100, 8, null, "", 0, 50, true);
            add(power);
            power.visible = false;

            log("Welcome to Lee Carvallo's Putting Challenge. I am Carvallo.");
            //FlxG.play("putt/sfx/welcome", 1.0f, false);

            sound = new FlxSound();
            sound.loadEmbedded("putt/sfx/welcome", false);
            sound.play();

            if (FlxG.debug && Globals.platform == "touch")
            {
                FlxG.mouse.show();

                ActionButton robot = new ActionButton(FlxG.width - 40, FlxG.height - 40);
                add(robot);
            }
            
            add(rollIndicators);

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

        public void loadOgmo()
        {
            Console.WriteLine("Ogmo Loading level {0}", Globals.hole);

            Dictionary<string, string> ogmo = FlxXMLReader.readAttributesFromOelFile("putt/ogmo/hole" + Globals.hole.ToString() + ".oel", "level/roll");

            rollTiles = new FlxTilemap();
            rollTiles.loadMap(ogmo["roll"], FlxG.Content.Load<Texture2D>("putt/rollIndicators"), 8, 8);
            rollTiles.color = new Color(1, 1, 1, 0.8f);
            add(rollTiles);

            List<Dictionary<string, string>> levelNodes = FlxXMLReader.readNodesFromOelFile("putt/ogmo/hole" + Globals.hole.ToString() + ".oel", "level/bg");

            foreach (Dictionary<string, string> nodes in levelNodes)
            {
                //foreach (KeyValuePair<string, string> kvp in nodes)
                //{
                //    Console.Write("Key = {0}, Value = {1}, ",
                //        kvp.Key, kvp.Value);
                //}
                //Console.Write("\r\n");

                if (nodes["Name"] == "hole")
                {
                    hole.reset(Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]));
                }
            }
        }

        /// <summary>
        /// also load the hole Ogmo level.
        /// </summary>
        public void chooseIntroduction()
        {
            //Console.WriteLine("Introduction --");

            //if (framesElapsed == 2)
            //{
            //    loadOgmo();
            //}
            if (Globals.hole == 1)
            {
                if (lee.debugName == "introduction")
                {

                }
                else
                {
                    lee.debugName = "introduction";
                    playSound("iamcarvallo");
                }
            }
            else
            {
                if (framesElapsed == 1)
                {
                    playSound("hole" + Globals.hole.ToString());
                }
            }
            if (sound.getState() == SoundState.Stopped)
            {
                resetSelections();
                state = GameState.ChooseClub;
                return;
            }
        }

        private void playClubSound(bool AllowSound=true)
        {
            if (selected == 0)
            {
                if (AllowSound) playSound("putter");
                lee.club = "putter";
            }
            else if (selected == 1)
            {
                if (AllowSound) playSound("onewood");
                lee.club = "wood";
            }
            else if (selected == 2)
            {
                if (AllowSound) playSound("threewood");
                lee.club = "wood";
            }
            else if (selected == 3)
            {
                if (AllowSound) playSound("fivewood");
                lee.club = "wood";
            }
            else if (selected == 4)
            {
                if (AllowSound) playSound("oneiron");
                lee.club = "iron";
            }
            else if (selected == 5)
            {
                if (AllowSound) playSound("twoiron");
                lee.club = "iron";
            }
            else if (selected == 6)
            {
                if (AllowSound) playSound("threeiron");
                lee.club = "iron";
            }
            else if (selected == 7)
            {
                if (AllowSound) playSound("fouriron");
                lee.club = "iron";
            }
            else if (selected == 8)
            {
                if (AllowSound) playSound("fiveiron");
                lee.club = "iron";
            }
            else if (selected == 9)
            {
                if (AllowSound) playSound("sixiron");
                lee.club = "iron";
            }
            else if (selected == 10)
            {
                if (AllowSound) playSound("seveniron");
                lee.club = "iron";
            }
            else if (selected == 11)
            {
                if (AllowSound) playSound("eightiron");
                lee.club = "iron";
            }
            else if (selected == 12)
            {
                if (AllowSound) playSound("nineiron");
                lee.club = "iron";
            }
            else if (selected == 13)
            {
                if (AllowSound) playSound("sandwedge");
                lee.club = "wedge";
            }
        }
        public void chooseClub()
        {
            if (selected > clubs.Count - 1) selected = 0;
            lee.debugName = "";

            playClubSound(false);

            if (framesElapsed == 3)
            {
                log("Hole: " + Globals.hole + ", Now, choose a club. ");
                playSound("chooseaclub");
            }

            ball.visible = true;
            ball.velocity.Y = 0;
            ball.velocity.X = 0;
            ball.play("size2");

            text.text = clubs[selected].ToString();

            if (Globals.ACTIONJUSTPRESSED && (selected == 0 || suggestionForClubNoted))
            {
                #region PlaySounds
                playClubSound();
                #endregion

                log(clubs[selected].ToString());
                
                state = GameState.ChooseForce;
                resetSelections();
                return;
            }
            else if (Globals.ACTIONJUSTPRESSED)
            {
                log("May I suggest a Putter");

                playSound("youhavechosen");

                suggestionForClubNoted = true;
                suggestionForClubStatus = 1;
            }

            if (suggestionForClubStatus == 1)
            {
                if (sound.getState() == SoundState.Stopped)
                {
                    playClubSound();
                    suggestionForClubStatus = 2;
                }
            }
            else if (suggestionForClubStatus == 2)
            {
                if (sound.getState() == SoundState.Stopped)
                {
                    playSound("mayisuggestaputter");
                    suggestionForClubStatus = 3;
                }
            }
        }

        private void playForceSound()
        {
            if (selected == 0) playSound("feathertouch");
            else if (selected == 3) playSound("powerdrive");
            else if (selected == 4) playSound("chipshot");
            else if (selected == 5) playSound("pitch");
            else if (selected == 6) playSound("fade");
            else if (selected == 7) playSound("draw");
        }

        public void chooseForce()
        {
            if (selected > force.Count - 1) selected = 0;
            if (framesElapsed == 3)
            {
                log("Now enter the force of your swing.");
                playSound("nowenter");
            }

            text.text = force[selected].ToString();

            if (Globals.ACTIONJUSTPRESSED && (selected == 0 || suggestionForForceNoted))
            {
                log("You have entered " + force[selected].ToString() );

                playForceSound();

                selectedPower = selected;

                resetSelections();
                state = GameState.Swing;
                return;
            }
            else if (Globals.ACTIONJUSTPRESSED)
            {
                log("I suggest feather touch.");
                playSound("youhavechosen");

                suggestionForForceNoted = true;
                suggestionForForceStatus = 1;
            }

            if (suggestionForForceStatus == 1)
            {
                if (sound.getState() == SoundState.Stopped)
                {
                    playForceSound();
                    suggestionForForceStatus = 2;
                }
            }
            if (suggestionForForceStatus == 2)
            {
                if (sound.getState() == SoundState.Stopped)
                {
                    playSound("isuggestfeathertouch");
                    suggestionForForceStatus = 3;
                }
            }

        }

        public void chooseSwing()
        {
            text.text = " ";
            rollTiles.color = new Color(1, 1, 1, 1.0f);

            if (framesElapsed == 3)
            {
                log("Now aim your shot.");
            }

            aim.startAim = true;
            aim.health = 1;

            if (Globals.ACTIONJUSTPRESSED)
            {
                resetSelections();
                aim.startAim = false;
                aim.startHealth = true;
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

            if (Globals.ACTIONJUSTPRESSED)
            {
                float an = FlxU.getAngle(new Vector2(ball.x + (ball.width / 2), ball.y + (ball.height / 2)), new Vector2(aim.x + (aim.width / 2), aim.y + (aim.height / 2)));

                float an2 = FlxU.getAngle(new Vector2(ball.x, ball.y), new Vector2(aim.x, aim.y));
                float an3 = FlxU.getAngle(new Vector2(ball.x + (ball.width / 1), ball.y + (ball.height / 1)), new Vector2(aim.x + (aim.width / 1), aim.y + (aim.height / 1)));

                Console.WriteLine("Angle to shoot is {0} , {1} , {2}", an, an2, an3);

                ball.angle = an + 90;

                int multiplier = 1;
                multiplier *= selectedPower;
                int initialPower = 5;

                ball.setVelocityFromAngle(initialPower + (aim.health * multiplier));
                ball.maxVelocity.X = initialPower + (aim.health * multiplier);
                ball.maxVelocity.Y = initialPower + (aim.health * multiplier);
                ball.angle = 0;
                aim.startAim = false;
                aim.startHealth = false;

                FlxG.play("putt/sfx/putt", 1.0f, false);
                rollTiles.color = new Color(1, 1, 1, 0.1f);

                resetSelections();
                state = GameState.BallInPlay;
                lee.debugName = "swing";
                return;
            }
        }

        public void ballInPlay()
        {
            FlxU.overlap(ball, hole, ballInHole);

            if (ball.velocity.Y == 0)
            {
                resetSelections();
                state = GameState.BallEnded;
                return;
            }
        }

        public void ballEnded()
        {
            lee.debugName = "";
            if (framesElapsed == 3)
            {
                //Console.WriteLine("-- Ball.x/y {0} {1}", ball.x, ball.y);

                log("Ball is in ... ");
                playSound("ballisin");
                power.visible = false;
            }

            if (sound.getState() == SoundState.Stopped)
            {
                ballEndedStatus++;

                if (ballEndedStatus == 1)
                {

                    float dist = FlxU.getDistance(new Vector2(ball.x, ball.y), new Vector2(hole.x, hole.y) );

                    Console.WriteLine("-- Ball.x/y {0} {1} -- Distance. {2}", ball.x, ball.y, dist);
                    
                    if (Globals.ballInHole==true)
                    {
                        log("ball is in the hole. Great shot pal.");
                    }
                    else if (lee.club == "wood")
                    {
                        log("ball is in carpark.");
                    }
                    else if (lee.club == "iron")
                    {
                        log("ball is over the green. Next time try using a putter.");
                    }
                    else if (dist < 8)
                    {
                        log("Extremely close, but on the pro circuit give me's are not allowed.");
                    }
                    else if (dist > 50)
                    {
                        log("Not even close there tiger.");
                    }
                    else if (ball.x > FlxG.width)
                    {
                        log("too far to the right.");
                    }
                    else if (ball.x < 50)
                    {
                        log("too far to the left");
                    }
                    else if (ball.y < 75)
                    {
                        log("too hard there, champ. ease up on the power next time.");
                    }




                }
            }

            if (framesElapsed > 4)
            {
                if (Globals.ACTIONJUSTPRESSED || sound.getState() == SoundState.Stopped)
                {
                    lee.club = "";
                    state = GameState.PlayAgain;
                    resetSelections();
                    return;
                }
            }
        }

        public void choosePlayAgain()
        {
            if (selected > playAgain.Count - 1) selected = 0;
            if (framesElapsed == 3)
            {
                log("Would you like to play again?");
                playSound("wouldyouliketoplayagain");
            }

            text.text = playAgain[selected].ToString();

            if (Globals.ACTIONJUSTPRESSED && selected == 0)
            {
                log("You have selected Yes");
                playSound("youhaveselectedyes");
                Globals.hole++;

                state = GameState.Reset;
                return;
            }
            if (Globals.ACTIONJUSTPRESSED && selected == 1)
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

                    if (Globals.ballInHole)
                    {
                        Globals.scoreCard.Add(1);
                    }
                    else
                    {
                        Globals.scoreCard.Add(0);
                    }

                    resetSelections();
                    //hole.reset(FlxU.random(20, FlxG.width - 20), hole.y);

                    

                    suggestionForClubNoted = false;
                    suggestionForForceNoted = false;
                    suggestionForClubStatus = 0;
                    suggestionForForceStatus = 0;
                    Globals.ballInHole = false;

                    state = GameState.Introduction;
                    FlxG.state = new PuttState();
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

        /// <summary>
        /// The main update function.
        /// </summary>
        override public void update()
        {
            if (FlxG.debug && FlxG.keys.justPressed(Keys.D2))
            {
                Globals.canSkip = true;
            }
            if (FlxG.keys.justPressed(Keys.B))
            {
                FlxG.showBounds = !FlxG.showBounds;
            }
            lee.soundState = sound.getState();


            //------------------------------------------------------------------
            //Console.WriteLine(sound.getName());  

            if (state == GameState.Introduction)
            {
                
            }

            if (sound.getState() == SoundState.Stopped || Globals.hole>1 || Globals.canSkip)
            {
                if (Globals.ACTIONJUSTPRESSED)
                {
                    everyAction();
                }

                //lee.play("idle");
                
                if (FlxControl.LEFTJUSTPRESSED)
                {
                    FlxG.play("putt/sfx/blip");
                    selected--;
                    if (selected < 0) selected = 0;
                }
                if (FlxControl.RIGHTJUSTPRESSED || (FlxG.mouse.justPressed() && FlxG.mouse.x < (FlxG.width/4)*3))
                {
                    FlxG.play("putt/sfx/blip");
                    selected++;
                }


                framesElapsed++;

                if (state == GameState.Introduction)
                {
                    chooseIntroduction();
                }
                else if (state == GameState.ChooseClub)
                {
                    chooseClub();
                }
                else if (state == GameState.ChooseForce)
                {
                    chooseForce();
                }
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
                else if (state == GameState.BallEnded)
                {
                    ballEnded();
                }
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

            }

            //Console.WriteLine(rollTiles.getTile((int)(ball.x / 8),(int)(ball.y / 8)).ToString() );
            int tile = rollTiles.getTile((int)(ball.x / 8), (int)(ball.y / 8));
            if (tile != -1)
            {
                ball.adjustSpeedForTile(tile);
            }

            base.update();
        }

        protected bool ballInHole(object Sender, FlxSpriteCollisionEvent e)
        {
            Console.WriteLine("Ball speed at time of sinking {0} {1} -- ball x/y {2} {3} hole x/y {4} {5} ", ball.velocity.X, ball.velocity.Y, ball.x, ball.y,hole.x,hole.y);
            
            if (ball.velocity.Y < -60)
            {
                // Voice -- "A little too much juice on that one. Next time try a softer approach.
                // too fast to sink

                if (ball.x>hole.x-1)
                    ball.velocity.X = 56;
                else if (ball.x < hole.x-1)
                    ball.velocity.X = -56;

            }
            else
            {
                //went in the hole.
                if (ball.visible)
                {
                    FlxG.play("putt/sfx/ballinhole");
                    Globals.ballInHole = true;

                }
                state = GameState.BallEnded;
                ball.visible = false;
                resetSelections();
            }
            return true;
        }
    }
}
