using Ef_code_first.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mysqlx.Crud;

namespace Ef_code_first.Repositories
{

    public interface IUserRepository
    {
        Task<User> Insert(User user);
        Task<User> Update(User user);
         Task<User?> Delete(User user);
        Task<User?> GetUser(int id);
        Task<User?> GetUserOnly(int id);
        Task<List<User>> GetAll();

    }


    public class UserRepository : IUserRepository
    {
        private readonly Curso_EF_Context _context;


        public UserRepository(Curso_EF_Context context)
        {
            _context = context;
        }

        public async Task<User> Insert(User user)
        {
            EntityEntry<User> insertedUser = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return insertedUser.Entity;
        }

        public async Task<User?> GetUser(int id)
        {
            return await _context.Users.Include(a => a.WorkingExperience).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserOnly(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }


        //only for writing the example. Not used
        public async Task<List<User>> EagerLoading()
        {
            //Direct query in database. Takes all at once
            return await _context.Users.Include(u => u.WorkingExperience).Where(uid => uid.Id > 0).ToListAsync();
        }

        //only for writing the example. Not used
        public async Task LazyLoading()
        {
            //First takes the main entity and then goes for the rest of the values in other entities
            //Usually much slower
            //It is only recommended if the database cannot handle big amount of data at once
            User? _user = await _context.Users.Where(u => u.Id > 0).FirstOrDefaultAsync();
            if (_user != null)
            {
                var experiencies = _user.WorkingExperience;
                Console.WriteLine(experiencies.Count.ToString());
            }
        }

        //only for writing the example. Not used
        public async Task ExplicitLoading()
        {
            //Forcing the exact data to be retrieve
            //Could work even if the lazyLoading feature is not activated
            User? _user = await _context.Users.Where(u => u.Id > 0).FirstOrDefaultAsync();
            if (_user != null)
            {
                await _context.Entry(_user).Collection(u => u.WorkingExperience).LoadAsync();
                Console.WriteLine(_user.WorkingExperience.Count.ToString());
            }
        }

        public async Task<User> Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> Delete(User user)
        {
            var user_temp = await _context.Users.FindAsync(user);

            if (user_temp != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }
            return null;


        }
    }
}
