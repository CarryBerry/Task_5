using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL.Models;
using Task_4.Models;

namespace Task_4.DAL
{
    public class Repository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        //private UsersContext context;
        //private DbSet<TEntity> dbSet;

        //public Repository(UsersContext context)
        //{
        //    this.context = context;
        //    this.dbSet = context.Set<TEntity>();
        //}

        //public IEnumerable<TEntity> GetAll()
        //{
        //    return dbSet.ToList();
        //}

        //public TEntity GetById(int id)
        //{
        //    return dbSet.Find(id);
        //}

        //public void Insert(TEntity item)
        //{
        //    dbSet.Add(item);
        //}

        //public void Delete(int id)
        //{
        //    TEntity item = dbSet.Find(id);
        //    if (item != null)
        //    {
        //        dbSet.Remove(item);
        //    }
        //}

        //public IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate)
        //{
        //    return dbSet.Where(predicate).ToList();
        //}

        //public virtual int? GetId(TEntity item)
        //{
        //    throw new NotImplementedException();
        //    //var tmp = dbSet.FirstOrDefault(x => (x.Id == item.Id));
        //    //if (tmp == null)
        //    //{
        //    //    return null;
        //    //}
        //    //else
        //    //{
        //    //    return tmp.Id;
        //    //}
        //}

        //public void Update(TEntity item)
        //{
        //    context.Entry(item).State = EntityState.Modified;
        //}

        //public void Save()
        //{
        //    context.SaveChanges();
        //}
    }
}
