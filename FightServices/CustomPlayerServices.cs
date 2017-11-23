using FightGame.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FightGame
{
    public interface IPlayerService
    {
        List<Player> GetPlayers();
    }

    public class ApiPlayerService : IPlayerService
    {
        private const string ApiUrl = "https://swapi.co/api/people/";

        public List<Player> GetPlayers()
        {
            var httpClient = new HttpClient();
            Task<string> task = httpClient.GetStringAsync(ApiUrl);

            //Forma 1 de recuperar valor
            //Task.Run(async () =>
            //{
            //    string result = await task;
            //});
            //Forma 2 e recuperaar valor
            string result = task.Result;
            StarsWarsPeople people = JsonConvert.DeserializeObject<StarsWarsPeople>(result);

            //var players = new List<Player>();
            //foreach(var person in people.results)
            //{
            //    players.Add(new Player()
            //    {
            //        Id = ++Game.LastId,
            //        Name = person.name,
            //        Gender = person.name == "male"? Gender.Male : Gender.Female,
            //        Lives = Game.DefaultLives,
            //        Power = Game.DefaultPower
            //    });
            //}
            var players = people.results.Select(person => new Player
            {
                Id = ++GameModel.LastId,
                Name = person.PlayerName,
                Gender = person.PlayerName == "male"? Gender.Male : Gender.Female,
                Lives = GameModel.DefaultLives,
                Power = GameModel.DefaultPower
            });
            return players.ToList();
        }
    }
    public class CustomPlayerServices : IPlayerService
    {
        public List<Player> GetPlayers()
        {
          return new List<Player>
            {
                new Player
                {
                    Id = ++GameModel.LastId,
                    Name = "Real Madrid",
                    Gender = Gender.Male,
                    Lives = GameModel.DefaultLives,
                    Power = GameModel.DefaultPower
                },
                new Player
                {
                    Id = ++GameModel.LastId,
                    Name = "Barcelona",
                    Gender = Gender.Female,
                    Lives = GameModel.DefaultLives,
                    Power = GameModel.DefaultPower
                },
                new Player
                {
                    Id = ++GameModel.LastId,
                    Name = "Juventus",
                    Gender = Gender.Male,
                    Lives = GameModel.DefaultLives,
                    Power = GameModel.DefaultPower
                },
                new Player
                {
                    Id = ++GameModel.LastId,
                    Name = "Ballern",
                    Gender = Gender.Male,
                    Lives = GameModel.DefaultLives,
                    Power = GameModel.DefaultPower
                },
                new Player
                {
                    Id = ++GameModel.LastId,
                    Name = "Culrural",
                    Gender = Gender.Male,
                    Lives = GameModel.DefaultLives,
                    Power = GameModel.DefaultPower
                },
            };
        }
    }
}
