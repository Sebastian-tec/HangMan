global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using System.Diagnostics.Tracing;

namespace HangMan
{
    public class OutputMethods
    {
        // Generate a random word from a list based on the length of the wordlength
        public string RandomWord(int length)
        {
            Random random = new Random(); // Make use of the random class
            string[] word = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "word.txt")); // Read all the lines from the words.txt file
            string randomWord = ""; // Get a random word from the list
            // Create a for loop to generate a random word
            do
            {
                randomWord = word[random.Next(0, word.Length)]; // Get a random word from the list
            } while (randomWord.Length != length);

            return randomWord; // Return the word
        }

        // Create a method to get the user input
        public string GetLetter()
        {
            // Get a letter from the user and convert it to lowercase 
            // This string declaration is probably not needed, and can be rewritten as "return Convert.ToString(Console.ReadKey().KeyChar).ToLower();"
            string letter = Convert.ToString(Console.ReadKey().KeyChar).ToLower();

            return letter; // Return the letter
        }

        // Create a method to check if the letter is in the word
        public bool CheckLetter(string word, string letter)
        {
            // Check if the letter is in the word
            if (word.Contains(letter))
            {
                return true; // Return true if the letter is in the word
            }

            return false; // Return false if the letter is not in the word
        }

        /* A void found on stackoverflow to clear the current line
         * Basically just removes the typed char for a prettier console
        */
        public void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public void Layout()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta; // Set the foreground color to darkblue, just for fun
            Console.WriteLine(@"
  _   _                   __  __             
 | | | | __ _ _ __   __ _|  \/  | __ _ _ __  
 | |_| |/ _` | '_ \ / _` | |\/| |/ _` | '_ \ 
 |  _  | (_| | | | | (_| | |  | | (_| | | | |
 |_| |_|\__,_|_| |_|\__, |_|  |_|\__,_|_| |_|
                    |___/                    
"); // Write the title

            Console.ForegroundColor = ConsoleColor.White; // Set the foreground color to white (so default)
        }

        public void HealthColor(int health)
        {
            if (health >= 0 && health <= 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                HealthBar(health);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (health > 3 && health <= 6)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                HealthBar(health);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (health > 6 && health <= 10)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                HealthBar(health);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public void HealthBar(int health)
        {
            Console.Write("Health: ");
            if (health > 0)
            {
                for (int i = 0; i <= health; i++)
                {
                    Console.Write("♥ ");
                }
            }
            else
            {
                Console.Write("X");
            }

            Console.WriteLine();
        }

        public void CurrentWord(string word, List<string> wordPrint, List<int> wordPos)
        {
            string print = "";
            for (int i = 0; i < word.Length; i++)
            {
                print += "_";
            }
            Console.Write($"Current word: {print}"); // Write the word

            if (wordPrint.Count > 0)
            {
                for (int i = 0; i < wordPrint.Count; i++)
                {
                    Console.SetCursorPosition(wordPos[i] + 14, Console.CursorTop); // Set the cursor position to the index of the letter + 14
                    Console.Write($"{wordPrint[i]}"); // Write the letter(s)
                }
            }
            Console.WriteLine(); // Add a little space
        }

        public void FullLayout(int health, string word, List<string> wordPrint, List<int> wordPos)
        {
            Layout();
            HealthColor(health);
            CurrentWord(word, wordPrint, wordPos);
        }
    }
}
