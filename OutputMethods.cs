// I preffer using "global" infront, cuz then i don't need to write "using ---" in every file 
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using System.Diagnostics.Tracing;
global using System.Runtime.InteropServices;

namespace HangMan
{
    // I would like to split up the long list of methods into different classes, but yeah
    public class OutputMethods // Create a class to handle input/output
    {
        // Generate a random word from a list based on the length of the wordlength
        public string RandomWord(int length)
        {
            Random random = new(); // Make use of the random class
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
            int currentLineCursor = Console.CursorTop; // Get the current line
            Console.SetCursorPosition(0, Console.CursorTop); // Set the cursor to the start of the line
            Console.WriteLine(new string(' ', Console.WindowWidth)); // Write a new line with the same length as the window
            Console.SetCursorPosition(0, currentLineCursor); // Set the cursor to the start of the line
        }

        public void Layout() 
        {
            Console.Clear(); // Clear the console
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
            if (health >= 0 && health <= 3) // If the health is between 0 and 3
            {
                Console.ForegroundColor = ConsoleColor.Red; // Set the foreground color to red
                HealthBar(health); // Call the healthbar method
                Console.ForegroundColor = ConsoleColor.White; // Set the foreground color to white (so default)
            }
            else if (health > 3 && health <= 6) // If the health is in between 3 & 6
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Set the foreground color to yellow, why isn't orange an option???
                HealthBar(health); // Call the healthbar method
                Console.ForegroundColor = ConsoleColor.White; // Set the foreground color to white (so default)
            }
            else if (health > 6 && health <= 10) // If the health is between 6 and 10
            {
                Console.ForegroundColor = ConsoleColor.Green; // Set the foreground color to green
                HealthBar(health); // Call the healthbar method
                Console.ForegroundColor = ConsoleColor.White; // Set the foreground color to white (so default)
            }
        }
        public void HealthBar(int health)
        {
            Console.Write("Health: "); // Write the health text
            if (health > 0) // Check if health is above 0
            {
                for (int i = 0; i <= health; i++) // Create a for loop to write the amount of hearts
                {
                    Console.Write("♥ "); // Write a heart
                }
            }
            else // If the health is 0 or below
            {
                Console.Write("X"); // Write an X
            }

            Console.WriteLine(); // Write a new line for some space
        }

        public void CurrentWord(string word, List<string> wordPrint, List<int> wordPos)
        {
            string print = ""; // Create a string to print the word
            for (int i = 0; i < word.Length; i++) // Create a for loop based on the word length
            {
                print += "_"; // Add a _ to the print 
            }
            Console.Write($"Current word: {print}"); // Write the word, "Current word:" = 14 chars

            if (wordPrint.Count > 0) // Check if the wordprint list is not empty
            {
                for (int i = 0; i < wordPrint.Count; i++) // Create a for loop based on the amount of items in the list
                {
                    Console.SetCursorPosition(wordPos[i] + 14, Console.CursorTop); // Set the cursor position to the index of the letter + the length of "Current word: "
                    Console.Write($"{wordPrint[i]}"); // Write the letter(s)
                }
            }
            Console.WriteLine(); // Add a little space
        }

        public void FullLayout(int health, string word, List<string> wordPrint, List<string> GuessedKeys, List<int> wordPos) 
        {
            Layout(); // Call the layout method
            HealthColor(health); // Call the healthcolor method
            CurrentWord(word, wordPrint, wordPos); // Call the currentword method
            KeyList(GuessedKeys); // Call the keylist method, i might set this one to [Optional]
        }

        public void KeyList(List<string> guessedWord)
        {

            Console.Write("Guessed keys: ");
            if (guessedWord.Count > 0)
            {
                foreach (string item in guessedWord)
                {
                    Console.Write($"{item}");
                }
            }

            Console.WriteLine();
        }
    }
}
