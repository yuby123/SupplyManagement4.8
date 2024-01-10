using System;
using System.Collections.Generic;

namespace SupplyManagement.Contracts
{
    public interface IGeneralRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetByGuid(Guid guid); // Tanda tanya dihilangkan untuk kembali ke semantik pra-C# 8.0

        TEntity Create(TEntity entity); // Tanda tanya dihilangkan

        bool Update(TEntity entity);

        bool Delete(TEntity entity);
    }
}
