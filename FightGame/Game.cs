using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FightGame
{
    public class Game
    {
        public const int DefaultLives = 2;
        public const int DefaultPower = 10;

        public List<Player> Players { get; set; }

        private Random _random = new Random();

        public Game()
        {
            ConsoleHelper.WriteLine(@"___________.__       .__     __      ________                       
                            \_   _____/|__| ____ |  |___/  |_   /  _____/_____    _____   ____  
                             |    __)  |  |/ ___\|  |  \   __\ /   \  ___\__  \  /     \_/ __ \ 
                             |     \   |  / /_/  >   Y  \  |   \    \_\  \/ __ \|  Y Y  \  ___/ 
                             \___  /   |__\___  /|___|  /__|    \______  (____  /__|_|  /\___  >
                                 \/      /_____/      \/               \/     \/      \/     \/ by Ruben",ConsoleColor.Blue);
            Players = new List<Player>
            {
                new Player
                {
                    Name = "Alberto",
                    Gender = Gender.Male,
                    Lives = DefaultLives,
                    Power = DefaultPower
                },
                new Player
                {
                    Name = "Mary",
                    Gender = Gender.Female,
                    Lives = DefaultLives,
                    Power = DefaultPower
                },
                new Player
                {
                    Name = "Juan",
                    Gender = Gender.Male,
                    Lives = DefaultLives,
                    Power = DefaultPower
                },
                new Player
                {
                    Name = "Thor",
                    Gender = Gender.Male,
                    Lives = DefaultLives,
                    Power = DefaultPower
                },
            };
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
                Id = Guid.NewGuid(),
                Gender = gender.Value,
                Name = name,
                Power = DefaultPower,
                Lives = DefaultLives
            };

            Players.Add(player);

            Console.WriteLine("\n\nJugador añadido:");

            player.Status();

            Console.ReadKey(true);

            Menu();
        }

        public void Fight()
        {
            // hay más de un jugador? no = error, si = seguimos
            if (Players.Count < 2)
            {
                Console.WriteLine("\nNo hay suficientes jugadores");
            }
            else
            {
                // elegir un player aleatoriamente
                var indexPlayer1 = _random.Next(0, Players.Count);
                var player1 = Players[indexPlayer1];

                // elegir el segundo player aleatoriamente pero que no se repita
                int indexPlayer2 = 0;

                while (indexPlayer1 == indexPlayer2)
                {
                    indexPlayer2 = _random.Next(0, Players.Count);
                }
                
                var player2 = Players[indexPlayer2];
                var damage = _random.Next(1, 5);

                // quitamos power al player 2 (si llega a 0 o menor que 0, le quitamos una vida).
                player2.Power -= damage;
                Console.WriteLine($"{player1.Name} ha zurrado a {player2.Name}");
                // ConsoleHelper.WriteLine($"{player1.Name} ha zurrado a {player2.Name}", ConsoleColor.Blue);

                if (player2.Power <= 0)
                {
                    player2.Lives --;
                    player1.Gems ++;

                    if (player2.Lives > 0)
                        Console.WriteLine($"{player2.Name} ha perdido una vida");
                    else
                        Console.WriteLine($"{player2.Name} ha muerto");
                }

                Console.WriteLine("\nPulsa intro para luchar de nuevo. " +
                    "Cualquier otra tecla para ver menú");

                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.Enter)
                {
                    Fight();
                }
                else
                {
                    Menu();
                }
            }
        }

        public void Ranking()
        {

        }

        public void Status()
        {
            if (Players.Count == 0)
            {
                Console.WriteLine("\nNo hay jugadores");
            }
            else
            {
                Console.WriteLine($"\nNombre\t\tVidas\tPoder\tGemas\tSexo");
                Console.WriteLine($"------------------------------------------------");

                foreach (var player in Players)
                {
                    player.Status();
                }
            }
            
            Console.ReadKey(true);

            Menu();
        }
    }
}
