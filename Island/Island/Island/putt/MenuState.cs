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

        override public void create()
        {
            base.create();

            menu = new FlxSprite(0, 0);
            menu.loadGraphic("putt/menu", true, false, 256, 224);
            add(menu);

        }

        override public void update()
        {

            if (elapsedInState > 1.0f)
            {
                menu.color = FlxColor.randomColor();

                if (elapsedInState > 2.0f)
                {
                    elapsedInState = 0;
                }

            }

            if (FlxControl.CANCELJUSTPRESSED)
            {
                FlxG.state = new LoadRomState();
            }

            if (FlxControl.ACTIONJUSTPRESSED)
            {
                FlxG.state = new PuttState();
            }

            base.update();
        }


    }
}
