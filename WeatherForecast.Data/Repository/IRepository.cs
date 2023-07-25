using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.DataStore.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<T> FindByCondition(Expression<Func<T, bool>> expression, bool loadchildren = false, IList<string>? children = null);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(int id);
    }
}
