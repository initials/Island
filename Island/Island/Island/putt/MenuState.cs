using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace Island
{
    public class MenuState : FlxState
    {
        private FlxSprite menu;
        private bool toPlay;
        private bool toStart;

        override public void create()
        {
            base.create();

            menu = new FlxSprite(0, 0);
            menu.loadGraphic(Globals.ContentFolder + "/menu", true, false, 256, 224);
            add(menu);

            toPlay = false;
            FlxG.playMp3("putt/music/GreenlandIsAlive", 1.0f);

            toStart = false;

            FlxGlobal.cheatString = "";
        }

        override public void update()
        {
            
            if (elapsedInState > 1.0f)
            {
                

                if (elapsedInState > 1.25f)
                {
                    elapsedInState = 0.16f;
                    if (toPlay == false)
                    {
                        //FlxG.playMp3("putt/music/GreenlandIsAlive", 1.0f);
                        toPlay = true;
                        
                    }
                    menu.color = Color.White;

                }

            }

            //if (FlxControl.CANCELJUSTPRESSED)
            //{
            //    FlxG.state = new LoadRomState();
            //}

            if (FlxControl.ACTIONJUSTPRESSED || FlxG.mouse.pressed() )
            {
                if (elapsedInState > 0.15f)
                {
                    FlxG.play("putt/sfx/blip");
                    toStart = true;
                    elapsedInState = 0;
                }
            }

            if (toStart)
            {
                menu.color = FlxColor.randomColor();

                if (elapsedInState > 0.25f)
                {
                    Globals.hasPlayedHoleAgain = false;
                    FlxG.stopMp3();
                    Globals.canSkip = false;
                    Globals.hole = 1;
                    FlxG.state = new PuttState();
                }
            }

            /* CHEAT CODES
             * okgo - Play through automatically.
             * hole# // hole## go straight to hole ##
             * bonestorm - play Lee Carvallo's Putting Challenge
             * rob - play Roberto Selavino's Putting Championship
             */ 
            if (FlxGlobal.cheatString.StartsWith("okgo"))
            {
                Globals.playThroughAutomatically = true;
            }
            if (FlxGlobal.cheatString.StartsWith("hole"))
            {
                Globals.hasPlayedHoleAgain = false;
                Globals.hole = Convert.ToInt32( FlxGlobal.cheatString.Substring(4, FlxGlobal.cheatString.Length-4)) ;
                FlxG.stopMp3();
                Globals.canSkip = false;
                FlxG.state = new PuttState();
                return;
            }
            if (FlxGlobal.cheatString.StartsWith("bonestorm"))
            {
                Globals.ContentFolder = "putt";
            }
            if (FlxGlobal.cheatString.StartsWith("rob"))
            {
                Globals.ContentFolder = "putt_r";
            }

            base.update();
        }


    }
}
