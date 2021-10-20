/*
 * Adapted from Programming is Fun: C# Adventure Game 
 * http://programmingisfun.com/learn/c-sharp-adventure-game
*/

using System;                      // NAMESPACE used for Console class
using System.Threading;            // NAMESPACE used for Thread.Sleep method
using System.Collections.Generic;  // NAMESPACE used for List class
using System.Linq;                 // NAMESPACE used for array methods

// NAMESPACE: Adventure
namespace Adventure
{
  // CLASS: Game
  public static class Game
  {
    // ATTRIBUTES
    static string CharacterName;
    static bool hasVisitedForest = false;
    static List<string> Inventory = new List<string>();
    static string[] allRandomItems = {"a compass", "a string", "some duct tape", "a duck", "a key", "the secret of reincarnation"};
    static string[] randomItems = allRandomItems.ToArray();  // Make a copy of the allRandomItems array so that we can add remove items


    // METHOD: StartGame
    public static void StartGame()
    {
      Console.Clear();
      Console.WriteLine("WELCOME TO THE ADVENTURE GAME\n\nTry not to die... Good luck!\n");
      NameCharacter();
      ChoosePath();
    }    
    // METHOD: ResetGame
    public static void ResetGame()
    { 
      // Reset Attributes
      CharacterName = "";  // Reset the CharacterName
      hasVisitedForest = false;  // Reset the state variables for visiting locations
      Inventory.Clear();  // Remove all of the items from the Inventory
      randomItems = allRandomItems.ToArray(); // Make a new copy of the original items

      // Restart Game
      StartGame();
    }    
    // METHOD: EndGame
    public static void EndGame()
    {
      Console.WriteLine("\nWell, you made it to The End... Whew!\nLet's see what you've got.");
      Thread.Sleep(1500);
      PrintInventory();
      Thread.Sleep(1500);
      if (Inventory.Contains("a key"))
      {
        Console.Write("You found the ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("key");
        Console.ResetColor();
        Console.Write("... You can unlock the door and leave! Yay!!\n");
      }
      else
      {
        Console.Write("Oh bummer... You didn't find the ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("key");
        Console.ResetColor();
        Console.Write(" so you are trapped here forever.\n");
      }
    }

    // METHOD: Prompt
    static void Prompt()
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.Write("> ");
      Console.ResetColor();
    }
    
    // METHOD: GetPlayerInput()
    static string GetPlayerInput()
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.Write("> ");

      // Read player input
      Console.ForegroundColor = ConsoleColor.Yellow;

      string input = Console.ReadLine();

      Console.ResetColor();

      return input.ToUpper();

    }

    // METHOD: NameCharacter
    static void NameCharacter()
    {
      // Ask the player what name they want for their character 
      // then store it in the CharacterName attribute.
      Console.WriteLine("What would you like your character's name to be?");
      CharacterName = GetPlayerInput();

      Console.Write("\nGreat! Your character is now named ");
      PrintCharacterName();
      Console.Write(".\n\n");
     
    }

    // METHOD: PrintCharacterName()
    static void PrintCharacterName()
    {
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.Write(CharacterName);
      Console.ResetColor();
    }

    // METHOD: PrintInventory
    static void PrintInventory()
    {
      if (Inventory.Count == 0)
      {
        Console.WriteLine("\nThere is nothing in your inventory.. So sad :(\n");
      } 
      else
      {
        Console.WriteLine("\nHere is your inventory:");
        
        // Loop through the Inventory list with foreach and print out items
        foreach (string item in Inventory)
        {
          Console.Write(" - ", item);
          Console.ForegroundColor = ConsoleColor.Blue;
          Console.WriteLine(item);
          Console.ResetColor();
        }
        Console.Write("\n");
      }

    }

    // METHOD: CheckForReincarnation
    static void CheckForReincarnation()
    {
      int waitTime = 5; //seconds
      if (Inventory.Contains("the secret of reincarnation"))
      {
        Thread.Sleep( 2000 );
        Console.WriteLine("Lucky you!!! You know the the secret of reincarnation.");
        Console.WriteLine("You will now be reborn in {0} seconds.", waitTime);
        Thread.Sleep( waitTime * 1000 );
        Console.Clear();
        ResetGame();
      } 
      else
      {
        // Completely end the game by stopping the program
        Environment.Exit(0);
      }
    }

    // METHOD: FindRandomItem
    static void FindRandomItem()
    {      
      // Create a Random number generator
      Random rnd = new Random();  

      if(randomItems.Length > 0)
      {
        // Return a random number in the array index range
        int result = rnd.Next(0, randomItems.Length); 
        
        // Add the random item to the Inventory
        Inventory.Add(randomItems[result]);

        // Tell the player what they found
        Console.Write("\nYou found ", randomItems[result]);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(randomItems[result]);
        Console.ResetColor();
        Console.Write("!\n\n");

        // Remove the found item from the array
        // Adapted from: https://www.techiedelight.com/remove-specified-element-from-array-csharp/
        randomItems = randomItems.Except(new string[] { randomItems[result] }).ToArray();
      }

    }

    // METHOD: ChoosePath
    static void ChoosePath()
    {  
      // Choices:  FOREST - BEACH - INVENTORY
      string choices = "Forest or Beach? Inventory?";
      PrintCharacterName();
      Console.WriteLine(", choose a path or see what you've got:");

      // Keep asking the player until there is a valid response
      bool notValidResponse = true;
      do 
      {
        // Print the choices 
        Console.WriteLine(choices);

        // Read and process player input
        switch(GetPlayerInput())
        {
          case "F":
          case "FOREST":
            Console.WriteLine("You've chosen the path to the Forest!");
            notValidResponse = false;
            Forest();
            break;
          case "B":
          case "BEACH":
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You've chosen the path to the Beach!\nYou can't play this game at the beach, but have fun! Bye :)\n");
            Console.ResetColor();
            notValidResponse = false;
            CheckForReincarnation();
            break;
          case "I":
          case "INVENTORY":
            PrintInventory();
            break;
          default:
            Console.WriteLine("Huh?? ");
            break;
        }
      } 
      while(notValidResponse);

    }

    // METHOD: Forest
    static void Forest()
    {

      // If they have have visited, display this message: 
      if (hasVisitedForest)
      {
        Thread.Sleep(1000);
        Console.WriteLine("You're back in the Forest.");
      }
      // Otherwise, don't display a message but remember that they've visited.
      else
      {
        hasVisitedForest = true;
      }

      // Player finds a random item
      FindRandomItem();

      if(Inventory.Contains("a compass"))
      {
        Console.WriteLine("Your compass says that you are headed west.");
      }

      // Choices:  CLIMB - SWIM - GO BACK - INVENTORY
      string choices = "Climb or Swim? Go Back? Inventory?";
      Console.WriteLine("Now, do you want to...");

      // Keep asking the player until there is a valid response
      bool notValidResponse = true;
      do 
      {
        // Print the choices
        Console.WriteLine(choices);

        // Read and process player input
        switch(GetPlayerInput())
        {
          case "C":
          case "CLIMB":
            Console.WriteLine("You've chosen to climb a tree!");
            notValidResponse = false;
            ClimbTree();
            break;
          case "S":
          case "SWIM":
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You've chosen to swim in the river and you get eaten by killer fish!\n");
            Console.ResetColor();
            notValidResponse = false;
            CheckForReincarnation();
            break;
          case "G":
          case "GO BACK":
            Console.WriteLine("No problem.\n");
            Thread.Sleep(2000);
            Console.Clear();
            notValidResponse = false;
            ChoosePath();
            break;
          case "I":
          case "INVENTORY":
            PrintInventory();
            break;
          default:
            Console.Write("Huh?? ");
            break;
        }
      }
      while(notValidResponse);
    }

    // METHOD: ClimbTree
    static void ClimbTree()
    {
      // Choices:  EAT - THROW - GO BACK - INVENTORY
      string choices = "Eat or Throw? Go Back? Inventory?";
      Console.WriteLine("You see some fruit.");

      // Keep asking the player until there is a valid response
      bool notValidResponse = true;
      do 
      {
        // Print the choices
        Console.WriteLine(choices);

        // Read and process player input
        switch(GetPlayerInput())
        {
          case "E":
          case "EAT":
            Console.WriteLine("You ate the fruit and now you have super jumping power!");
            Thread.Sleep(2000);
            Inventory.Add("super jumping power");
            notValidResponse = false;
            break;
          case "T":
          case "THROW":
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You hit a monkey and it is really mad at you!");
            Thread.Sleep(2000);
            Console.ResetColor();
            Inventory.Add("fear of monkeys");
            notValidResponse = false;
            break;
          case "G":
          case "GO BACK":
            Console.WriteLine("No problem.\n");
            Thread.Sleep(2000);
            Console.Clear();
            notValidResponse = false;
            Forest();
            break;
          case "I":
          case "INVENTORY":
            PrintInventory();
            break;
          default:
            Console.WriteLine("Huh?? ");
            break;
        }
      }
      while(notValidResponse);
    }

  }

  class Program
  {
    // METHOD: Main - Default entry point for running program
    static void Main()
    {
      // Start the game.. This method is the starting point for all other choices!
      Game.StartGame();

      // End the game.. Print the Inventory and check for the key!
      Game.EndGame();

      // Wait to end the game until the player presses a key.
      Console.WriteLine("\nPress ENTER to go on about your day.\n");
      Console.Read();
    }

  }
}