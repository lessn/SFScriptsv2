using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using System;

namespace BuffGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            Game.OnGameUpdate += Game_OnGameUpdate;
           Game.PrintChat("BuffGrabber by Snorflake loaded!");
        }

        static void Game_OnGameUpdate(EventArgs args)
        {
            foreach (var target in ObjectManager.Get<Obj_AI_Hero>().Where(target => !target.IsMe))
            {
                foreach (var buff in target.Buffs.Where(buff => buff.Name != "colossalstrength"))
                {
                    Game.PrintChat("Buff: " + buff.Name + " - DisplayName: " + buff.DisplayName);
                }
            }
        }
    }
}
