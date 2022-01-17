using System;

namespace Kostky
{
    public static class BetterText
    {
        private static void Reset()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void RedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Reset();
        }
        public static void GreenText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Reset();
        }
        public static void BlueText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(text);
            Reset();
        }
        public static void CyanText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
            Reset();
        }
        public static void MagentaText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Reset();
        }
    }
}
