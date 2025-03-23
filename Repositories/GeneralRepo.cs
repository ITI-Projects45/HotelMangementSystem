using HotelMangementSystem.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelMangementSystem.Repositories
{
    public class GeneralRepo<T> : IGeneralRepo<T> where T : class
    {
        private readonly DatabaseContext context;

        public GeneralRepo(DatabaseContext context)
        {
            this.context = context;
        }
        public T GetById(int id)
        {
            T item = context.Set<T>().Find(id);
            if (item != null)
            {
                context.Entry(item).State = EntityState.Detached;
                return item;
            }
            return null;
        }
        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
        public void Insert(T item)
        {
            context.Add(item);
        }
        public void Update(T item)
        {
            context.Update<T>(item);
        }
        public bool Delete(int id)
        {
            T item = GetById(id);
            if (item != null)
            {
                context.Remove(item);
                return true;
            }
            return false;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
