using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightGame
{
    public class StarsWarsPeople
    {
        public List<Person> results { get; set; }
    }
    public class Person
    {
        //nada
        [JsonProperty("name")]
        public string PlayerName { get; set; }
        [JsonProperty("gender")]
        public string PlayerGender { get; set; }

    }
}
