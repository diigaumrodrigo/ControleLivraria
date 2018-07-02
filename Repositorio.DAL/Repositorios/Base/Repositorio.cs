using System;
using System.Data.Entity;
using System.Linq;

namespace Repositorio.DAL.Repositorios.Base
{
    public class Repositorio<TEntity> : IDisposable, IRepositorio<TEntity>
        where TEntity : class
    {
        public DbContext ctx { get; set; }

        public Repositorio(DbContext ctx)
        {
            this.ctx = ctx;
        }        

        public DbContextTransaction BeginTransaction()
        {
            return ctx.Database.BeginTransaction(System.Data.IsolationLevel.Serializable);
        }

        public IQueryable<TEntity> GetAll()
        {
            return ctx.Set<TEntity>();
        }

        public IQueryable<TObject> GetAllSelectColumn<TObject>(Func<TEntity, TObject> parameters)
        {
            return ctx.Set<TEntity>().Select(parameters).AsQueryable();
        }

        public IQueryable<TEntity> GetWhere(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        public TObject GetMaxIdOrNull<TObject>(Func<TEntity, bool> predicate, Func<TEntity, TObject> column)
        {
            return GetAll().Where(predicate).Max(column);
        }

        public TEntity FirstOrDefaultWhereCondition(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).FirstOrDefault();
        }

        public TEntity Find(params object[] key)
        {
            return ctx.Set<TEntity>().Find(key);
        }

        public void Update(TEntity obj)
        {
            ctx.Entry(obj).State = EntityState.Modified;
        }

        public void SaveAllChanges()
        {
            ctx.SaveChanges();
        }

        public void Add(TEntity obj)
        {
            ctx.Set<TEntity>().Add(obj);
        }

        public void Delete(Func<TEntity, bool> predicate)
        {
            ctx.Set<TEntity>()
                .Where(predicate).ToList()
                .ForEach(del => ctx.Set<TEntity>().Remove(del));
        }

        public void Dispose()
        {
            if (ctx != null)
            {
                ctx.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
