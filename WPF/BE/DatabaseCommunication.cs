using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Konscious.Security.Cryptography;
using System.Collections.ObjectModel;

namespace Notepad.BE
{
    public class DatabaseCommunication
    {
        public async Task<string> PostDataAsync<T>(T data, string url)
        {
            using (var client = new HttpClient())
            {
                string jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(responseContent);
                    string message = jsonResponse["message"].ToString();

                    return message;
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(errorResponse);
                    string message = jsonResponse["detail"].ToString();

                    return message;
                }
            }
        }

        public async Task<string> PatchDataAsync<T>(T data, string url)
        {
            using (var client = new HttpClient())
            {
                string jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PatchAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(responseContent);
                    string message = jsonResponse["message"].ToString();

                    return message;
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(errorResponse);
                    string message = jsonResponse["detail"].ToString();

                    return message;
                }
            }
        }

        public async Task<TOutput> LoginAsync<TInput, TOutput>(TInput data, string url)
        {
            using (var client = new HttpClient())
            {
                string jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TOutput>(responseContent);
                }
                else
                {
                    var result = default(TOutput);
                    return result;
                }
            }
        }

        public async Task<ObservableCollection<T>> LoadDataFromDatabaseAsync<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        ObservableCollection<T> dataList = ParseJsonArray<T>(responseBody);
                        return dataList;
                    }
                    else
                    {
                        MessageBox.Show("Chyba pri získavaní dát z API. Kód odpovede: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba pri komunikácii s API: " + ex.Message);
                }

                return null;
            }
        }

        public string HashPassword(string password)
        {
            byte[] salt = Encoding.UTF8.GetBytes("4CzmS5HvI6gBmfxlB5BgqeJNe2QhU85h");

            if (!String.IsNullOrEmpty(password))
            {
                using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password)))
                {
                    hasher.Salt = salt;
                    hasher.DegreeOfParallelism = 8;
                    hasher.MemorySize = 65536;
                    hasher.Iterations = 4;

                    byte[] hashBytes = hasher.GetBytes(16);

                    string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                    return hash;
                }
            }
            return String.Empty;
        }

        private static ObservableCollection<T> ParseJsonArray<T>(string jsonArray)
        {
            try
            {
                JObject jsonObject = JObject.Parse(jsonArray);
                JArray notesArray = (JArray)jsonObject["notes"];
                ObservableCollection<T> notes = notesArray.ToObject<ObservableCollection<T>>();

                return notes;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba pri spracovaní JSON: " + ex.Message);
                return null;
            }
        }
    }
}

