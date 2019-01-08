using cfp.online.Shared.Models;
using cfp.online.Shared.Validation;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace cfp.online.Tool
{
    class Program
    {
        static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        static async Task MainAsync(string[] arguments)
        {
            var versionString = Assembly.GetEntryAssembly()
                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                .InformationalVersion
                                .ToString();

            var recordCount = 10;
            var root = "http://c4ponline.azurewebsites.net";
            var countryCode = arguments.Length == 1 ? arguments[0] : string.Empty;
            var countCheck = arguments.Length == 2 ? int.TryParse(arguments[1], out recordCount) : false;

            var url = $"{root}/Data/GetAvailableCallForPapers/{recordCount}/{countryCode}";

            if (string.IsNullOrEmpty(countryCode) || !AreaValidator.Validate(countryCode))
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine($"C4P Online v{versionString}");
                Console.WriteLine("Usage: c4p <country> [count]");
                Console.WriteLine("Example: c4p [NA|SA|AF|EU|AUS]");
                Console.Write(Environment.NewLine);
                return;
            }

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    var typedResponse = JsonConvert.DeserializeObject<CallForPaperResponse>(stringData);
                    Console.Write(Environment.NewLine);

                    if (typedResponse.Proposals.Count() == 0)
                    {
                        Console.WriteLine($"There are no active Call for Papers at the moment in {countryCode}.");
                        Console.WriteLine($"Check back later or submit yours @ {root}");
                        Console.Write(Environment.NewLine);
                        return;
                    }

                    Console.WriteLine($"C4P Online v{versionString}");
                    Console.WriteLine($"Here are the available Call for Papers in {countryCode}");

                    foreach (var item in typedResponse.Proposals)
                    {
                        Console.WriteLine("--------------------------");
                        Console.WriteLine($"Conference Name: {item.ConferenceName}");
                        Console.WriteLine($"URL: {item.Website}");
                        Console.WriteLine($"Ends: {item.EndDate.ToString("MM/dd/yyyy")}");
                    }

                    Console.Write(Environment.NewLine);
                }
                else
                {
                    Console.WriteLine($"Sorry, there was an error fetching the available Call for Papers in {countryCode}");
                }
            }
        }
    }
}
