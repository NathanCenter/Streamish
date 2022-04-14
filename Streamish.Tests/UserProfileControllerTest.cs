using Microsoft.AspNetCore.Mvc;
using Streamish.Controllers;
using Streamish.Models;
using Streamish.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Streamish.Tests
{
    public class UserProfileControllerTest
    {
        [Fact]
        public void Get_Returns_All_UserProfiles()
        {
            var userCount = 12;
            var users = CreateUser(userCount);
            var repo = new InMemoryUserRepository(users);
            var controller = new UserProfileController(repo);

            var result = controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUsers = Assert.IsType<List<UserProfile>>(okResult.Value);
            Assert.Equal(userCount, actualUsers.Count);
            Assert.Equal(users, actualUsers);



        }

        [Fact]
        public void Get_By_Id_Returns_Users_With_Given_Id()
        {
            var testUserId = 99;
            var users = CreateUser(5);
            users[0].Id = testUserId;
            var repo = new InMemoryUserRepository(users);
            var controller = new UserProfileController(repo);

            var result = controller.Get(testUserId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUser = Assert.IsType<UserProfile>(okResult.Value);

            Assert.Equal(testUserId, actualUser.Id);
        }
        [Fact]
        public void Post_Method_Adds_A_New_User()
        {
            var userCount = 20;
            var users = CreateUser(userCount);
            var repo = new InMemoryUserRepository(users);
            var controller = new UserProfileController(repo);

            var newUser = new UserProfile()
            {
                Name = "new user",
                Email = "new email",
                ImageUrl = "https://image.shutterstock.com/image-vector/man-icon-vector-260nw-1040084344.jpg",
                DateCreated = DateTime.Now,
            };
            controller.Post(newUser);
            Assert.Equal(userCount + 1, repo.InternalData.Count);
        }
        [Fact]
        public void Put_Method_Updates_A_User()
        {
            var testUserId = 99;
            var users = CreateUser(5);
            users[0].Id = testUserId;

            var repo = new InMemoryUserRepository(users);
            var controller = new UserProfileController(repo);

            var userToUpdate = new UserProfile()
            {
                Id = testUserId,
                Name="Updated",
                Email="Updated",
                DateCreated = DateTime.Today,
                ImageUrl = "http://some.url"
            };
            controller.Put(testUserId, userToUpdate);
            var profileFromDB = repo.InternalData.FirstOrDefault(p => p.Id == testUserId);
            Assert.NotNull(profileFromDB);

            Assert.Equal(userToUpdate.Id, profileFromDB.Id);
            Assert.Equal(userToUpdate.Name, profileFromDB.Name);
            Assert.Equal(userToUpdate.Email, profileFromDB.Email);
            Assert.Equal(userToUpdate.DateCreated, profileFromDB.DateCreated);
            Assert.Equal(userToUpdate.ImageUrl, profileFromDB.ImageUrl);

        }

        [Fact]
        public void Delete_Method_Removes_A_UserProfile()
        {
            // Arrange
            var testUserId = 99;
            var users = CreateUser(5);
            users[0].Id = testUserId; // Make sure we know the Id of one of the videos

            var repo = new InMemoryUserRepository(users);
            var controller = new UserProfileController(repo);

            // Act
            controller.Delete(testUserId);

            // Assert
            var videoFromDb = repo.InternalData.FirstOrDefault(p => p.Id == testUserId);
            Assert.Null(videoFromDb);
        }
        private List<UserProfile> CreateUser(int count)
        {
            var users = new List<UserProfile>();
            for (var i = 1; i <= count; i++)
            {
                users.Add(new UserProfile()
                {
                    Id = i,
                    Name = $"Name {i}",
                    Email = $"Email {i}",
                    ImageUrl = $"https://image.shutterstock.com/image-vector/man-icon-vector-260nw-1040084344.jpg",

                });
            }
            return users;
        }

        private UserProfile CreateTestUserProfile(int id)
        {
            return new UserProfile()
            {
                Id = id,
                Name = $"User {id}",
                Email = $"user{id}@example.com",
                DateCreated = DateTime.Today.AddDays(-id),
                ImageUrl = $"http://user.url/{id}",
            };
        }
    }

    
}
