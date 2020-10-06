using System;
using System.Collections.Generic;
using System.Text;
using ContosoCore.Models.Entities.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using ContosoCore.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContosoCore.DAL.Repos.Base
{
    public class RepoBase<T> : IDisposable, IRepos<T> where T : EntityBase
    {
        protected readonly ContosoCoreContext db;
        protected DbSet<T> Table;

        protected RepoBase()
        {
            db = new ContosoCoreContext();
            Table = db.Set<T>();
        }

        protected RepoBase(DbContextOptions<ContosoCoreContext> options)
        {
            db = new ContosoCoreContext();
            Table = db.Set<T>();
        }

        public ContosoCoreContext Context => db;

        public int Count => Table.Count();

        public virtual int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }

        bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
            }
            db.Dispose();
        }

        public T Find(int? id) => Table.Find(id);

        public virtual IEnumerable<T> GetAll() => Table;

        public T GetFirst() => Table.FirstOrDefault();

        public int SaveChanges()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public virtual int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }
    }
}
