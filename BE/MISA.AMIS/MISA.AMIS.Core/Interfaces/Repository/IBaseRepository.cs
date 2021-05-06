using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    public interface IBaseRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(Guid entityId);
        int Insert(T entity);

        int Update(T entity);

        int Delete(Guid entityId);
    }
}
