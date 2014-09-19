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
    public class PlayState : FlxState
    {
        //private FlxTilemap tiles;

        private FlxGroup tileGrp;
        private FlxGroup waterGrp;
        private FlxGroup pellets;
        //private FlxSprite m;

        private FlxSprite player;

        private FlxSprite shadow;

        int flightHeight = 0;

        override public void create()
        {
            FlxG.resetHud();
            FlxG.hideHud();

            FlxG.backColor = FlxColor.ToColor("#3cbcfc");

            base.create();

            FlxCaveGeneratorExt caveExt = new FlxCaveGeneratorExt(40, 40, 0.514f, 2);
            string[,] caveLevel = caveExt.generateCaveLevel();

            //Optional step to print cave to the console.
            //caveExt.printCave(caveLevel);
            #region color

            Color[] colors = new Color[] { 
                FlxColor.ToColor("#7C7C7C"),

FlxColor.ToColor("#0000FC"),

FlxColor.ToColor("#0000BC"),

FlxColor.ToColor("#4428BC"),

FlxColor.ToColor("#940084"),

FlxColor.ToColor("#A80020"),

FlxColor.ToColor("#A81000"),

FlxColor.ToColor("#881400"),

FlxColor.ToColor("#503000"),

FlxColor.ToColor("#007800"),

FlxColor.ToColor("#006800"),

FlxColor.ToColor("#005800"),

FlxColor.ToColor("#004058"),

FlxColor.ToColor("#000000"),

FlxColor.ToColor("#000000"),

FlxColor.ToColor("#000000"),

FlxColor.ToColor("#BCBCBC"),

FlxColor.ToColor("#0078F8"),

FlxColor.ToColor("#0058F8"),

FlxColor.ToColor("#6844FC"),

FlxColor.ToColor("#D800CC"),

FlxColor.ToColor("#E40058"),

FlxColor.ToColor("#F83800"),

FlxColor.ToColor("#E45C10"),

FlxColor.ToColor("#AC7C00"),

FlxColor.ToColor("#00B800"),

FlxColor.ToColor("#00A800"),

FlxColor.ToColor("#00A844"),

FlxColor.ToColor("#008888"),

FlxColor.ToColor("#000000"),

FlxColor.ToColor("#000000"),

FlxColor.ToColor("#000000"),

FlxColor.ToColor("#F8F8F8"),

FlxColor.ToColor("#3CBCFC"),

FlxColor.ToColor("#6888FC"),

FlxColor.ToColor("#9878F8"),

FlxColor.ToColor("#F878F8"),

FlxColor.ToColor("#F85898"),

FlxColor.ToColor("#F87858"),

FlxColor.ToColor("#FCA044"),

FlxColor.ToColor("#F8B800"),

FlxColor.ToColor("#B8F818"),

FlxColor.ToColor("#58D854"),

FlxColor.ToColor("#58F898"),

FlxColor.ToColor("#00E8D8"),

FlxColor.ToColor("#787878"),

FlxColor.ToColor("#000000"),

FlxColor.ToColor("#000000"),

FlxColor.ToColor("#FCFCFC"),

FlxColor.ToColor("#A4E4FC"),

FlxColor.ToColor("#B8B8F8"),

FlxColor.ToColor("#D8B8F8"),

FlxColor.ToColor("#F8B8F8"),

FlxColor.ToColor("#F8A4C0"),

FlxColor.ToColor("#F0D0B0"),

FlxColor.ToColor("#FCE0A8"),

FlxColor.ToColor("#F8D878"),

FlxColor.ToColor("#D8F878"),

FlxColor.ToColor("#B8F8B8"),

FlxColor.ToColor("#B8F8D8"),

FlxColor.ToColor("#00FCFC"),

FlxColor.ToColor("#F8D8F8"),

FlxColor.ToColor("#000000"),

FlxColor.ToColor("#000000") 
            };

            #endregion 

            tileGrp = new FlxGroup();
            waterGrp = new FlxGroup();
            pellets = new FlxGroup();

            Vector2 startPos = new Vector2(0,0) ;

            for (int i = 0; i < caveLevel.GetLength(1); i++)
            {
                for (int y = 0; y < caveLevel.GetLength(0); y++)
                {
                    //string toPrint = tiles[y, i];
                    if (Convert.ToInt32(caveLevel[y, i]) != 0)
                    {

                        if (startPos.X == 0)
                        {
                            startPos = new Vector2(i * 8, y * 8);

                        }
                        FlxSprite x = new FlxSprite(i * 8, y * 8);
                        //x.createGraphic(8, 8, colors[Convert.ToInt32(caveLevel[y, i])]);
                        x.loadGraphic("autotilesIsland", false, false, 8, 8);
                        //x.color = colors[Convert.ToInt32(caveLevel[y, i])];

                        x.frame = Convert.ToInt32(caveLevel[y, i]);
                        //x.scale = 2;
                        //x.angularDrag = 250;
                        //x.setOffset(4, 4);
                        tileGrp.add(x);

                        if (FlxU.random() < 0.02f)
                        {
                            FlxSprite xx = new FlxSprite(i * 8, y * 8);
                            xx.createGraphic(8, 8, Color.Red);
                            pellets.add(xx);
                        }
                    }
                    else
                    {
                        FlxSprite x = new FlxSprite(i * 8, y * 8);
                        x.loadGraphic("water", false, false, 8, 8);
                        x.addAnimation("flow", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, (int)FlxU.random(1, 7), true);
                        x.play("flow");
                        
                        waterGrp.add(x);
                    }
                    //Console.Write(toPrint);
                }

                //Console.WriteLine();
            }

            //string newMap = caveExt.convertMultiArrayStringToString(caveLevel);


            add(tileGrp);
            add(waterGrp);
            add(pellets);

            //m = new FlxSprite(0, 0);
            //m.loadGraphic("flixel/cursor");
            //add(m);

            shadow = new FlxSprite(startPos.X, startPos.Y);
            shadow.createGraphic(8, 8, Color.Black);
            shadow.debugName = "onground";
            shadow.alpha = 0.5f;
            add(shadow);

            player = new FlxSprite(startPos.X, startPos.Y);
            player.createGraphic(8, 8, colors[12]);
            add(player);

            //FlxG.showHud();

            FlxG.follow(shadow, 20.0f);
            FlxG.followBounds(0, 0, 320, 320);

        }

        override public void update()
        {
            FlxG.setHudText(1, "Flight Height : " + flightHeight.ToString());

            
            if (shadow.debugName == "fly")
            {
                flightHeight++;
                if (flightHeight>=16)
                {
                    flightHeight = 16;
                }

            }
            if (shadow.debugName == "onground")
            {
                flightHeight--;
                if (flightHeight <= 0)
                {
                    flightHeight = 0;
                }

            }

            player.at(shadow);
            player.y -= flightHeight;
            //player.x -= flightHeight;

            if (FlxControl.ACTIONJUSTPRESSED && elapsedInState>0.6f)
            {
                if (shadow.debugName == "onground") shadow.debugName = "fly";
                else if (shadow.debugName == "fly") shadow.debugName = "onground";
            }


            //m.x = ((int)(FlxG.mouse.x / 8) * 8);
            //m.y = ((int)(FlxG.mouse.y / 8) * 8);

            

            if (shadow.debugName=="fly")
            {
                if (FlxControl.LEFTJUSTPRESSED)
                {
                    shadow.x -= 8;
                }
                if (FlxControl.RIGHTJUSTPRESSED)
                {
                    shadow.x += 8;
                }
                if (FlxControl.UPJUSTPRESSED)
                {
                    shadow.y -= 8;
                }
                if (FlxControl.DOWNJUSTPRESSED)
                {
                    shadow.y += 8;
                }
            }
            else if (shadow.debugName == "onground")
            {
                if (FlxControl.LEFTJUSTPRESSED)
                {
                    shadow.x -= 8;
                    if (FlxU.overlap(shadow, waterGrp, null) == true)
                    {
                        shadow.x += 8;
                    }
                }
                if (FlxControl.RIGHTJUSTPRESSED)
                {
                    shadow.x += 8;
                    if (FlxU.overlap(shadow, waterGrp, null) == true)
                    {
                        shadow.x -= 8;
                    }
                }
                if (FlxControl.UPJUSTPRESSED)
                {
                    shadow.y -= 8;
                    if (FlxU.overlap(shadow, waterGrp, null) == true)
                    {
                        shadow.y += 8;
                    }
                }
                if (FlxControl.DOWNJUSTPRESSED)
                {
                    shadow.y += 8;
                    if (FlxU.overlap(shadow, waterGrp, null) == true)
                    {
                        shadow.y -= 8;
                    }
                }
            }

            //if (FlxControl.LEFT)
            //{
            //    player.x -= 8;
            //}
            //if (FlxControl.RIGHT)
            //{
            //    player.x += 8;
            //}
            //if (FlxControl.UP)
            //{
            //    player.y -= 8;
            //}
            //if (FlxControl.DOWN)
            //{
            //    player.y += 8;
            //}



            //FlxG.setHudText(1, tile

            FlxU.overlap(pellets, shadow, getPellet);

            if (FlxG.debug)
            {
                if (FlxG.keys.F8)
                {
                    if (pellets.getFirstAlive() != null)
                    {
                        shadow.at(pellets.getFirstAlive());
                    }

                }
            }

            if (pellets.getFirstAlive() == null)
            {
                FlxG.username = FlxU.randomString(32);

                FlxOnlineStatCounter.sendStats("theisland", "-", (int)(elapsedInState*100));

                FlxG.state = new WinState();
                return;
            }

            base.update();




        }

        protected bool getPellet(object Sender, FlxSpriteCollisionEvent e)
        {
            if (shadow.debugName == "onground")
            {
                e.Object1.kill();
            }

            return true;
        }

    }
}
