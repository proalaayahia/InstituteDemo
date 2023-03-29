using InstituteDemo.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InstituteDemo.UI.Controllers
{
    public class CommonController<T> : Controller
    {
        private readonly Settings _settings;
        public CommonController(IOptions<Settings> option)
        {
            this._settings = option.Value;
        }
        protected async Task<IEnumerable<T>> getAllAsync(string path)
        {
            using (HttpClient client = new HttpClient())
            {
                string endpoint = _settings.ApiUrl + $"api/{path}";
                client.BaseAddress = new Uri(_settings.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                using (var Response = await client.GetAsync(endpoint))
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (Response.IsSuccessStatusCode)
                        {
                            var data = await Response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<IEnumerable<T>>(data);
                            return result;
                        }
                    }
                return null;
            }
        }
        protected async Task<T> getByIdAsync<T>(int id, string path) where T:class
        {
            using (HttpClient client = new HttpClient())
            {
                string endpoint = _settings.ApiUrl + $"api/{path}/{id}";
                client.BaseAddress = new Uri(_settings.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                using (var Response = await client.GetAsync(endpoint))
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (Response.IsSuccessStatusCode)
                        {
                            var data = await Response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<T>(data);
                            return result;
                        }
                    }
                return null;
            }
        }
        protected async Task<bool> postAsync(T model, string path)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                string endpoint = _settings.ApiUrl + $"api/{path}";
                client.BaseAddress = new Uri(_settings.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return await Task.FromResult(true);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "something went wrong!");
                        return await Task.FromResult(false);
                    }
                }
            }
        }
        protected async Task<bool> putAsync(T model, string path)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                string endpoint = _settings.ApiUrl + $"api/{path}";
                client.BaseAddress = new Uri(_settings.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                using (var Response = await client.PutAsync(endpoint, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return await Task.FromResult(true);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "something went wrong!");
                        return await Task.FromResult(false);
                    }
                }
            }
        }
        protected async Task<bool> removeAsync(int id, string path)
        {
            using (HttpClient client = new HttpClient())
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
                string endpoint = _settings.ApiUrl + $"api/{path}/{id}";
                client.BaseAddress = new Uri(_settings.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                using (var Response = await client.DeleteAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return await Task.FromResult(true);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "something went wrong!");
                        return await Task.FromResult(false);
                    }
                }
            }
        }
    }
}
