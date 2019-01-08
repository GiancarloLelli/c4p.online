using cfp.online.Shared.Models;
using cfp.online.Shared.Validation;
using Newtonsoft.Json;
using System;
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

            var root = "http://localhost:51903";
            var countryCode = arguments.Length > 0 ? arguments[0] : string.Empty;
            var url = $"{root}/Data/GetAvailableCallForPapers/10/{countryCode}";

            if (string.IsNullOrEmpty(countryCode) || !AreaValidator.Validate(countryCode))
            {
                Console.WriteLine($"C4P Online v{versionString}");
                Console.WriteLine("Usage: c4p <country>");
                Console.WriteLine("Example: c4p [NA|SA|AF|EU|AUS]");
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

                    if (typedResponse.Proposals == null)
                    {
                        Console.WriteLine("There are no active Call for Papers at the moment.");
                        Console.WriteLine($"Check back later or submit yours @ {root}");
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
