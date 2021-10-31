using Model.InMemoryDataAccess;
using Operations;

namespace Model
{
    public class UserLocationQuery: IQuery<Location>
    {
        private readonly UserLocationReader userLocationReader;

        public UserLocationQuery(UserLocationReader userLocationReader)
        {
            this.userLocationReader = userLocationReader;
        }

        public Location Run()
        {
            userLocationReader.UserId = UserId;
            return userLocationReader.Read();
        }

        public string UserId { get; set; }
    }
}
