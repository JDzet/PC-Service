using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PC_Service
{
    public class ApiYandex
    {
        private const string YandexGeocoderApiUrl = "https://geocode-maps.yandex.ru/1.x/";
        private const string YandexApiKey = "e28e8e09-c6e2-4b2d-9804-ab2b804fd379";

        public async Task<List<string>> GetAutocompleteAddresses(string input)
        {
           if(input == null || input == "") 
            {
                return null;
            } 
            using (HttpClient client = new HttpClient())
            {
               
                string url = $"{YandexGeocoderApiUrl}?apikey={YandexApiKey}&geocode={Uri.EscapeDataString(input)}&format=json";
                var response = await client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var json = JObject.Parse(content);
                    var features = json["response"]["GeoObjectCollection"]["featureMember"];

                    List<string> addresses = new List<string>();
                    foreach (var feature in features)
                    {
                        string address = feature["GeoObject"]["metaDataProperty"]["GeocoderMetaData"]["text"].ToString();
                        addresses.Add(address);
                    }

                    return addresses;
                }
                else
                {
                    // Обработка ошибок
                    string errorMessage = $"Ошибка: {response.StatusCode}, {content}";
                    throw new Exception(errorMessage);
                }
            }
        }


    }
}
