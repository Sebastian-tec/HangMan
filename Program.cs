class Program
{
    static void Main(string[] args)
    {
        do
        {
            
            OutPut op = new OutPut(); // Create a new instance of the HangMan class
            int length; // Create an int to hold the number of 
            int health = 10; // Create a int to hold the number of health/
            string word; // Create a string to hold the word


            Console.ForegroundColor = ConsoleColor.DarkBlue; // Set the foreground color to darkblue, just for fun
            Console.WriteLine("HangMan\n"); // Write the title
            Console.Title = "HangMan"; // Set the title of the window
            Console.ForegroundColor = ConsoleColor.White; // Set the foreground color to white (so default)

            // Create a do-while loop to secure the user input
            do
            {
                Console.Write("Word length: "); // Ask the user for the wordlength
            } while (!int.TryParse(Console.ReadLine(), out length)); // If the user input is not a number, ask again



            word = op.RandomWord(length); // Get a random word from the OutPut class
            List<int> wordPos = new List<int>(); // Create an array to store the word
            List<string> wordPrint = new List<string>(); // Create an array to store the word
            // Console.WriteLine(word); // Debug

            // Create a while loop to check if the user has any health left
            while (health > 0)
            {
                string key = "";
                do
                {
                    Console.WriteLine("Type a letter to guess the word"); // Ask for their guess
                    key = op.GetLetter(); // Save their guess in a string
                    op.ClearCurrentConsoleLine(); // Clear the line
                } while (!key.All(char.IsLetter));
                 
                //Console.WriteLine($"Key: {key}"); // Debug

                if (op.CheckLetter(word, key)) // Check if the letter is in the random word
                {
                    // Console.WriteLine($"Index: {word.IndexOf(key)}"); // Debug
                    wordPos.Add(word.IndexOf(key)); // Add the index of the letter to the list
                    // Console.WriteLine($"Char: {key}"); // Debug
                    wordPrint.Add(key); // Add the letter to the list
                    Console.WriteLine("Correct! The word contains the letter " + key); // Tell the user the letter is in the word
                }
                else
                {
                    Console.WriteLine("Incorrect! The word does not contain the letter " + key); // Tell the user the letter is not in the word
                    health--; // Remove one from the health

                }
                // Console.WriteLine($"{wordPrint.Count} {wordPos.Count}"); // Debug

                if (wordPrint.Count > 0) // If the wordPrint list is not empty
                {
                    for (int j = 0; j < wordPrint.Count; j++) // Create a for loop based on the list-length
                    {
                        
                        if (j == 0) // If the loop is on the first iteration
                        {
                            Console.WriteLine(); // Add a little space
                            Console.Write($"Current word: "); // Write the word
                        }

                        Console.SetCursorPosition(wordPos[j] + 14, Console.CursorTop); // Set the cursor position to the index of the letter + 14
                        Console.Write($"{wordPrint[j]}"); // Write the letter(s)
                    }
                    Console.WriteLine(); // Add a little space
                }

                Console.WriteLine($"Health left: {health}");

                if (wordPrint.Count == word.Length)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Set the foreground color to green
                    Console.WriteLine("\nYou guessed the word!"); // Tell the user they guessed the word
                    Console.ForegroundColor = ConsoleColor.White; // Set the foreground color to white (so default)
                    Console.WriteLine("\nDo you want to play again? [Y/N]"); // Ask the user if they want to play again
                    string choice = op.GetLetter(); // Save their choice in a string
                    if (choice == "n") // If they want to play again
                    {
                        Environment.Exit(0); // Exit the program
                    }
                }

                if (health == 0) // If their health is 0 (aka, they lost, noobs lol)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Set the foreground color to red
                    Console.WriteLine("You ran out of health!"); // Tell the user they lost
                    Console.WriteLine($"The word was {word} and you guessed {wordPrint.Count} letters correctly"); // Tell the user the word and how many letters they guessed correctly
                    Console.ForegroundColor = ConsoleColor.White; // Set the foreground color to white (so default)
                    Console.WriteLine("\nDo you want to play again? [Y/N]"); // Ask the user if they want to play again
                    string choice = op.GetLetter(); // Save their choice in a string
                    if (choice == "n") // If they want to play again
                    {
                        Environment.Exit(0); // Exit the program
                    }
                }

                Console.WriteLine(); // Add a little space
            }
        } while (true); // Loop the program forever (until the user exits)

    }

    // Create a new class to store methods
    class OutPut 
    {
        // Generate a random word from a list based on the length of the wordlength
        public string RandomWord(int length)
        {
            // Create a "empty" string 
            // It's done this way since c# doesn't allow me to return the string without an declaration in the same scope
            string word = "";
            string alphabet = "abcdefghijklmnopqrstuvwxyz"; // Create a list of the alphabet
            
            Random random = new Random(); // Make use of the random class

            // Create a for loop to generate a random word
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(alphabet.Length); // Get a random number between 0 and 25
                word += alphabet[index]; // Add the letter to the word based on the random index
            }
            
            return word; // Return the word
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
    }
}