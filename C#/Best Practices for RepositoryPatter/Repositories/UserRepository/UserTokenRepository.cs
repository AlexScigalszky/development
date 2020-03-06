using Example.Model.Model;

namespace Example.Model.Repositories.UserRepository
{
    public class UserTokenRepository : GenericRepository<UserToken, UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(Context context) : base(context)
        {

        }
    }
}
