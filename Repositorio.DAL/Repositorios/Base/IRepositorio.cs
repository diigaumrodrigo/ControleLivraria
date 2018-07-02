using System;
using System.Data.Entity;
using System.Linq;

namespace Repositorio.DAL.Repositorios.Base
{
    public interface IRepositorio<TEntity> where TEntity : class
    {
        DbContextTransaction BeginTransaction();
        IQueryable<TEntity> GetAll();
        IQueryable<TObject> GetAllSelectColumn<TObject>(Func<TEntity, TObject> parameters);
        IQueryable<TEntity> GetWhere(Func<TEntity, bool> predicate);
        TObject GetMaxIdOrNull<TObject>(Func<TEntity, bool> predicate, Func<TEntity, TObject> column);
        TEntity FirstOrDefaultWhereCondition(Func<TEntity, bool> predicate);
        TEntity Find(params object[] key);
        void Update(TEntity obj);
        void SaveAllChanges();
        void Add(TEntity obj);
        void Delete(Func<TEntity, bool> predicate);
    }
}
