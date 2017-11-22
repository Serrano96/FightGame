using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Lives { get; set; }
        public int Power { get; set; }
        public Gender Gender{get; set;}
        public int Gems { get; set; }

        public void Status()
        {
            var gender = Gender == Gender.Male? "Hombre" : "Mujer";
            Console.WriteLine($"\n\n{Name}");
            Console.WriteLine("================");
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Vidas: {Lives}");
            Console.WriteLine($"Power: {Power}");
            Console.WriteLine($"Gemas: {Gems}");
            Console.WriteLine($"Sexo: {gender}\n\n");
        }

        public void Train()
        {

        }
    }
}
