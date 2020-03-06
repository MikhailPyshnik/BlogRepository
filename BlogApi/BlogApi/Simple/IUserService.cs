using Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Api.Simple_API_for_Authentication
{
    public interface IUserService
    {
        //  User Authenticate(string username, string password);

        //  //IEnumerable<User> GetAll();
         Task <User> GetById(string name);
        //  User Create(User user, string password);

        ////  void Update(User user, string password = null);
        //  void Delete(int id);

        Task<User> Authenticate(string username, string password);

        //IEnumerable<User> GetAll();
        //Task<User> GetById(int id);
        
        Task<User> Create(User user, string password);

        //  void Update(User user, string password = null);
        Task Delete(string email);
    }
}
