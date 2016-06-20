using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
namespace COMP2007_lesson4
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            // create new userStore and userManager objects
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);

            //Search for and create a new user object
            var user = userManager.Find(UserNameTextBox.Text, PasswordTextBox.Text);

            //if a match is found for the user
            if(user != null)
            {
                //authenticate and login the new user
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                //sign in the user
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);

                //redirect to mainmenu page
                Response.Redirect("~/Contoso/MainMenu.aspx");
            }
            else
            {
                //if user doesnt exist, throw an error to the alertflashdiv
                StatusLabel.Text = "Invalid Username or Password";
                AlertFlash.Visible = true;
            }
        }
    }
}