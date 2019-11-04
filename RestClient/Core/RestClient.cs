using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace RestClient.Core
{
    public class RestClient
    {
        readonly HttpClient httpClient = new HttpClient();

        public HttpResponseMessage Get(string url)
        {
            httpClient.DefaultRequestHeaders.Clear();

            return httpClient.GetAsync(url).Result;
        }

        public HttpResponseMessage Post(string url, object objToPost)
        {
            httpClient.DefaultRequestHeaders.Clear();

            StringContent content = new StringContent(JsonConvert.SerializeObject(objToPost), Encoding.UTF8, "application/x-www-form-urlencoded");
            return httpClient.PostAsync(url, content).Result;
        }

        public HttpResponseMessage Put(string url, object objToPut)
        {
            httpClient.DefaultRequestHeaders.Clear();

            StringContent content = new StringContent(JsonConvert.SerializeObject(objToPut), Encoding.UTF8, "application/x-www-form-urlencoded");
            return httpClient.PutAsync(url, content).Result;
        }

        public HttpResponseMessage Delete(string url)
        {
            httpClient.DefaultRequestHeaders.Clear();

            return httpClient.DeleteAsync(url).Result;
        }

        public int GetPostResponseId(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var testString = response.Content.ReadAsStringAsync().Result;
                Regex regex = new Regex("\"id\": (\\d+)"); 
                var result = regex.Match(testString).Groups[1].ToString();
                return Int32.Parse(result);
            }
            else
            {
                throw new ArgumentException($" Request was failed with status code {response.StatusCode}");
            }
        }
    }
}
