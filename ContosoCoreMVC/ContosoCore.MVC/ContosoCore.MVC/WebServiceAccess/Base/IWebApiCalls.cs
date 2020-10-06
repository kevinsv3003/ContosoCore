using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using ContosoCore.Models.Entities;

namespace ContosoCore.MVC.WebServiceAccess.Base
{
    public interface IWebApiCalls
    {
        Task<IList<Student>> GetStudentsAll(string uri);

        Task<Student> GetStudent(int id);

        Task<string> AddStudent(Student student);

        Task<string> UpdateStudent(Student student);

        Task DeleteStudent(int id);

        HttpResponseHeaders Headers { get; }
    }
}
