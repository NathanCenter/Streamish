using Streamish.Models;
using System.Collections.Generic;


namespace Streamish.Repositories
{
    public interface IUserProfileRepository
    {
        public List<UserProfile> GetAll();
        void Add(UserProfile user_Profile);
        void Delete(int id);

        UserProfile GetById(int id);
        void Update(UserProfile user_Profile);
    }
}
