using November4Practice.ExceptionRelated;
using November4Practice.Models;

namespace November4Practice.Helpers
{
    internal static class InputHelper
    {
        public static string PromptAndGetNonEmptyString(string prompt)
        {
            string input = null;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(input) || input == string.Empty)
                    ConsoleHelpers.PrintError("Empty input is not allowed!");

            } while (String.IsNullOrWhiteSpace(input) || input == string.Empty);

            return input;
        }

        public static int PromptAndGetPositiveInt(string prompt)
        {
            int input = 0;
            do
            {
                input = Convert.ToInt32(PromptAndGetNonEmptyString(prompt));

                if (input < 0)
                    ConsoleHelpers.PrintError("Positive input is required!");

            } while (input < 0);

            return input;
        }

        public static int PromptAndGetPositiveRangedInt(string prompt, int maxRange)
        {
            int input = 0;
            do
            {
                input = PromptAndGetPositiveInt(prompt);

                if (input > maxRange)
                    ConsoleHelpers.PrintError($"Positive input with {maxRange} max value is required!");

            } while (input > maxRange);

            return input;
        }

        public static int PromptAndGetPositiveNzInt(string prompt)
        {
            int input = 0;
            do
            {
                input = Convert.ToInt32(PromptAndGetNonEmptyString(prompt));

                if (input <= 0)
                    ConsoleHelpers.PrintError("Positive non zero input is required!");

            } while (input <= 0);

            return input;
        }

        public static decimal PromptAndGetPositiveDecimal(string prompt)
        {
            decimal input = 0;
            do
            {
                input = Convert.ToDecimal(PromptAndGetNonEmptyString(prompt));

                if (input <= 0m)
                    ConsoleHelpers.PrintError("Positive non zero input is required!");

            } while (input < 0m);

            return input;
        }

        /// <summary>
        /// Prints interactive ui with commands
        /// </summary>
        /// <typeparam name="T">Command Enum type</typeparam>
        /// <param name="commands">Command Enum Array</param>
        /// <param name="printBuffer">Prints Buffer</param>
        /// <param name="header">Prompt header</param>
        /// <returns>Command index From Commands Array</returns>
        public static int DisplayAndGetCommandBySelection<T>
            (T[] commands, Action printBuffer, string header = "") where T: Enum 
        {
            int currentCmdIndex = 0;
            do
            {
                Console.Clear();
                printBuffer.Invoke();

                if(!String.IsNullOrEmpty(header))
                    ConsoleHelpers.PrintWarning(header+": ");

                for (int i = 0; i< commands.Length; i++)
                {
                    if (i == currentCmdIndex)
                        ConsoleHelpers.InlineSelectionCursor();
                    else
                        Console.Write("  ");
                    Console.WriteLine(commands[i]);
                }
                var keyPress = Console.ReadKey().Key;

                if(keyPress == ConsoleKey.UpArrow)
                {
                    currentCmdIndex = currentCmdIndex - 1 < 0? 0: currentCmdIndex - 1;
                }

                if (keyPress == ConsoleKey.DownArrow)
                {
                    currentCmdIndex = currentCmdIndex + 1 > commands.Length-1 ? commands.Length-1 : currentCmdIndex + 1;
                }

                if (keyPress == ConsoleKey.Enter)
                    return currentCmdIndex;

            } while (true);    
        }

    }
}
