// This code requires the Nuget package Microsoft.AspNet.WebApi.Client to be installed.
// Instructions for doing this in Visual Studio:
// Tools -> Nuget Package Manager -> Package Manager Console
// Install-Package Microsoft.AspNet.WebApi.Client

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HouseValueLibrary
{

    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    public class ServiceCall
    {
        public static async Task<double> InvokeRequestResponseService(string[] data, string apiKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/4bd409f228a84888bea3475e41cd0f96/services/2abc1bf6d31746d28751c352dfa4a125/execute?api-version=2.0&details=true");
                   
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"Id", "Pincode", "PropertyType", "FloorCategory", "Sqft", "Bedrooms", "Bathrooms", "Status", "Age", "PricePerSqft"},
                                Values = new string[,] {  { data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9] }  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var serviceResponse = JsonConvert.DeserializeObject<Response>(result);
                    string value = serviceResponse.Results.output1.value.Values[0][10];
                    return double.Parse(value);
                }
                else
                {
                    throw new Exception("Call failed");
                }
            }
        }
    }
}