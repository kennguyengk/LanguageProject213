using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Script.Serialization;
using System.Web.Security;


namespace LanguageProject
{
    public class IdentityConfig
    {
    }

    public class ApplicationUserManager : UserManager<Models.User>
    {

        public ApplicationUserManager(IUserStore<Models.User> store) : base(store)
        {

        }
       
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var store = new UserStore<Models.User>(context.Get<DAL.DataContext>());
            var manager = new ApplicationUserManager(store);

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            manager.UserLockoutEnabledByDefault = false;
            return manager;


        }

    }

    public class ApplicationRoleManager : RoleManager<IdentityRole> {

        public ApplicationRoleManager(DAL.DataContext context) : base(new RoleStore<IdentityRole>(context)) {

        }

        public List<string>MyRoles{

            get {

                return new List<string> { "Admin", "Student", "Teacher" };
            }

        }

        public void CreateRoles() {

            foreach (string role in MyRoles) {

                var role_exist = this.RoleExists(role);

                if (!role_exist) {
                    this.Create(new IdentityRole(role));
                }
            }
           

        }

    }

    public class ApplicationSignInManager : SignInManager<Models.User, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(Models.User user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

       

    }
    public class CustomIdentity
    {
        interface ICustomPrincipal : IPrincipal
        {
            string Id { get; set; }
            string FirstName { get; set; }
            string AvatarPath { get; set; }
        }
        public class CustomPrincipalSerializeModel
        {
            public string Id { get; set; }
            public string FName { get; set; }
            public string AvatarPath { get; set; }

            public IList<string> Roles { get; set; }
        }

        public class CustomPrincipal : ICustomPrincipal
        {
            public IIdentity Identity { get; private set; }
            public bool IsInRole(string role) {

                return Roles.Contains(role) ? true : false; 

            }

            public CustomPrincipal(string email)
            {
                this.Identity = new GenericIdentity(email);
                
            }

            public string Id { get; set; }
            public string FirstName { get; set; }
            public IList<string> Roles { get; set; }
            public string AvatarPath { get; set; }
        }

        public static CustomPrincipal CookieToIdentity(HttpCookie authCookie) {

            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);

            CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
            newUser.Id = serializeModel.Id;
            newUser.FirstName = serializeModel.FName;
            newUser.Roles = serializeModel.Roles;
            newUser.AvatarPath = serializeModel.AvatarPath;

            return newUser;

        }

        public static HttpCookie Create(Models.User user,IList<string> roles)
        {

            // var user = userRepository.Users.Where(u => u.Email == viewModel.Email).First();

            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.Id = user.Id;
            serializeModel.FName = user.FName;
            serializeModel.AvatarPath = user.AvatarPath;
            serializeModel.Roles = roles;
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     user.Email,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(15),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

            return faCookie;



        }


    }
   
    

}