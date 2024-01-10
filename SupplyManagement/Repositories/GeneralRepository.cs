using SupplyManagement.Contracts;
using SupplyManagement.Utilities.Handler;
using SupplyManagement.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity; // Ganti System.Data.Entity karena menggunakan EF 6
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
            // EF 6 menggunakan ToList() secara langsung tanpa AsNoTracking()
            return _context.Set<TEntity>().ToList();
        }

        public TEntity GetByGuid(Guid guid)
        {
            // Find() tetap digunakan untuk mencari entitas berdasarkan kunci
            var entity = _context.Set<TEntity>().Find(guid);
            // Clear() tidak ada di EF 6, bisa digunakan AsNoTracking() atau reset context jika perlu
            // _context.Entry(entity).State = EntityState.Detached;
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
                // Exception handling harus disesuaikan dengan cara EF 6 menangani exceptions
                // is not null diganti dengan != null untuk kompatibilitas dengan C# versi sebelumnya
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
                _context.Entry(entity).State = EntityState.Modified; // Cara update di EF 6
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception seperti di Create()
                throw; // Ulangi pengecualian atau tangani sesuai dengan logika bisnis Anda
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
                // Anda bisa menambahkan logika penanganan kesalahan yang lebih baik di sini
                return false;
            }
        }
    }
}
