using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Casino
{
    public delegate void GameResult(string result);

    public event GameResult GameOver;

    private List<string> gameHistory = new List<string>();

    public void Game()
    {
        Random random = new Random();
        int compvb = random.Next(0, 2);

        Console.WriteLine("Сделайте выбор: 0 - орел, 1 - решка");

        int uservb;
        while (true)
        {
            if (!int.TryParse(Console.ReadLine(), out uservb) || uservb < 0 || uservb > 1)
            {
                Console.WriteLine("Неверный ввод, попробуйте снова.");
                continue;
            }

            string result = uservb == compvb ? "Победа!" : "Поражение!";
            gameHistory.Add(result);

            GameOver.Invoke(result);

            Console.WriteLine("Хотите сыграть еще раз? (да/нет)");
            string playAgain = Console.ReadLine().ToLower();

            if (playAgain != "да")
            {
                saveHistory();
                break;
            }

            compvb = random.Next(0, 2);
            Console.WriteLine("Сделайте ваш выбор: 0 - орел, 1 - решка");
        }
    }

    private void saveHistory()
    {
        string filePath = "game_history.txt";
        File.WriteAllLines(filePath, gameHistory);
        Console.WriteLine("История игр сохранена в файле.");
    }

    public void showHistory()
    {
        if (gameHistory.Any())
        {
            Console.WriteLine("История предыдущих игр:");
            foreach (var result in gameHistory)
            {
                Console.WriteLine(result);
            }
        }
        else
        {
            Console.WriteLine("История игр - отсутствует.");
        }
    }
}

class Program
{
    static void Main()
    {
        Casino casino = new Casino();

        casino.GameOver += DisplayGameResult;

        casino.Game();
        casino.showHistory();
    }

    static void DisplayGameResult(string result)
    {
        Console.WriteLine("Результат игры: " + result);
    }
}