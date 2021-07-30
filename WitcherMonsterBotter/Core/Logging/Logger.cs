using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Logging
{
    public static class Logger
    {
        public enum LogType
        {
            INFO,
            WARNING,
            SUCCESS,
            ERROR
        }

        private static ConsoleColor GetColor(LogType type)
        {
            return type switch
            {
                LogType.ERROR => ConsoleColor.Red,
                LogType.INFO => ConsoleColor.White,
                LogType.WARNING => ConsoleColor.Yellow,
                LogType.SUCCESS => ConsoleColor.Green,
                _ => ConsoleColor.White
            };
        }

        public static void Log(LogType type, string message)
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = GetColor(type);

            Console.WriteLine($"[{type}] {message}");

            Console.ForegroundColor = old;
        }
    }
}
