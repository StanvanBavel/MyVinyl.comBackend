using Newtonsoft.Json;
using System.Text;

namespace MyVinyl.com.Logging
{

        public class logData
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public bool IsActive { get; set; } = true;


        public logData(string Name, string Description, string Imaga)
            {
                //this.Id = Id;
                this.Name = Name;
                this.Description = Description;
                this.Image = Image;
                //this.IsActive = IsActive;
            }

            public static async Task LogToMicroserviceAsync(logData data, string url)
            {
                using (var httpClient = new HttpClient())
                {


                    var jsonPayload = JsonConvert.SerializeObject(data);
                    var payload = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(url, payload);

                    // Check the response status code
                    if (!response.IsSuccessStatusCode)
                    {
                        // Log an error message if the request failed
                        Console.WriteLine("Failed to log to microservice");
                    }
                }
            }

        }
    }
