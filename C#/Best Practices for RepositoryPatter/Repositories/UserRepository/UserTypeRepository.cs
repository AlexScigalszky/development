using Example.Model.Model;

namespace Example.Model.Repositories.UserRepository
{
    public class UserTypeRepository : GenericRepository<UserType, UserType>, IUserTypeRepository
    {
        public UserTypeRepository(Context context): base(context)
        {

        }
    }
}
