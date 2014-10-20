using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Island
{
    class Lee : FlxSprite
    {
        public string club;
        public SoundState soundState;

        public Lee(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("putt/lee_150x150"), true, false, 150, 150);
            loadAnimationsFromGraphicsGaleCSV("content/putt/lee_150x150.csv", null, new List<string> {"introduction_talk", "swing_wood", "swing_putter"}, true);
            //addAnimation("idlex_putter", new int[] { 15,16 }, 12);
            boundingBoxOverride = false;
            debugName = "";
            club = "";
            soundState = SoundState.Stopped;


        }

        override public void update()
        {

            //Console.WriteLine("Lee carvello's club: {0}, debugname: {1},", this.club, this.debugName);
            if (this.debugName == "swing")
            {
                if (this.club == "")
                    play("swing");
                else
                    play("swing_" + this.club);

            }
            // Not talking
            else if (soundState == SoundState.Stopped)
            {
                if (this.club == "")
                    play("idle");
                else
                    play("idle_" + this.club);
            }
                //talking
            else
            {
                if (this.debugName == "")
                {
                    if (this.club == "")
                        play("talk");
                    else
                        play("talk_" + this.club);

                }
                
                else
                    play(this.debugName + "_talk");
            }


            base.update();

        }


    }
}
