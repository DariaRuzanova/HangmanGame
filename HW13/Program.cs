using System;

namespace HW13;

class Program
{
    static void Main(string[] args)
    {
        HangmanGame hangmanGame = new HangmanGame();
        string word = hangmanGame.GenerateWord();
        Console.WriteLine($"The word consist is {word.Length} letters.");
        Console.WriteLine("Try to guess the word.");
        while (hangmanGame.GameStatus==GameStatus.Inprogress)
        {
            Console.WriteLine("Pick a letter");
            char c = Console.ReadLine().ToCharArray()[0];

            string curState = hangmanGame.GuessLetter(c);
            Console.WriteLine(curState);
            Console.WriteLine($"Remaining tries = {hangmanGame.RemainingTries}");
            Console.WriteLine($"Tried letters: {hangmanGame.TriedLetters}");
        }

        if (hangmanGame.GameStatus == GameStatus.Lost)
        {
            Console.WriteLine($"You lost. \n The word was {hangmanGame.Word}");
        }
        else if(hangmanGame.GameStatus==GameStatus.Won)
        {
            Console.WriteLine("You won!");
        }

        Console.ReadLine();
    }
}