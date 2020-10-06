using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ContosoCore.MVC.Configuration;
using ContosoCore.MVC.WebServiceAccess.Base;
using ContosoCore.Models.Entities;

namespace ContosoCore.MVC.WebServiceAccess
{
    public class WebApiCalls : WebApiCallsBase, IWebApiCalls
    {
        //Inyeccion

        public WebApiCalls(IWebServiceLocator ContosoCoreService) : base(ContosoCoreService)
        {

        }


        public HttpResponseHeaders Headers
        {
            get
            {
                return HeadersBase;
            }
        }

        public async Task<string> AddStudent(Student student)
        {
            var json = JsonConvert.SerializeObject(student);
            return await SubmitPostRequestAsync(StudentBaseUri, json);
        }

        public async Task DeleteStudent(int id)
        {
            await SubmitDeleteRequestAsyn(StudentBaseUri + "/" + id);
        }

        public async Task<Student> GetStudent(int id)
        {
            return await GetItemAsync<Student>(StudentBaseUri + "/" + id);
        }

        public async Task<IList<Student>> GetStudentsAll(string uri)
        {
            return await GetItemListAsync<Student>(StudentBaseUri);
        }

        public async Task<string> UpdateStudent(Student student)
        {
            var json = JsonConvert.SerializeObject(student);
            return await SubmitPutRequestAsyn(StudentBaseUri + "/" + student.Id, json);
        }
    }
}
