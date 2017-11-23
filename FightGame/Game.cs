using FightGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightGame
{
    public class Game
    {
        public List<Player> Players { get; set; }

        
        private Random _random = new Random(DateTime.Now.Millisecond);

        public Game()
        {
            ConsoleHelper.WriteLine(@"___________.__       .__     __      ________                       
                            \_   _____/|__| ____ |  |___/  |_   /  _____/_____    _____   ____  
                             |    __)  |  |/ ___\|  |  \   __\ /   \  ___\__  \  /     \_/ __ \ 
                             |     \   |  / /_/  >   Y  \  |   \    \_\  \/ __ \|  Y Y  \  ___/ 
                             \___  /   |__\___  /|___|  /__|    \______  (____  /__|_|  /\___  >
                                 \/      /_____/      \/               \/     \/      \/     \/ by Ruben",ConsoleColor.Cyan);
            IPlayerService playerService = new ApiPlayerService();
            Players = playerService.GetPlayers();
        }

        public void Run()
        {
            

            Menu();
            while (true)
            {
                ConsoleKeyInfo option = Console.ReadKey(true);
                if (option.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("\nHasta luego!");
                    Task.Run(async () => await Task.Delay(1500)).Wait();
                    break;
                }
                switch (option.KeyChar)
                {
                    case '1':
                        AddPlayer();
                        break;

                    case '2':
                        Status();
                        break;

                    case '3':
                        Fight();
                        break;

                    case '4':
                        Ranking();
                        break;

                    case '5':
                        Console.WriteLine("\n\n¿Estás seguro? (s/n)");
                        var answer = Console.ReadKey();
                        if (answer.KeyChar != 's')
                        {
                            Menu();
                        }
                        break;

                    default:
                        Console.WriteLine("\nSolo hay que escribir un número. ¿Es tan dificil?\n\n");
                        Menu();
                        break;
                }
            }
          
        }
        public void Ranking() { }
        private void Menu()
        {
            Console.WriteLine("\n\nElige una opción:\n");
            Console.WriteLine("1. Añadir jugador");
            Console.WriteLine("2. Status");
            Console.WriteLine("3. Luchar");
            Console.WriteLine("4. Ranking");
            Console.WriteLine("5. Salir");
        }

        public void AddPlayer()
        {
            string name = null;
            
            while(string.IsNullOrEmpty(name) || name.Length < 3)
            {
                Console.WriteLine("\n\nEscribe nombre del jugador (y presiona enter):");
                name = Console.ReadLine();
            }

            Gender? gender = null;

            while(gender == null)
            {
                Console.WriteLine("\nElige sexo:\n1. Femenino\n2. Masculino");
                var genderKey = Console.ReadKey(true);

                if (genderKey.KeyChar == '1')
                {
                    gender = Gender.Female;
                }
                else if (genderKey.KeyChar == '2')
                {
                    gender = Gender.Male;
                }
            }

            var player = new Player
            {
                Id = ++GameModel.LastId,
                Gender = gender.Value,
                Name = name,
                Power = GameModel.DefaultPower,
                Lives = GameModel.DefaultLives
            };

            Players.Add(player);

            Console.WriteLine("\n\nJugador añadido:");

            player.Status();

            
        }

        public void Fight()
        {
            var currentPlayers = Players
                .Where(x => x.Lives > 0)
                .ToList();

            // hay más de un jugador?
            if (currentPlayers.Count < 2)
            {
                ConsoleHelper.WriteLine("\nNo hay suficientes jugadores", ConsoleColor.Red);
                return;
            }
            int indexPlayer1;
            int indexPlayer2;
            do
            {
                indexPlayer1 = _random.Next(0, currentPlayers.Count);
                indexPlayer2 = _random.Next(0, currentPlayers.Count); ;

            } while (indexPlayer1 == indexPlayer2);
            var player1 = currentPlayers[indexPlayer1];
            var player2 = currentPlayers[indexPlayer2];
            // elegir un player aleatoriamente
            //var indexPlayer1 = _random.Next(0, currentPlayers.Count);
            // var player1 = currentPlayers[indexPlayer1];

            // elegir el segundo player aleatoriamente pero que no se repita
            //int indexPlayer2 = _random.Next(0, currentPlayers.Count); ;
            //while (indexPlayer1 == indexPlayer2)
            //    indexPlayer2 = _random.Next(0, currentPlayers.Count);

            //var player2 = currentPlayers[indexPlayer2];

            // quitamos power al player 2 (el nivel de daño será aleatorio entre 1 y 5)
            var damage = _random.Next(1, 5);
            player2.Power -= damage;

            ConsoleHelper.WriteLine($"==> {player1.Name} ha zurrado a {player2.Name}",
                ConsoleColor.Blue);

            if (player2.Power <= 0)
            {
                player2.Lives--;
                player2.Power = player2.Lives > 0 ? GameModel.DefaultPower : 0;

                if (player2.Lives > 0)
                {
                    ConsoleHelper.WriteLine($"{player2.Name} ha perdido una vida",
                        ConsoleColor.Yellow);
                }
                else
                {
                    player2.Gems = 0;
                    ConsoleHelper.WriteLine($"{player2.Name} ha muerto",
                        ConsoleColor.Red);
                }

                player1.Gems++;

                ConsoleHelper.WriteLine($"{player1.Name} ha ganado una gema. " +
                    $"Ahora tiene {player1.Gems} en total.",
                    ConsoleColor.Green);

                // cada 3 gemas le damos una vida
                if (player1.Gems == 3)
                {
                    player1.Lives++;
                    player1.Gems = 0;

                    ConsoleHelper.WriteLine($"{player1.Name} ha ganado una VIDA!!",
                        ConsoleColor.Magenta);
                }

                // comprobar si hay ganador
                if (Players.Count(x => x.Lives > 0) == 1)
                {
                    Console.WriteLine("\n\n+============================================+");
                    Console.WriteLine("+============================================+");
                    Console.WriteLine("+============================================+");
                    ConsoleHelper.WriteLine($"      {player1.Name} HA GANADO", ConsoleColor.Cyan);
                    Console.WriteLine("+============================================+");
                    Console.WriteLine("+============================================+");
                    Console.WriteLine("+============================================+");
                }
            }
        }


        public void Status()
        {
            if (Players.Count == 0)
            {
                Console.WriteLine("\nNo hay jugadores");
            }
            else
            {
                Console.WriteLine($"\nNombre\t\t\t\t\t\tId\tVidas\tPoder\tGemas\tSexo");
                Console.WriteLine($"----------------------------------------------------------------------------------");

                var ordered = Players
                    .OrderByDescending(x => x.Lives)
                    .ThenByDescending(x => x.Power)
                    .ThenByDescending(x => x.Gems);

                foreach (var player in ordered)
                {
                    var status = player.Status();
                    var color = player.Lives > 0 ? ConsoleColor.White : ConsoleColor.White;
                    ConsoleHelper.WriteLine(status, color);
                }
            }
        }
    }
}
