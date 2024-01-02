using System;
using System.IO;
using System.Net;

namespace HW13;

public class HangmanGame
{
    private readonly int allowedMisses;
    private bool[] openIndexes;
    private int triesCounter = 0;
    private string triedLetters;
    public GameStatus GameStatus { get; private set; } = GameStatus.NonStarted;
    public string Word { get; private set; }

    public string TriedLetters
    {
        get
        {
            var chars = triedLetters.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }
    }

    public int RemainingTries => allowedMisses - triesCounter;

    public HangmanGame(int allowedMisses = 6)
    {
        if (allowedMisses is < 5 or > 8)
        {
            throw new ArgumentException("Number of allowed misses should be between 5 and 8");
        }
        this.allowedMisses = allowedMisses;
    }

    public string GenerateWord()
    {
        string[] words = File.ReadAllLines("//data//Daria//Rider//HW13//HW13//Hangman//WordsStockRus.txt");
        Random r = new Random(DateTime.Now.Millisecond);
        int randomIndex = r.Next(words.Length - 2);
        Word = words[randomIndex];
        openIndexes = new bool[Word.Length];
        GameStatus = GameStatus.Inprogress;
        return Word;
    }

    public string GuessLetter(char letter)
    {
        if (triesCounter == allowedMisses)
        {
            throw new InvalidOperationException($"Exceeded the max misses number: {allowedMisses}");
        }

        if (GameStatus != GameStatus.Inprogress)
        {
            throw new IOException($"Inappropriate status of the game: {GameStatus}");
        }

        bool openAny = false;
        string result = String.Empty;
        for (int i = 0; i < Word.Length; i++)
        {
            if (Word[i] == letter)
            {
                openIndexes[i] = true;
                openAny = true;
            }

            if (openIndexes[i])
            {
                result += Word[i];
            }
            else
            {
                result += "-";
            }
        }

        if (!openAny)
        {
            triesCounter++;
        }

        triedLetters += letter;
        if (IsWin())
        {
            GameStatus = GameStatus.Won;
        }
        else if(triesCounter ==allowedMisses)
        {
            GameStatus = GameStatus.Lost;
        }
        return result;
    }

    private bool IsWin()
    {
        foreach (var cur in openIndexes)
        {
            if (cur == false)
            {
                return false;
            }
        }

        return true;
    }
}