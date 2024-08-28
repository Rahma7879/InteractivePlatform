
using InteractivePlatform.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace InteractivePlatform.BL.Repository
{
    public class GenericRepo<TEntity> where TEntity : class
    {
        InteractivePlatformContext db;

        public GenericRepo(InteractivePlatformContext db)
        {
            this.db = db;
        }

        public List<TEntity> selectall()
        {
            return db.Set<TEntity>().ToList();
        }

        public TEntity selectbyid(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public  void add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);

        }

        public   void update(TEntity entity)
        {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public  void delete(int id)
        {
            TEntity obj = db.Set<TEntity>().Find(id);
            db.Set<TEntity>().Remove(obj);
        }
        public User FindEmail(string email)
        {
            return db.Users.FirstOrDefault(u => u.Email == email);
        }
        

        public IEnumerable<TEntity> GetPaged(int page, int pageSize)
        {
            return db.Set<TEntity>()
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .ToList();
        }

        public int Count()
        {
            return db.Set<TEntity>().Count();
        }


    }
}
