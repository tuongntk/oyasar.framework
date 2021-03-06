﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OYASAR.Framework.Core.Interface;
using System.Threading.Tasks;

#if NET451
using System.Data.Entity;

// ReSharper disable once CheckNamespace
namespace OYASAR.Framework.EFNet
{
    public class EFRepository<TK> : IEFRepository<TK>
    {
        private readonly DbContext _dbContext;

        public EFRepository(TK dbContext)
        {
            this._dbContext = dbContext as DbContext;
        }

        public T Add<T>(T entity) where T : class
        {
            var dbSet = _dbContext.Set<T>();
            return dbSet.Add(entity);
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            var dbSet = _dbContext.Set<T>();
            return dbSet.AsQueryable();
        }

        public T GetByKey<T>(object key) where T : class
        {
            var dbSet = _dbContext.Set<T>();
            return dbSet.Find(key);
        }

        public T DeleteByKey<T>(object key) where T : class
        {
            var dbSet = _dbContext.Set<T>();
            return dbSet.Remove(GetByKey<T>(key));
        }

        public void Edit<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void LazyLoad<T, K>(T entity, Expression<Func<T, ICollection<K>>> expr) where T : class where K : class
        {
            _dbContext.Entry(entity).Collection(expr).Load();
        }

        public void LazyLoad<T, K>(T entity) where T : class where K : class
        {
            _dbContext.Entry(entity).Reference(typeof(K).Name).Load();
        }

        public void LazyLoad<T, K>(T entity, Expression<Func<T, IEnumerable<K>>> expr) where T : class where K : class
        {
            throw new Exception("EntityFramework is not supported Expr!");
        }

        public IQueryable<T> SqlQuery<T>(string str, params object[] obj) where T : class
        {
            return _dbContext.Database.SqlQuery<T>(str, obj).AsQueryable();
        }

        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> expr) where T : class
        {
            var dbSet = _dbContext.Set<T>().Where(expr);
            return dbSet.AsQueryable();
        } 

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IList<T>> GetAllAsync<T>() where T : class
        {
            var dbSet = _dbContext.Set<T>();
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetByKeyAsync<T>(object key) where T : class
        {
            var dbSet = _dbContext.Set<T>();
            return await dbSet.FindAsync(key);
        }

        public async Task<IList<T>> SqlQueryAsync<T>(string str, params object[] obj) where T : class
        {
            return await _dbContext.Database.SqlQuery<T>(str, obj).ToListAsync();
        }

        public async Task<IList<T>> GetAllAsync<T>(Expression<Func<T, bool>> expr) where T : class
        {
            var dbSet = _dbContext.Set<T>().Where(expr);
            return await dbSet.ToListAsync();
        }

        public async Task LazyLoadAsync<T, K>(T entity, Expression<Func<T, ICollection<K>>> expr)
            where T : class
            where K : class
        {
            await _dbContext.Entry(entity).Collection(expr).LoadAsync();
        }

        public Task LazyLoadAsync<T, K>(T entity, Expression<Func<T, IEnumerable<K>>> expr)
            where T : class
            where K : class
        {
            throw new Exception("EntityFramework is not supported Expr!");
        }

        public async Task LazyLoadAsync<T, K>(T entity)
            where T : class
            where K : class
        {
            await _dbContext.Entry(entity).Reference(typeof(K).Name).LoadAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<TEntity>> GetListAsync<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        {
            return await queryable.ToListAsync();
        }

        public async Task<TEntity> GetSingleOrDefaultAsync<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        {
            return await queryable.SingleOrDefaultAsync();
        }

        public async Task<TEntity> GetFirstOrDefaultAsync<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        {
            return await queryable.FirstOrDefaultAsync();
        }
    }
}

#endif
