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
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
           Game.PrintChat("BuffGrabber by Snorflake loaded!");
        }

        static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args != null) 
                Game.PrintChat(args.SData.Name);
        }

        static void Game_OnGameUpdate(EventArgs args)
        {
            foreach (var buff in ObjectManager.Get<Obj_AI_Hero>().Where(target => target.IsMe).SelectMany(target => target.Buffs.Where(buff => buff.Name != "colossalstrength")))
            {

                Game.PrintChat("Buff: " + buff.Name + " - DisplayName: " + buff.DisplayName);
            }
        }
    }
}
