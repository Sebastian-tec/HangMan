

using HangMan;

class Program
{
    static void Main(string[] args)
    {
        do
        {

            OutputMethods op = new OutputMethods(); // Create a new instance of the HangMan class
            int length; // Create an int to hold the number of 
            int health = 10; // Create a int to hold the number of health/
            string word; // Create a string to hold the word

            Console.Title = "HangMan"; // Set the title of the window
            op.Layout(); // Call the Layout method

            // Create a do-while loop to secure the user input
            do
            {
                Console.Write("Word length: "); // Ask the user for the wordlength
            } while (!int.TryParse(Console.ReadLine(), out length) || length > 14); // If the user input is not a number, ask again


            word = op.RandomWord(length); // Get a random word from the OutPut class
            List<int> wordPos = new List<int>(); // Create an array to store the word
            List<string> wordPrint = new List<string>(); // Create an array to store the word
            // Console.WriteLine(word); // Debug
            string wordPlaceHolder = word;


            // Create a while loop to check if the user has any health left
            while (health > 0)
            {
                op.FullLayout(health, word, wordPrint, wordPos);
                string key = "";
                do
                {
                    Console.WriteLine(word);
                    Console.WriteLine("Type a letter to guess the word"); // Ask for their guess
                    key = op.GetLetter(); // Save their guess in a string
                    op.ClearCurrentConsoleLine(); // Clear the line
                } while (!key.All(char.IsLetter));

                if (!wordPlaceHolder.Contains(key))
                {
                    health--;
                    Console.WriteLine($"Wrong! {key} is not in the word!");
                }

                while (wordPlaceHolder.Contains(key))
                {
                    int ind = word.IndexOf(key);
                    wordPrint.Add(key);
                    wordPos.Add(ind);
                    wordPlaceHolder = wordPlaceHolder.Replace(key, "_");
                    Console.WriteLine($"Correct! {key} is in the word!");
                }
                


                if (health == 0) // If their health is 0 (aka, they lost, noobs lol)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Set the foreground color to red
                    op.FullLayout(health, word, wordPrint, wordPos);
                    
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

                if (wordPrint.Count == word.Length)
                {
                    op.FullLayout(health, word, wordPrint, wordPos);
                    Console.ForegroundColor = ConsoleColor.Green; // Set the foreground color to green
                    Console.WriteLine("\nYou guessed the word!"); // Tell the user they guessed the word
                    Console.ForegroundColor = ConsoleColor.White; // Set the foreground color to white (so default)
                    Console.WriteLine("\nDo you want to play again? [Y/N]"); // Ask the user if they want to play again
                    string choice = op.GetLetter(); // Save their choice in a string
                    if (choice == "n") // If they want to play again
                    {
                        Environment.Exit(0); // Exit the program
                    }
                    health = 0;
                }

                Console.WriteLine(); // Add a little space
            }
        } while (true); // Loop the program forever (until the user exits)
    }
}