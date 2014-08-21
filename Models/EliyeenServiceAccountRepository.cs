using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EliyeenWebServicesModule.Models
{
    public class EliyeenServiceAccountRepository : IDisposable
    {
        private EliyeenDBContext _ctx;

        private UserManager<AppUser> _userManager;

        public EliyeenServiceAccountRepository()
        {
            _ctx = new EliyeenDBContext();
            _userManager = new UserManager<AppUser>(new UserStore<AppUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            AppUser user = new AppUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            
            return result;
        }

        public async Task<AppUser> FindUser(string userName, string password)
        {
            AppUser user = await _userManager.FindAsync(userName, password);
            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }

}