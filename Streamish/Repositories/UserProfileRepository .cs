using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Streamish.Models;
using Streamish.Utils;
using System.Collections.Generic;
using System.Data;

namespace Streamish.Repositories
{

    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }





        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select id,name,Email,ImageUrl,DateCreated 
                    from UserProfile";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var profile = new List<UserProfile>();
                        while (reader.Read())
                        {
                            profile.Add(new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),


                            });

                        }
                        return profile;
                    }
                }

            }

        }

        public void Add(UserProfile user_Profile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT into UserProfile(Name,Email,ImageUrl,DateCreated)
                    OUTPUT INSERTED.ID
                    Values(@name,@email,@imageurl,@datecreated)";

                    DbUtils.AddParameter(cmd, "@name", user_Profile.Name);
                    DbUtils.AddParameter(cmd, "@email", user_Profile.Email);
                    DbUtils.AddParameter(cmd, "@imageurl", user_Profile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@datecreated", user_Profile.DateCreated);
                    user_Profile.Id = (int)cmd.ExecuteScalar();

                }

            }
        }

        public void Update(UserProfile user_Profile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE UserProfile Set Name=@Name,
                    Email=@Email,
                    ImageUrl=@ImageUrl,
                    DateCreated=@DateCreated
                    WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Name", user_Profile.Name);
                    DbUtils.AddParameter(cmd, "@Email", user_Profile.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", user_Profile.DateCreated);
                    DbUtils.AddParameter(cmd, "@Id", user_Profile.Id);

                    cmd.ExecuteNonQuery();
                }

            }
        }
        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }

            }
        }
        public UserProfile GetByIdandVideo(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select up.name,up.Email,up.ImageURl, v.id as VideoId ,v.Title,v.Description from UserProfile up left join Video v
                    on v.UserProfileId =up.Id
                     where up.id=@Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfile userProfile = null;
                       
                        if (reader.Read())
                        {
                            userProfile = new UserProfile()
                            {
                                Id = id,
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),

                                Videos = new List<Video>()
                            };
                            if (DbUtils.IsNotDbNull(reader, "VideoId"))
                            {
                                while (reader.Read())
                                {
                                    userProfile.Videos.Add(new Video
                                    {
                                        Id = reader.GetInt32("VideoId"),
                                        Title = DbUtils.GetString(reader, "Title"),
                                        Description = DbUtils.GetString(reader, "Description"),

                                    });
                                }
                                    
                            }


                        }
                        return userProfile;
                    }
                }
            }


        }
    }
}
