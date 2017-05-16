using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppSecretManager.Core.Database
{
     interface IRepository<T>
    {
        bool Insert(T entity);
        void Delete(T entity);
        void Delete(string secret);
        bool Update(T entity);
        T Find(T entity);
        T Find(Expression<Func<T, bool>> predicate);
        IList<T> FindSevral(Expression<Func<T, bool>> predicate);
        IList<T> GetAll();
        T GetBySecret(string secret);
        bool ValidateSecret(string secret);
    }
}