using LivegramBot.DataAccess;
using LivegramBot.Entities;
using Microsoft.EntityFrameworkCore;

namespace LivegramBot.Services
{
    public class UserService
    {
        public int AddUserAsync(User user)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var chechChatId = context.User.FirstOrDefault(x => x.ChatId == user.ChatId);

                if (chechChatId == null)
                {
                    context.User.AddAsync(user);
                
                    int response = context.SaveChanges();

                    return response;
                }

                return 0;

            }
        }

        public IEnumerable<User> GetAllAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var users = context.User.AsNoTracking().ToList();

                return users;
            }
        }

        public User FindUser(long chatId)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                var user =  context.User.FirstOrDefault(x => x.ChatId == chatId);

                return user;
            }
        }
    }
}