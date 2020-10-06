using System;
using System.Collections.Generic;
using System.Text;
using ContosoCore.Models.Entities.Base;

namespace ContosoCore.DAL.Repos.Base
{
    public interface IRepos<T> where T: EntityBase
    {
        int Count { get; }
        T Find(int? id);
        T GetFirst();
        IEnumerable<T> GetAll();
        int Add(T entity, bool persist = true);
        int Update(T entity, bool persist = true);
        int Delete(T entity, bool persist = true);
        int SaveChanges();
    }
}
