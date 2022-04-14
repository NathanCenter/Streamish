using Microsoft.Extensions.Configuration;
using Streamish.Models;
using System.Collections.Generic;

namespace Streamish.Repositories
{

    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public void Add(UserProfile user_Profile)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<UserProfile> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public UserProfile GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(UserProfile user_Profile)
        {
            throw new System.NotImplementedException();
        }
    }
}
