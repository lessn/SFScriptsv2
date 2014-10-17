using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace SFSeries
{
    class Singed
    {
        public static Menu Menu;
        public static Spell Q, W, E;
        public static Orbwalking.Orbwalker Orbwalker;
        public Singed()
        {
            Game_OnGameLoad();
        }

        static void Game_OnGameLoad()
        {
            Q = new Spell(SpellSlot.Q);
            W = new Spell(SpellSlot.W, 600);
            E = new Spell(SpellSlot.E, 250);
            W.SetSkillshot(0.5f,300f,int.MaxValue,false,SkillshotType.SkillshotCircle);
            Menu = new Menu("SF Series", "menu", true);

            var orbwalkerMenu = new Menu("Orbwalker", "orbwalker");
            Orbwalker = new Orbwalking.Orbwalker(orbwalkerMenu);
            Menu.AddSubMenu(orbwalkerMenu);
            Menu.AddItem(
                new MenuItem("enabled", "Invisible Poison").SetValue(new KeyBind("T".ToCharArray()[0],
                    KeyBindType.Toggle)));
            Menu.AddToMainMenu();

            Game.PrintChat("Singed exploit by Snorflake loaded!");
            Game.OnGameUpdate += Game_OnGameUpdate;
        }

        static void Game_OnGameUpdate(EventArgs args)
        {
            switch (Orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    Combo();
                    break;
            }
            QExploit();
        }

        private static void Combo()
        {
            var target = SimpleTs.GetTarget(W.Range, SimpleTs.DamageType.Magical);
            if (target == null) return;

            if (W.IsReady() && ObjectManager.Player.Distance(target) < W.Range + target.BoundingRadius)
                W.Cast(target);
            if (E.IsReady() && ObjectManager.Player.Distance(target) < E.Range)
                E.Cast(target, true);
        }

        static void QExploit()
        {
            if (!Menu.Item("enabled").GetValue<KeyBind>().Active) return;
            if (Q.IsReady())
                Q.Cast(ObjectManager.Player, true);
        }
    }
}
