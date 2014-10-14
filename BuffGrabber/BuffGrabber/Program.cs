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
            foreach (var player in ObjectManager.Get<Obj_AI_Hero>())
            {
                Game.PrintChat(player.BaseSkinName + " Has buffs: " + player.Buffs);
            }
        }
    }
}
