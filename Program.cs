using System;

namespace Klasser_og_nedarvning
{
    class Program
    {
        static void Main(string[] args)
        {
            spil();
        }
        static void Scoretavle(int antal_deltagere, Player[] players)
        {
            Console.WriteLine("Indhold i skattekister:");
            for (int i = 0; antal_deltagere > i; i++)
            {
                Console.WriteLine("");
                Console.WriteLine($"{players[i].Navn}: {players[i].Score}");
            }
        }
        static void spil()
        {
            Console.Write("Indtast antal deltagere i dette spil: ");
            int antal_deltagere = int.Parse(Console.ReadLine());
            Player[] players = new Player[antal_deltagere];

            bool vinder = false;
            for (int i = 0; i != antal_deltagere; i++)
            {
                Console.Write($"Indtast deltager {i + 1}: ");
                Player player = new HumanPlayer(Console.ReadLine(), 0);
                players[i] = player;
            }

            Console.WriteLine("Tryk \"Spacebar\" for at kaste terningen");
            while (vinder == false)
            {
                for (int i = 0; i < antal_deltagere; i++)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Tryk \"S\" for at se Scoretavlen. Det er nu {players[i].Navn}s tur:");
                    Console.WriteLine("");
                    if (Console.ReadKey(true).Key == ConsoleKey.S)
                        do
                        {
                            Scoretavle(antal_deltagere, players);
                            Console.WriteLine($"Det er nu {players[i]}s tur. Tryk \"Spacebar\" for at kaste terningen ");
                        } while (Console.ReadKey(true).Key == ConsoleKey.S);
                    players[i].Play();
                    if (players[i].Score >= 20)
                    {
                        Scoretavle(antal_deltagere, players);
                        Console.WriteLine($"{players[i].Navn} har vundet!");
                        Console.WriteLine();
                        vinder = true;
                        break;
                    }
                }
            }
            Console.WriteLine("Spillet er slut.");
        }
    }
    class HumanPlayer
    {
        string navn;
        int score;
        static Random rnd = new Random();

        public Player(string navn, int score)
        {
            this.navn = navn;
            this.score = score;
        }
        public void Play()
        {
            int[] kast = new int[20];
            int sum = 0;
            bool runde = true;
            for (int kast_index = 0; runde == true; kast_index++) //runde start
            {

                kast[kast_index] = rnd.Next(1, 7);
                if (kast[kast_index] == 1)
                {
                    Console.WriteLine($"{navn} slår: 1 ");
                    runde = false;
                }
                else
                {
                    Console.Write($"{navn} slår:");
                    foreach (int nummer in kast)
                        if (nummer > 0)
                            Console.Write($" {nummer},");
                    Console.WriteLine(" Læg samlede kast i kisten? (tryk \"Enter\")");
                    var userinput = Console.ReadKey(true).Key;
                    if (userinput == ConsoleKey.Enter)
                    {
                        Console.WriteLine("");
                        Console.Write($"{navn} vælger at stoppe, ");
                        for (int i = 0; i != kast_index + 1; i++)
                            sum = sum + kast[i];
                        Console.Write("der bliver lagt ");
                        for (int i2 = 0; i2 <= kast_index; i2++)
                        {
                            if (i2 != kast_index)
                                Console.Write($" {kast[i2]} + ");
                            else
                                Console.Write($" {kast[i2]} ");
                        }
                        score = score + sum;
                        return;
                    }
                    else if (userinput != ConsoleKey.Spacebar)
                        while (userinput != ConsoleKey.Spacebar)
                        {
                            Console.WriteLine("Tryk \"Spacebar\" for at kaste igen");
                            userinput = Console.ReadKey(true).Key;
                        }
                    else
                        runde = true;
                }

            }
        }

        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }

        public string Navn
        {
            get
            {
                return navn;
            }
        }
    }
    class ComputerPlayer : Player
    {
        public ComputerPlayer(string navn, int score) :
            base(navn, score)
        {

        }
    }
}
