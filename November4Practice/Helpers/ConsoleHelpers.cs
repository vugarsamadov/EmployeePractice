using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace November4Practice.Helpers
{
    internal static class ConsoleHelpers
    {
        private const ConsoleColor ERROR_COLOR = ConsoleColor.Red;
        private const ConsoleColor WARNING_COLOR = ConsoleColor.Yellow;
        private const ConsoleColor POSITIVE_COLOR = ConsoleColor.Green;
        private const string SELECTION_CURSOR = ">  ";

        public static void InlineError(string errorMsg)
        {
            Console.ForegroundColor = ERROR_COLOR;
            Console.Write(errorMsg);
            Console.ResetColor();
        }

        public static void InlineWarning(string warningMsg)
        {
            Console.ForegroundColor = WARNING_COLOR;
            Console.Write(warningMsg);
            Console.ResetColor();
        }

        public static void InlinePositive(string msg)
        {
            Console.ForegroundColor = POSITIVE_COLOR;
            Console.Write(msg);
            Console.ResetColor();
        }

        public static void InlineSelectionCursor()
        {
            Console.ForegroundColor = POSITIVE_COLOR;
            Console.Write(SELECTION_CURSOR);
            Console.ResetColor();
        }

        public static void PrintError(string errorMsg) => InlineError(errorMsg+"\n");

        public static void PrintWarning(string warningMsg) => InlineWarning(warningMsg+"\n");

        public static void PrintPositive(string msg) => InlinePositive(msg+"\n");
    }
}
