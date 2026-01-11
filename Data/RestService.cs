using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ClinicaVeterinaraMobile.Models;
using System.Net.Security;

namespace ClinicaVeterinaraMobile.Data
{
    public class RestService
    {
        HttpClient client;

      
        string BaseUrl = "http://localhost:5244/api/Vets";

        public List<Vet> Items { get; private set; }

        public RestService()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            client = new HttpClient(handler);
        }

        public async Task<List<Vet>> RefreshDataAsync()
        {
            Items = new List<Vet>();

            string url = BaseUrl;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                url = "http://10.0.2.2:5244/api/Vets";
            }

            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Vet>>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task SaveVetAsync(Vet item, bool isNewItem = false)
        {
            string url = BaseUrl;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                url = "http://10.0.2.2:5244/api/Vets";
            }

            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                if (isNewItem)
                {

                    response = await client.PostAsync(url, content);
                }
                else
                {
                    response = await client.PutAsync(url + "/" + item.ID, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(@"\tVet successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteVetAsync(int id)
        {
            string url = BaseUrl;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                url = "http://10.0.2.2:5244/api/Vets";
            }

            try
            {
                var response = await client.DeleteAsync(url + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(@"\tVet successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}