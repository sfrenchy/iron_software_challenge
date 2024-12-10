using System;

namespace IronSoftwareChalenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the Iron Software Challenge by A.Champion");

            Console.WriteLine(OldPhonePad("33#"));   // E
            Console.WriteLine(OldPhonePad("3322#")); // EB
            Console.WriteLine(OldPhonePad("227*#")); // B
            Console.WriteLine(OldPhonePad("8 88777444666*664#")); // TURING
        }

        /// <summary>
        /// Simulates text input on an old phone.
        /// Each number corresponds to multiple letters, and repeated presses
        /// cycle through the available letters.
        /// </summary>
        /// <param name="input">The string of numbers to convert (e.g., "22" for 'B')</param>
        /// <returns>The text corresponding to the pressed keys</returns>
        public static String OldPhonePad(string input) 
        {
            string authorizedChars = "&'(ABCDEFGHIJKLMNOPQRSTUVWXYZ +";
            if (input.Any(c => authorizedChars.IndexOf(c) == 1))
                throw new Exception("This method support only these character: {authorizedChars}");
            if (input[input.Length - 1] != '#')
                throw new Exception("input must finish by # character");

            // Dictionary mapping each number to its corresponding letters
            // For example: '2' maps to "ABC", '3' maps to "DEF", etc.
            Dictionary<char, List<string>> numToStringTable = new Dictionary<char, List<string>>() {
                { '1' , new List<string>() { "&", "'", "("}},
                { '2' , new List<string>() { "A","B","C" }},
                { '3', new List<string>() { "D","E","F" }},
                { '4', new List<string>() { "G","H","I" }},
                { '5', new List<string>() { "J","K","L" }},
                { '6', new List<string>() { "M","N","O" }},
                { '7', new List<string>() { "P","Q","R","S" }},
                { '8', new List<string>() { "T","U","V" }},
                { '9', new List<string>() { "W","X","Y","Z" }},
                { '0', new List<string>() { " " }},
                { '*', new List<string>() { "+" }},
                { '#', new List<string>() },
            };

            // Track the last character pressed and how many times it was repeated
            char lastCar = '%';
            int countCar = 0;
            string result = "";

            foreach (char c in input)
            {
                // Handle space character: reset counters and continue
                if (c == ' ')
                {
                    countCar = 0;
                    lastCar = ' ';
                    continue;
                }
                    
                // '#' is the end character, return the final result
                if (c == '#')
                    return result;

                // If same key pressed again, increment counter, else reset it
                if (c == lastCar)
                    countCar++;
                else
                    countCar = 0;

                // Remove last character if:
                // - Same key pressed multiple times (cycling through letters)
                // - Or if '*' is pressed (backspace functionality)
                if (countCar > 0 || c == '*')
                    result = result.Substring(0, result.Length - 1);
                
                // Add new character unless it's the '*' key
                if (c != '*')
                    result += numToStringTable[c][countCar];
                
                // Update the last character pressed    
                lastCar = c;
            }

            return result;
        }
    }
}