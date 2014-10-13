using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace SFSeries
{
    partial class Program
    {
        public static string ChampionName;
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad +=Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            ChampionName = ObjectManager.Player.ChampionName;

            switch (ChampionName)
            {
                case "Ahri":
                    new Ahri();
                    break;
                case "Katarina":
                    new Katarina();
                    break;
                default:
                    Game.PrintChat("Champion not supported by SFSeries");
                    break;
                    
            }
        }
    }
}
