using IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryFactory: IDBContextFactory
    {
        public IBaseRepositorys<T> CreateRepository<T>(ISqlServerDBContext mydbcontext) where T : class
        {
            return new BaseRepositorys<T>(mydbcontext);
        }
    }
}
