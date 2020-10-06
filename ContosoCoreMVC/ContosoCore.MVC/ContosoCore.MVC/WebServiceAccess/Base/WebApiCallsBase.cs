using ContosoCore.MVC.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ContosoCore.MVC.WebServiceAccess.Base
{
    public abstract class WebApiCallsBase
    {
        protected readonly string ServiceAddress;
        protected readonly string StudentBaseUri;
        protected HttpResponseHeaders _headers;

        protected WebApiCallsBase(IWebServiceLocator ContosoService)
        {
            ServiceAddress = ContosoService.ServiceAddress;
            StudentBaseUri = $"{ServiceAddress}api/Student";
        }

        internal async Task<IList<T>> GetItemListAsync<T>(string uri) where T : class, new()
        {
            try
            {
                return JsonConvert.DeserializeObject<IList<T>>(await GetJsonFromGetResponseAsync(uri));
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal async Task<string> GetJsonFromGetResponseAsync(string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"The Call to {uri} failed. Status code:{response.StatusCode}");
                    }

                    _headers = response.Headers;
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        internal async Task<T> GetItemAsync<T>(string uri) where T : class, new()
        {
            try
            {
                var json = await GetJsonFromGetResponseAsync(uri);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        //Metodo para agregar
        protected async Task<string> SubmitPostRequestAsync(string uri, string json)
        {
            using (var client = new HttpClient())
            {
                var task = client.PostAsync(uri, CreateStringContent(json));
                return await ExecuteRequestAndProcessResponse(uri, task);
            }
        }

        //Metodo para modificar
        protected async Task<string> SubmitPutRequestAsyn(string uri, string json)
        {
            using (var client = new HttpClient())
            {
                Task<HttpResponseMessage> task = client.PutAsync(uri, CreateStringContent(json));
                return await ExecuteRequestAndProcessResponse(uri, task);
            }
        }

        //Metodo para eliminar
        protected async Task SubmitDeleteRequestAsyn(string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(uri);
                    var response = await deleteAsync;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Status error: {response.StatusCode.ToString()}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        protected static async Task<string> ExecuteRequestAndProcessResponse(string uri, Task<HttpResponseMessage> task)
        {
            try
            {
                var response = await task;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"The call to {uri} failed. Status code: {response.StatusCode}");
                }
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        protected StringContent CreateStringContent(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        internal HttpResponseHeaders HeadersBase { get { return _headers; } }


    }
}
