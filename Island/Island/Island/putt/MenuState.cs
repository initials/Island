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
            menu.loadGraphic("putt/menu", true, false, 256, 224);
            add(menu);

            toPlay = false;
            FlxG.playMp3("putt/music/GreenlandIsAlive", 1.0f);

            toStart = false;
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

            if (FlxControl.CANCELJUSTPRESSED)
            {
                FlxG.state = new LoadRomState();
            }

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
                    FlxG.stopMp3();
                    Globals.canSkip = false;
                    Globals.hole = 1;
                    FlxG.state = new PuttState();
                }
            }

            base.update();
        }


    }
}
