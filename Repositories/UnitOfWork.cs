

namespace App.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
        //public Task<int> SaveChangesAsync()=> context.SaveChangesAsync();
    }
}
