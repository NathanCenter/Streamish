using Streamish.Models;
using System.Collections.Generic;

namespace Streamish.Repositories
{
    public interface IVideoRepository
    {
        void Add(Video video);
        void Delete(int id);
       
        Video GetById(int id);
        void Update(Video video);
        List<Video> GetAllWithComments();
         Video GetVideoByIdWithComments(int id);
        void Search();
    }
}