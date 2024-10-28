using APPDATA.MyDbContext;
using Microsoft.EntityFrameworkCore;

namespace APPAPI.Services
{
    public class CRUDapi<T> where T : class
    {
        Context _context;
        DbSet<T> _dbSet;
        public CRUDapi(Context context, DbSet<T> dbset)
        {
           _context = context;
            _dbSet = dbset;
        }
        public bool CreateItem(T item)
        {
            try
            {
                _dbSet.Add(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteItem(T item)
        {
            try
            {
                _dbSet.Remove(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public IEnumerable<T> GetAllItems()
        {
            try
            {
                return _dbSet.ToList();

            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool UpdateItem(T item)
        {
            try
            {
                _dbSet.Update(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
