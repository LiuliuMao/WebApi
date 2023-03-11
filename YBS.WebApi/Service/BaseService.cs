using IRepository;
using IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected IDBContextFactory _ContextFactory { get; private set; }
        IBaseRepositorys<T> baseRepositorys;
        public BaseService(IDBContextFactory contextFactory, ISqlServerDBContext sqlServerDBContext)
        {
            _ContextFactory = contextFactory;
            baseRepositorys = _ContextFactory.CreateRepository<T>(sqlServerDBContext);
        }
        public BaseService()
        {

        }
        public T Add(T entity, bool isSave = true)
        {
            return baseRepositorys.Add(entity, isSave);
        }
        public async Task<T> AysAdd(T entity, bool isSave = true)
        {
            return await baseRepositorys.AysAdd(entity, isSave);
        }

        public void AddRange(IEnumerable<T> entitys, bool isSave = true)
        {
            baseRepositorys.AddRange(entitys, isSave);
        }

        public void Delete(T entity, bool isSave = true)
        {
            this.baseRepositorys.Delete(entity, isSave);
        }

        public void Delete(bool isSave = true, params T[] entitys)
        {
            this.baseRepositorys.Delete(isSave,entitys);
        }

        public async Task<int> AsyDelete(object id)
        {
            return await baseRepositorys.AsyDelete(id);
        }

        public int Delete(object id)
        {
            return baseRepositorys.Delete(id);
        }

        public void Delete(Expression<Func<T, bool>> @where, bool isSave = true)
        {
            baseRepositorys.Delete(@where, isSave);
        }

        public async Task<int> AysUpdate(T entity)
        {
            return await baseRepositorys.AysUpdate(entity);
        }

        public int Update(T entity)
        {
            return baseRepositorys.Update(entity);
        }
        public void Update(params T[] entitys)
        {
            baseRepositorys.Update(entitys);
        }

        public bool Any(Expression<Func<T, bool>> @where)
        {
            return baseRepositorys.Any(@where);
        }

        public int Count()
        {
            return baseRepositorys.Count();
        }

        public int Count(Expression<Func<T, bool>> @where)
        {
            return baseRepositorys.Count(@where);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> @where)
        {
            return baseRepositorys.FirstOrDefault(@where);
        }

        public T FirstOrDefault<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            return baseRepositorys.FirstOrDefault(@where, order,isDesc);
        }

        public IQueryable<T> Distinct(Expression<Func<T, bool>> @where)
        {
            return baseRepositorys.Distinct(@where);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> @where)
        {
            return baseRepositorys.Where(@where);
        }

        public IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            return baseRepositorys.Where<TOrder>(@where, order, isDesc);
        }

        public IEnumerable<T> Where<TOrder>(Func<T, bool> @where, Func<T, TOrder> order, int pageIndex, int pageSize, out int count, bool isDesc = false)
        {
            return baseRepositorys.Where<TOrder>(@where, order, pageIndex, pageSize,out count, isDesc);
        }

        public IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, int pageIndex, int pageSize, out int count, bool isDesc = false)
        {
            return baseRepositorys.Where<TOrder>(@where, order, pageIndex, pageSize, out count, isDesc);
        }

        public IQueryable<T> GetAll()
        {
            return this.baseRepositorys.GetAll();
        }

        public IQueryable<T> GetAll<TOrder>(Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            return this.baseRepositorys.GetAll(order, isDesc);
        }

        public T GetById<TType>(TType id)
        {
            return this.baseRepositorys.GetById(id);
        }
        public async Task<T> AysGetById<TType>(TType id)
        {
            return await this.baseRepositorys.AysGetById<TType>(id);
        }

        public TType Max<TType>(Expression<Func<T, TType>> column)
        {
            return this.baseRepositorys.Max<TType>(column);
        }

        public TType Max<TType>(Expression<Func<T, TType>> column, Expression<Func<T, bool>> @where)
        {
            return this.baseRepositorys.Max<TType>(column, @where);
        }

        public TType Min<TType>(Expression<Func<T, TType>> column)
        {
            return this.baseRepositorys.Min<TType>(column);
        }

        public TType Min<TType>(Expression<Func<T, TType>> column, Expression<Func<T, bool>> @where)
        {
            return this.baseRepositorys.Min<TType>(column, @where);
        }

        public TType Sum<TType>(Expression<Func<T, TType>> selector, Expression<Func<T, bool>> @where) where TType : new()
        {
            return this.baseRepositorys.Sum<TType>(selector, @where);
        }

        public void Dispose()
        {
            this.baseRepositorys.Dispose();
        }
    }
}
