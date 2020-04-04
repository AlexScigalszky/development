using Example.Model.Model;

namespace Example.Model.Repositories.UserRepository
{
	public class UserTypeRepository : GenericRepository<UserType, UserType>, IUserTypeRepository
	{
		
		public UserTypeRepository(Context context, IMapper mapper) : base(context, mapper)
		{

		}

		protected override UserTypeDTO GetDTOFromEntity(UserType entity)
		{
			return mapper.Map<UserTypeDTO>(entity);
		}
	}
}
