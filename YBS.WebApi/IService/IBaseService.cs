using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// 插入 - 通过实体对象添加
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSave">是否执行</param>
        /// /// <returns></returns>
        T Add(T entity, bool isSave = true);
        Task<T> AysAdd(T entity, bool isSave = true);
        /// <summary>
        /// 批量插入 - 通过实体对象集合添加
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        /// <param name="isSave">是否执行</param>
        void AddRange(IEnumerable<T> entitys, bool isSave = true);

        /// <summary>
        /// 删除 - 通过实体对象删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSave">是否执行</param>
        void Delete(T entity, bool isSave = true);

        /// <summary>
        /// 批量删除 - 通过实体对象集合删除
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        /// <param name="isSave">是否执行</param>
        void Delete(bool isSave = false, params T[] entitys);

        /// <summary>
        /// 删除 - 通过主键ID删除
        /// </summary>
        /// <param name="id">主键ID</param>
        Task<int> AsyDelete(object id);
        int Delete(object id);
        /// <summary>
        /// 批量删除 - 通过条件删除
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <param name="isSave">是否执行</param>
        void Delete(Expression<Func<T, bool>> @where, bool isSave = true);

        /// <summary>
        /// 修改 - 通过实体对象修改
        /// </summary>
        /// <param name="entity">实体对象</param>
        Task<int> AysUpdate(T entity);

        int Update(T entity);
        /// <summary>
        /// 批量修改 - 通过实体对象集合修改
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        void Update(params T[] entitys);

        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 返回总条数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 返回总条数 - 通过条件过滤
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 返回第一条记录
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 返回第一条记录 - 通过条件过滤
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="isDesc">排序方式</param>
        /// <returns></returns>
        T FirstOrDefault<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false);

        /// <summary>
        /// 去重查询
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        IQueryable<T> Distinct(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        IQueryable<T> Where(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 条件查询 - 支持排序
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="isDesc">排序方式</param>
        /// <returns></returns>
        IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false);

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <returns></returns>
        IEnumerable<T> Where<TOrder>(Func<T, bool> @where, Func<T, TOrder> order, int pageIndex, int pageSize, out int count, bool isDesc = false);

        /// <summary>
        /// 条件分页查询 - 支持排序 - 支持Select导航属性查询
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <returns></returns>
        IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, int pageIndex, int pageSize, out int count, bool isDesc = false);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// 获取所有数据 - 支持排序
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="order">排序条件</param>
        /// <param name="isDesc">排序方式</param>
        /// <returns></returns>
        IQueryable<T> GetAll<TOrder>(Expression<Func<T, TOrder>> order, bool isDesc = false);

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        T GetById<TType>(TType id);

        Task<T> AysGetById<TType>(TType id);
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <returns></returns>
        TType Max<TType>(Expression<Func<T, TType>> column);

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        TType Max<TType>(Expression<Func<T, TType>> column, Expression<Func<T, bool>> @where);

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <returns></returns>
        TType Min<TType>(Expression<Func<T, TType>> column);

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        TType Min<TType>(Expression<Func<T, TType>> column, Expression<Func<T, bool>> @where);

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="selector">字段条件</param>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        TType Sum<TType>(Expression<Func<T, TType>> selector, Expression<Func<T, bool>> @where) where TType : new();

        void Dispose();
    }
}
