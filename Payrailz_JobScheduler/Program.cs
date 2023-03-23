using System.Net.Http.Headers;

namespace Payrailz_JobScheduler
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            System.Threading.Thread.Sleep(15000);
            CallWebAPIAsync().Wait();          
        }
        static async Task CallWebAPIAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44336/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync("api/PayRailz/DataBaseUpdate", new object());

                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.
                    Uri returnUrl = response.Headers.Location;
                    Console.WriteLine(returnUrl);
                }
            }
        }
    }
}