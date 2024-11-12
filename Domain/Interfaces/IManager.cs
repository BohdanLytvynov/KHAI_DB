using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IManager<TEntity>
    {        
        IEnumerable<TEntity> GetAll();
        TEntity? GetById<TId>(TId id);
        bool Add(params TEntity[] entities);
        bool Remove(TEntity entity);
        bool Remove<TId>(TId Id);
        bool Edit<Tid>(Tid Id, TEntity newEntity);
    }
}
