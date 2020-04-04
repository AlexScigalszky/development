using Example.Model.Model;

namespace Example.Model.Repositories.UserRepository
{
	public class UserTokenRepository : GenericRepository<UserToken, UserToken>, IUserTokenRepository
	{
        public UserTokenRepository(Context context, IMapper mapped) : base(context, mapped)
        {

        }

        protected override UserTokenDTO GetDTOFromEntity(UserToken entity)
        {
            return mapper.Map<UserTokenDTO>(entity);
        }
	}
}
