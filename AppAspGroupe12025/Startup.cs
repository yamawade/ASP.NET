using AppAspGroupe12025.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppAspGroupe12025.Startup))]
namespace AppAspGroupe12025
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "momo";
                user.Email = "msarr091@gmail.com";
                string userPwd = "P@sser";
                var chkUser= UserManager.Create(user,userPwd);
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }

                if(!roleManager.RoleExists("Manager"))
                {
                    var roles = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    roles.Name = "Manager";
                    roleManager.Create(roles);

                }

                if(!roleManager.RoleExists("Employee"))
                {
                    var rol = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    rol.Name = "Employee";
                    roleManager.Create(rol);
                }


            }
    }
}
