using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        TEntity Find(object id);
        IEnumerable<TEntity> GetAll();
        int SaveChanges();
    }
}
