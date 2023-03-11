using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IDBContextFactory
    {
        IBaseRepositorys<T> CreateRepository<T>(ISqlServerDBContext mydbcontext) where T : class;
    }
}
