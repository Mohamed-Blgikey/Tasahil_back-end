using System.Linq.Expressions;

namespace Tasahil_.net_core_6_.Interface
{
    public interface IDynamicRep<T> where T : class
    {
        IEnumerable<T> Get(string[] includes = null);
        IEnumerable<T> Get(Expression<Func<T, bool>> match, string[] includes = null);
        T GetById(int id);
        T GetById(Expression<Func<T, bool>> match, string[] includes = null);


        T Create(T item);
        T Edit(T item);
        T Delete(T item);
    }
}
