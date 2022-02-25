// Activity # 5
// Author: Wei Chen
// Last Modified: 02/24/2022
// Description:
// From https://documenter.getpostman.com/view/8854915/Szf7znEe,
// Use Gender Detector(https://genderize.io/) API, a simple API to predict the gender of a person given their name.
// Note:
// The API is free for up to 1000 names/day only.

using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace WebAPIClient
{
    class Person
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("probability")]
        public float Probability { get; set; }

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
                    Console.WriteLine("Enter the person's name. Press Enter without writing a name to quit the program.");
                    Console.WriteLine("You can try names like peter, marry, etc.");

                    var PersonName = Console.ReadLine();

                    if (string.IsNullOrEmpty(PersonName))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://api.genderize.io?name=" + PersonName.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var person = JsonConvert.DeserializeObject<Person>(resultRead);

                    if (string.IsNullOrEmpty(person.Gender))
                    {
                        Console.WriteLine("ERROR. Invalid name, unable to predict that person's gender.");
                        Console.WriteLine("\n");
                    }
                    else
                    {
                        Console.WriteLine("---");
                        Console.WriteLine("The name of the person you entered: " + person.Name);
                        Console.WriteLine("Prediction of that person's gender: " + person.Gender);
                        Console.WriteLine("Certainty of the assigned gender: " + person.Probability);
                        Console.WriteLine("\n---");
                    }
                    
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR. Request limit reached!");
                }
                
            }
        }
    }
}