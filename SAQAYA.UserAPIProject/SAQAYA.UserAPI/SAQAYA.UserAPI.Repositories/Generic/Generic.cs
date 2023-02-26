using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SAQAYA.UserAPI.Entities.Entities;
using SAQAYA.UserAPI.Repositories;
using System.Linq.Expressions;

namespace Repositories
{
    public class Generic<T> where T : BaseModel
    {
        DbSet<T> dbSet;
        public EntitiesContext Context { get; set; }
        public Generic(EntitiesContext context)
        {
            Context = context;
            dbSet = Context.Set<T>();
        }

        #region Get
        /// <summary>
        /// 1- Get exist record
        /// </summary>
        /// <param name="id" type="string"></param>
        /// <returns name="entity" type="T"></returns>
        public T Get(string id)
        {
            return dbSet.FirstOrDefault(i => i.Id == id);
        }
        #endregion

        #region Post
        /// <summary>
        /// 1- Post new record
        /// </summary>
        /// <param name="T" type="T"></param>
        /// <returns name="entity" type="EntityEntry<T>"></returns>
        public EntityEntry<T> Post(T T)
        {
            return dbSet.Add(T);

        }
        #endregion

        #region Put
        /// <summary>
        /// 1- Update the record
        /// </summary>
        /// <param name="T" type="T"></param>
        /// <returns name="T" type="T"></returns>
        public T Put(T T)
        {
            if (!dbSet.Local.Any(i => i.Id == T.Id))
                dbSet.Attach(T);
            Context.Entry(T).State = EntityState.Modified;
            return T;
        }
        #endregion

        #region Delete
        /// <summary>
        /// 1- Delete the record
        /// </summary>
        /// <param name="T" type="T"></param>

        public void Delete(T T)
        {
            dbSet.Remove(T);
        }
        #endregion

        #region GetAll
        /// <summary>
        /// 1- Get all records
        /// </summary>
        /// <returns name="dbSet" type="IQueryable<T>"></returns>
        public IQueryable<T> GetAll()
        {
            return dbSet;
        }
        #endregion

    }
}
