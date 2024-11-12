using Data.Models.Tanks;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TankManagers
{
    public class ManagerBase<TEntity> : IManager<TEntity>
    {
        public virtual bool Add(params TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual bool Edit<Tid>(Tid Id, TEntity newEntity)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            List<TEntity> entities = new List<TEntity>();



            return entities;
        }

        public virtual TEntity? GetById<TId>(TId id)
        {
            throw new NotImplementedException();
        }

        public virtual bool Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual bool Remove<TId>(TId Id)
        {
            throw new NotImplementedException();
        }
    }
}
