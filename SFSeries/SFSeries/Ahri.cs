/*
 *   _________________________  .__          .__ 
 *  /   _____/\_   _____/  _  \ |  |_________|__|
 *  \_____  \  |    __)/  /_\  \|  |  \_  __ \  |
 *  /        \ |     \/    |    \   Y  \  | \/  |
 * /_______  / \___  /\____|__  /___|  /__|  |__|
 *         \/      \/         \/     \/          
 * 
 * Features:
 * Basic combo
 * Best prodiction.
 * Lag-free circles.
 * Easy user customability
 * 
 * Credits
 * Snorflake
 * 
 */
#region References

using LeagueSharp;
using LeagueSharp.Common;
using System;
using Color = System.Drawing.Color;

#endregion
// By iSnorflake
namespace SFSeries
{
    class Ahri
    {
        #region Declares
        public static string Name = "Ahri";
        public static Orbwalking.Orbwalker Orbwalker;
        public static Obj_AI_Hero Player = ObjectManager.Player;
        public static Spell Q, W, E;
        public static Items.Item Dfg;

        public static Menu Sf;

        public Ahri()
        {
            Game_OnGameLoad();
        }
        #endregion

        #region OnGameLoad
        static void Game_OnGameLoad()
        {
            if (Player.BaseSkinName != Name) return;

            Q = new Spell(SpellSlot.Q, 880);
            W = new Spell(SpellSlot.W, 800);
            E = new Spell(SpellSlot.E, 975);

            Q.SetSkillshot(0.50f, 100f, 1100f, false, SkillshotType.SkillshotLine);
            E.SetSkillshot(0.50f, 60f, 1200f, true, SkillshotType.SkillshotLine);
            //Base menu
            Sf = new Menu("SFAhri", Name, true);
            //Orbwalker and menu
            Sf.AddSubMenu(new Menu("Orbwalker", "Orbwalker"));
            Orbwalker = new Orbwalking.Orbwalker(Sf.SubMenu("Orbwalker"));
            //Target selector and menu
            var ts = new Menu("Target Selector", "Target Selector");
            SimpleTs.AddToMenu(ts);
            Sf.AddSubMenu(ts);
            //Combo menu
            Sf.AddSubMenu(new Menu("Combo", "Combo"));
            Sf.SubMenu("Combo").AddItem(new MenuItem("useQ", "Use Q?").SetValue(true));
            Sf.SubMenu("Combo").AddItem(new MenuItem("useW", "Use W?").SetValue(true));
            Sf.SubMenu("Combo").AddItem(new MenuItem("useE", "Use E?").SetValue(true));
            var harras = new Menu("Harras", "Harras");
            harras.AddItem(new MenuItem("useQH", "Use Q?").SetValue(true));
            Sf.AddSubMenu(harras);
            //Exploits
            Sf.AddItem(new MenuItem("NFE", "No-Face").SetValue(false));
            //Make the menu visible
            Sf.AddToMainMenu();

            Drawing.OnDraw += Drawing_OnDraw; // Add onDraw
            Game.OnGameUpdate += Game_OnGameUpdate; // adds OnGameUpdate (Same as onTick in bol)

            Game.PrintChat("SFAhri loaded! By iSnorflake");


        }
        #endregion

        #region OnGameUpdate
        static void Game_OnGameUpdate(EventArgs args)
        {
            switch (Orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    Combo();
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    Harras();
                    break;
               
            }
        }
        #endregion

        #region OnDraw
        static void Drawing_OnDraw(EventArgs args)
        {
            Utility.DrawCircle(Player.Position, Q.Range,Color.DarkBlue);
            Utility.DrawCircle(Player.Position, E.Range, Color.Purple);
        }
        #endregion

        #region Combo
        public static void Combo()
        {
            // Game.PrintChat("Got to COMBO function");
            var target = SimpleTs.GetTarget(Q.Range, SimpleTs.DamageType.Magical);
            if (target == null) return;


            if (target.IsValidTarget(Q.Range) && Q.IsReady())
            {
                Q.Cast(target, Sf.Item("NFE").GetValue<bool>());
            }
            if (target.IsValidTarget(W.Range) && W.IsReady())
            {
                W.Cast();
            }
            if (target.IsValidTarget(E.Range) & E.IsReady())
            {
                E.Cast(target, Sf.Item("NFE").GetValue<bool>());
            }

        }
        #endregion

        #region Harras
        public static void Harras()
        {
            var target = SimpleTs.GetTarget(E.Range, SimpleTs.DamageType.Magical);
            if (target == null) return;


            if (target.IsValidTarget(Q.Range) && Q.IsReady())
            {
                Q.Cast(target, Sf.Item("NFE").GetValue<bool>());
            }
        }
        #endregion

    }

}