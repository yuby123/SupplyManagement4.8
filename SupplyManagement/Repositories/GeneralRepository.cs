using SupplyManagement.Contracts;
using SupplyManagement.Utilities.Handler;
using SupplyManagement.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SupplyManagement.Repositories
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class
    {
        protected readonly SMDbContext _context;

        public GeneralRepository(SMDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity GetByGuid(Guid guid)
        {
            var entity = _context.Set<TEntity>().Find(guid);
            return entity;
        }

        public TEntity Create(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_tb_m_employees_email"))
                {
                    throw new ExceptionHandler("Email sudah ada");
                }
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_tb_m_employees_phone_number"))
                {
                    throw new ExceptionHandler("Nomor telepon sudah ada");
                }
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
