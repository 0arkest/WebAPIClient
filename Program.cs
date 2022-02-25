// Activity # 5
// Wei Chen

using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace WebAPIClient
{
    class Pokémon
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        public List<Types> Types { get; set; }

    }

    public class Type
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Types
    {
        [JsonProperty("type")]
        public Type Type;
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Pokémon name. Press Enter without writing a name to quit the program.");

                    var PokémonName = Console.ReadLine();

                    if (string.IsNullOrEmpty(PokémonName))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://pokeapi.co/api/v2/pokemon/" + PokémonName.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var pokémon = JsonConvert.DeserializeObject<Pokémon>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("Pokemon #" + pokémon.Id);
                    Console.WriteLine("Name: " + pokémon.Name);
                    Console.WriteLine("Weight: " + pokémon.Weight + "lb");
                    Console.WriteLine("Height: " + pokémon.Height + "ft");
                    Console.WriteLine("Type(s):");
                    pokémon.Types.ForEach(t => Console.WriteLine(" " + t.Type.Name));
                    Console.WriteLine("\n---");
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR. Please enter a valid Pokémon name!");
                }
                
            }
        }
    }
}