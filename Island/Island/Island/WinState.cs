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
    public class WinState: FlxState
    {

        override public void create()
        {
            base.create();

            //FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));
            for (int i = 0; i < 40; i++)
            {
                for (int y = 0; y < 40; y++)
                {
                    FlxSprite x = new FlxSprite(i * 8, y * 8);
                    x.loadGraphic("water", false, false, 8, 8);
                    x.addAnimation("flow", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, (int)FlxU.random(1, 7), true);
                    x.play("flow");

                    add(x);
                }
            }

            FlxTilemap t = new FlxTilemap();
            string map = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0\n";
            map += "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0\n";
            map += "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0\n";
            map += "0,0,1,1,1,1,1,0,1,0,1,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0\n";
            map += "0,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0\n";
            map += "0,0,0,0,1,0,0,0,1,1,1,0,1,1,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0\n";
            map += "0,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0\n";
            map += "0,0,0,0,1,0,0,0,1,0,1,0,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0\n";
            map += "0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0\n";
            map += "0,1,0,1,1,1,0,1,0,0,0,1,1,1,0,1,0,0,0,1,0,1,1,1,0,0,0,0,0,0,0\n";
            map += "0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,1,1,0,0,1,0,1,0,0,1,0,0,0,0,0,0\n";
            map += "0,1,0,1,1,1,0,1,0,0,0,1,1,1,0,1,0,1,0,1,0,1,0,0,1,0,0,0,0,0,0\n";
            map += "0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0,0,1,1,0,1,0,0,1,0,0,0,0,0,0\n";
            map += "0,1,0,1,1,1,0,1,1,1,0,1,0,1,0,1,0,0,0,1,0,1,1,1,0,0,0,0,0,0,0\n";
            map += "0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0\n";
            map += "0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0\n";
            map += "0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0\n";
            map += "0,0,0,1,1,1,1,0,0,0,0,0,0,1,1,1,1,0,0,0,0,1,1,1,0,0,0,0,0,0,0\n";
            map += "0,0,0,0,0,1,1,0,0,0,0,0,1,1,1,1,1,0,0,0,1,1,0,0,0,0,0,0,0,0,0\n";
            map += "0,0,0,0,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0\n";
            map += "0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0\n";
            map += "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0\n";
            map += "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0\n";


            t.auto = FlxTilemap.AUTO;
            t.loadMap(map, FlxG.Content.Load<Texture2D>("autotilesIsland"), 8, 8);
            add(t);


        }

        override public void update()
        {


            if (elapsedInState > 0.5f)
            {
                //FlxG.setHudText(3, "Press START\nto begin");
                //FlxG.setHudTextPosition(3, 0, 100);
                //FlxG.setHudTextScale(3, 2);
            }
            if (FlxControl.ACTIONJUSTPRESSED && elapsedInState > 0.5f)
            {
                FlxG.state = new PlayState();
            }


            base.update();
        }


    }
}
