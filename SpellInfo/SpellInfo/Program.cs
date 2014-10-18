using LeagueSharp;
using LeagueSharp.Common;
using System;

namespace SpellInfo
{
    class Program
    {
        private static Menu _menu;
        public static Spell Q, W, E, R;
        public static bool delayed;
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        
        static void Game_OnGameLoad(EventArgs args)
        {
            Q = new Spell(SpellSlot.Q);
            W = new Spell(SpellSlot.W);
            E = new Spell(SpellSlot.E);
            R = new Spell(SpellSlot.R);
            _menu = new Menu("SpellInfo", "spellinfo", true);
            _menu.AddItem(new MenuItem("Q", "Q").SetValue(new KeyBind("Q".ToCharArray()[0], KeyBindType.Press)));
            _menu.AddItem(new MenuItem("W", "W").SetValue(new KeyBind("W".ToCharArray()[0], KeyBindType.Press)));
            _menu.AddItem(new MenuItem("E", "E").SetValue(new KeyBind("E".ToCharArray()[0], KeyBindType.Press)));
            _menu.AddItem(new MenuItem("R", "R").SetValue(new KeyBind("R".ToCharArray()[0], KeyBindType.Press)));
            _menu.AddToMainMenu();

            Game.OnGameUpdate += Game_OnGameUpdate;
        }

        static void Game_OnGameUpdate(EventArgs args)
        {

            if (_menu.Item("Q").GetValue<KeyBind>().Active)
            {
                if (!delayed)
                {
                    delayed = true;
                    Game.PrintChat("Q Info:");
                    Game.PrintChat("Delay: " + Q.Instance.SData.SpellCastTime + " Width: " + Q.Instance.SData.LineWidth +
                                   " Speed: " + Q.Instance.SData.MissileSpeed);
                    Utility.DelayAction.Add(3000, () => delayed = false);
                }
            }
            if (_menu.Item("W").GetValue<KeyBind>().Active)
            {
                if (!delayed)
                {
                    delayed = true;
                    Game.PrintChat("W Info:");
                    Game.PrintChat("Delay: " + W.Instance.SData.SpellCastTime + " Width: " + W.Instance.SData.LineWidth +
                                   " Speed: " + W.Instance.SData.MissileSpeed);

                    Utility.DelayAction.Add(3000, () => delayed = false);
                }
            }
            if (_menu.Item("E").GetValue<KeyBind>().Active)
            {
                if (!delayed)
                {
                    delayed = true;
                    Game.PrintChat("E Info:");
                    Game.PrintChat("Delay: " + E.Instance.SData.SpellCastTime + " Width: " + E.Instance.SData.LineWidth +
                                   " Speed: " + E.Instance.SData.MissileSpeed);

                    Utility.DelayAction.Add(3000, () => delayed = false);
                }
            }
            if (_menu.Item("R").GetValue<KeyBind>().Active)
            {
                if (!delayed)
                {
                    delayed = true;
                    Game.PrintChat("R Info:");
                    Game.PrintChat("Delay: " + R.Instance.SData.SpellCastTime + " Width: " + R.Instance.SData.LineWidth +
                                   " Speed: " + R.Instance.SData.MissileSpeed);

                    Utility.DelayAction.Add(3000, () => delayed = false);
                }
            }
        }
    }
}
