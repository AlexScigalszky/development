using Example.Model.Model;

namespace Example.Model.Repositories.UserRepository
{
	public class UserRepository : GenericRepository<User, User>, IUserRepository
	{
		public UserRepository(Context context) : base(context)
		{

			protected override UserDTO GetDTOFromEntity(User entity)
			{
				if (entity == null)
				return null;
				var dto = mapper.Map<UserDTO>(entity);
				dto.TypeName = dto.Type?.Description;
				return dto;
			}
		}
	}
}
