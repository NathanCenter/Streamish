using Streamish.Models;
using Streamish.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamish.Tests.Mocks
{
     class InMemoryUserRepository : IUserProfileRepository
    {
        private readonly List<UserProfile> _data;

        public List<UserProfile> InternalData
        {
            get
            {
                return _data;
            }
        }
        public InMemoryUserRepository(List<UserProfile> startingData)
        {
            _data = startingData;
        }

        public InMemoryUserRepository(object users)
        {
        }

        public void Add(UserProfile userProfile)
        {
            var lastUser = _data.Last();
            userProfile.Id = lastUser.Id + 1;
            _data.Add(userProfile);
        }
        public void Delete(int id)
        {
            var userToDelete = _data.FirstOrDefault(p => p.Id == id);
            if (userToDelete == null)
            {
                return;
            }

            _data.Remove(userToDelete);
        }
        public List<UserProfile> GetAll()
        {
            return _data;
        }

        public UserProfile GetById(int id)
        {
            return _data.FirstOrDefault(p => p.Id == id);
        }

        public void Update(UserProfile User_Profile)
        {
            var currentUser = _data.FirstOrDefault(p => p.Id == User_Profile.Id);
            if (currentUser == null)
            {
                return;
            }

            currentUser.Name = User_Profile.Name;
            currentUser.Email = User_Profile.Email;
            currentUser.ImageUrl = User_Profile.ImageUrl;
            currentUser.DateCreated = User_Profile.DateCreated;


        }
        public List<UserProfile> Search(string criterion, bool sortDescending)
        {
            throw new NotImplementedException();
        }
        public List<UserProfile> GetAllWithComments()
        {
            throw new NotImplementedException();
        }

        public UserProfile GetByIdandVideo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
