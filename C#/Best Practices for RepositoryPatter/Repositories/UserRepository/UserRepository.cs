using Example.Model.Model;

namespace Example.Model.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User, User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {

        }
    }
}
