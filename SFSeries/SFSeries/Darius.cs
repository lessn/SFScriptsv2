using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using SharpDX;
using Color = System.Drawing.Color;

namespace SFSeries
{
    class Darius
    {

        //Orbwalker instance
        public static Orbwalking.Orbwalker Orbwalker;

        //Spells
        public static Spell Q;
        public static Spell W;
        public static Spell E;
        public static Spell R;

        //Menu
        public static Menu Config;
        private static Obj_AI_Hero _player;


        public Darius()
        {
            OnGameLoaded();
        }

        private static void OnGameLoaded()
        {
            _player = ObjectManager.Player;
            Q = new Spell(SpellSlot.Q, 425);
            W = new Spell(SpellSlot.W, 125);
            E = new Spell(SpellSlot.E, 540);
            R = new Spell(SpellSlot.R, 460);



            Game.PrintChat("Darius Loaded! By iSnorflake V2");
            //Create the menu
            Config = new Menu("Darius", "Darius", true);

            //Orbwalker submenu
            Config.AddSubMenu(new Menu("Orbwalking", "Orbwalking"));
            Orbwalker = new Orbwalking.Orbwalker(Config.SubMenu("Orbwalking"));
            //Add the targer selector to the menu.
            var targetSelectorMenu = new Menu("Target Selector", "Target Selector");
            SimpleTs.AddToMenu(targetSelectorMenu);
            Config.AddSubMenu(targetSelectorMenu);



            //Combo menu
            Config.AddSubMenu(new Menu("Combo", "Combo"));
            Config.SubMenu("Combo").AddItem(new MenuItem("UseQCombo", "Use Q").SetValue(true));
            Config.SubMenu("Combo").AddItem(new MenuItem("UseWCombo", "Use W").SetValue(true));
            Config.SubMenu("Combo").AddItem(new MenuItem("UseECombo", "Use E").SetValue(true));
            Config.SubMenu("Combo").AddItem(new MenuItem("UseRCombo", "Use R").SetValue(true));
            
            // Drawings
            Config.AddSubMenu(new Menu("Drawings", "Drawings"));
            Config.SubMenu("Drawings").AddItem(new MenuItem("QRange", "Q Range").SetValue(new Circle(true, Color.FromArgb(150, Color.DodgerBlue))));
            Config.SubMenu("Drawings").AddItem(new MenuItem("ERange", "E Range").SetValue(new Circle(true, Color.FromArgb(150, Color.Crimson))));
            Config.AddSubMenu(new Menu("Exploits", "Exploits"));
            Config.SubMenu("Exploits").AddItem(new MenuItem("NFE", "No-Face Exploit").SetValue(true));
            // Config.SubMenu("Drawings").AddItem(new MenuItem("ERange", "E Range").SetValue(new Circle(true, Color.FromArgb(150, Color.DodgerBlue))));

            Config.AddToMainMenu();
            //Add the events we are going to use\
            Game.OnGameUpdate += Game_OnGameUpdate;
            Drawing.OnDraw += Drawing_OnDraw;


        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            if (_player.IsDead) return;
            switch (Orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    Combo();
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    Harras();
                    break;
                case Orbwalking.OrbwalkingMode.LaneClear:
                    LaneClear();
                    break;
            }
        }

        private static void LaneClear()
        {
            if (!Orbwalking.CanMove(40)) return;

            var allMinions = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, Q.Range);
            var useQ = Config.Item("useQW").GetValue<bool>();
            if (!Q.IsReady()) return;
            Q.Cast();
        }

        private static void Harras()
        {
            var target = SimpleTs.GetTarget(E.Range, SimpleTs.DamageType.Physical);
            if (target == null) return;

            if (Q.IsReady() && _player.Distance(target) < Q.Range + target.BoundingRadius)
            {
                Q.Cast();
            }
            if (W.IsReady() && _player.Distance(target) < W.Range + target.BoundingRadius)
            {
                W.Cast();
            }
            if (E.IsReady() && _player.Distance(target) < E.Range)
            {
                E.Cast(target.ServerPosition, Config.Item("NFE").GetValue<bool>());
            }
        }

        private static void Combo()
        {
            var target = SimpleTs.GetTarget(E.Range, SimpleTs.DamageType.Physical);
            if (target == null) return;

            if (Q.IsReady() && _player.Distance(target) < Q.Range + target.BoundingRadius)
            {
                Q.Cast();
            }
            if (W.IsReady() && _player.Distance(target) < W.Range + target.BoundingRadius)
            {
                W.Cast();
            }
            if (E.IsReady() && _player.Distance(target) < E.Range)
            {
                E.Cast(target.ServerPosition, Config.Item("NFE").GetValue<bool>());
            }
            if (_player.GetSpellDamage(target, SpellSlot.R, 1) > target.Health)
            {
                R.CastOnUnit(target, Config.Item("NFE").GetValue<bool>());
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Config.Item("QRange").GetValue<Circle>().Active)
            {
                Utility.DrawCircle(_player.Position, Q.Range, Config.Item("QRange").GetValue<Circle>().Color);
            }
            if (Config.Item("ERange").GetValue<Circle>().Active)
            {
                Utility.DrawCircle(_player.Position, E.Range, Config.Item("ERange").GetValue<Circle>().Color);
            }
        }
    }
}
