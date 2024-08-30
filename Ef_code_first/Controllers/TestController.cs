using Ef_code_first.Models;
using Ef_code_first.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ef_code_first.Controllers
{
    [Route("test/")]
    public class TestController : Controller
    {
        
        public readonly Curso_EF_Context _EF_Context;
        public readonly IUserRepository _userRepository;
        
        public TestController (IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }

        [HttpPost("test")]
        public async Task Test()
        {
            User user1 = new User()
            {
                Email = $"{Guid.NewGuid()}@gmail.com",
                Name = "userName",
                WorkingExperience = new List<WorkingExperience>()
                {
                    new WorkingExperience()
                    {
                        Name = "experience 1",
                        Details = "detail 1",
                        Environment = "environment 1"
                    },
                    new WorkingExperience()
                    {
                        Name = "experience 2",
                        Details = "detail 2",
                        Environment = "environment 2"
                    }
                }
            };

            await _userRepository.Insert(user1);
            /*await _EF_Context.Users.AddAsync(user1);
            await _EF_Context.SaveChangesAsync();*/
        }


        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("user/{userId}")]
        public async Task<User?> Get_user(int userId)
        {
            return await _EF_Context.Users.FirstOrDefaultAsync(a=> a.Id == userId); 
        }

        [HttpGet("user_we/{userId}")]
        public async Task<User?> Get_User_and_w_e(int userId)
        {
            /*return await _EF_Context.Users.Include(a => a.WorkingExperience).FirstOrDefaultAsync(a => a.Id == userId);*/

            return await _userRepository.GetUser(userId);
        }
    }
}
