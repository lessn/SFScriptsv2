#region

using System;
using LeagueSharp;
using LeagueSharp.Common;

#endregion

namespace SFSeries
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            CustomEvents.Game.OnGameLoad += a =>
            {
                try
                {
                    var type = Type.GetType("SFSeries." + ObjectManager.Player.ChampionName);

                    if (type != null)
                    {
                        Activator.CreateInstance(type);
                        return;
                    }

                    Game.PrintChat(ObjectManager.Player.ChampionName + " not supported");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            };
        }
    }
}