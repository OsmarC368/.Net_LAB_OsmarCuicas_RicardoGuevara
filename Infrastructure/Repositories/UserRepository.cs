using Core.Entities;                      
using Core.Interfaces.Repositories;        
using Infrastructure.Data;                 
using Microsoft.EntityFrameworkCore;       

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
		{

		}

		public async ValueTask<User> GetByEmail(string email)
		{
			return await dbSet.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
		}

		public virtual async ValueTask<User> Login(string email, string password)
		{
			var user = await dbSet
				.Where(u => u.Email.Equals(email))
				.FirstOrDefaultAsync();

			if (user == null)
				return null;

			bool passwordOk = BCrypt.Net.BCrypt.Verify(password, user.Password);

			if (!passwordOk)
				return null;

			return user;
		}
    }
}
