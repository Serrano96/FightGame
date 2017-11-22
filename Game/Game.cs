using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public const int DefaultPower = 10; 
        public const int DefaultLives = 2;
        private Random _random = new Random();

        public Game()
        {
            
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
                    Name = "Ruben",
                    Gender = Gender.Male,
                    Lives = DefaultLives,
                    Power = DefaultPower
                },
                new Player
                {
                    Name = "Alex",
                    Gender = Gender.Male,
                    Lives = DefaultLives,
                    Power = DefaultPower
                },
                new Player
                {
                    Name = "Javi",
                    Gender = Gender.Male,
                    Lives = DefaultLives,
                    Power = DefaultPower
                },
                new Player
                {
                    Name = "Raul",
                    Gender = Gender.Male,
                    Lives = DefaultLives,
                    Power = DefaultPower
                }

            };
        }

        public void Start()
        {
            Console.WriteLine(@"___________.__       .__     __      ________                       
\_   _____/|__| ____ |  |___/  |_   /  _____/_____    _____   ____  
 |    __)  |  |/ ___\|  |  \   __\ /   \  ___\__  \  /     \_/ __ \ 
 |     \   |  / /_/  >   Y  \  |   \    \_\  \/ __ \|  Y Y  \  ___/ 
 \___  /   |__\___  /|___|  /__|    \______  (____  /__|_|  /\___  >
     \/      /_____/      \/               \/     \/      \/     \/ ");
            Menu();
        }
        private void Menu()
        {
            Console.WriteLine("\nElige una opcion:");
            Console.WriteLine("###################\n");
            Console.WriteLine("1. Añadir jugador");
            Console.WriteLine("2. Status");
            Console.WriteLine("3. Luchar");
            Console.WriteLine("4. Ranking");
            Console.WriteLine("5. salir\n"); 

            ConsoleKeyInfo option = Console.ReadKey();

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
                    Console.WriteLine("\n\n ¿Estas seguro? (s/n)");
                    var answer = Console.ReadKey();
                    if (answer.KeyChar != 's')
                    {
                        Menu();
                    }
                    break;

                default:
                    Console.WriteLine("Solo hay que escribir un numero.\n ¿Es tan dificil?");
                    Menu();
                    break;
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
                foreach (var player in Players)
                {
                    player.Status();
                }
            }
           
            Console.ReadKey();
            Menu();
        }

        public void AddPlayer()
        {
            string name = "";

            while(string.IsNullOrEmpty(name) || name.Length<3)
            {
                Console.WriteLine("\nEscribe el nombre del jugador:\n");
                name = Console.ReadLine();
            }

            Gender? gender = null;

            while(gender == null)
            {
                Console.WriteLine("\nElige sexo:\n\n1.Femenino\n2.Masculino");
                var genderKey = Console.ReadKey();

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

            Console.WriteLine("\n\nJugador añadido");
            player.Status();
            Console.ReadKey();
            Menu();
        }
        public void Fight()
        {
            int idPlayer1;
            int idPlayer2;

            if (Players.Count > 1)
            {
                do
                {
                    idPlayer1 = _random.Next(0, Players.Count);
                    idPlayer2 = _random.Next(0, Players.Count);

                }while (idPlayer1 == idPlayer2);

                var player1 = Players[idPlayer1];
                var player2 = Players[idPlayer2];
                var damge = _random.Next(1, 5);

                player2.Power -= damge;
                Console.WriteLine($"\n{player1.Name} ha zurrado a {player2.Name}");
                if (player2.Power <= 0)
                {
                    player2.Lives --;
                    player1.Gems ++;
                    if (player2.Lives > 0)
                    {
                        Console.WriteLine($"{player2.Name} ha perdido una vida");
                    }
                    else
                    {
                        Console.WriteLine($"{player2.Name} ha muerto en combate");
                    }
                }
                Console.WriteLine("\nPulsa intro para luchar de nuevo. "+
                    "Cualquier otra teca para ver el menu");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    Fight();
                    Console.Clear();
                }
                else
                {
                    
                    Menu();
                    Console.Clear();
                }


            }
            else
            {
                Console.WriteLine("\nNo hay suficientes jugadores");
            } 
            Menu();
            

        }
        public void Ranking()
        {
        }
    }
}
