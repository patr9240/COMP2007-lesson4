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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            //Redirect back to the default page
            Response.Redirect("~/Default.aspx");
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            // create new userStore and userManager objects
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);

            //create a new user object
            var user = new IdentityUser()
            {
                UserName = UserNameTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text,
                Email = EmailTextBox.Text
             };
            //create new user on db and store the result
            IdentityResult result = userManager.Create(user, PasswordTextBox.Text);

            //check if successfully registered
            if (result.Succeeded)
            {
                //authenticate and login the new user
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                //sign in
                authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                //redirect to mainmenu page
                Response.Redirect("~/Contoso/MainMenu.aspx");
            }
            else
            {
                //display error in the alertFlashDiv
                StatusLabel.Text = result.Errors.FirstOrDefault();
                AlertFlash.Visible = true;
            }

        }
    }
}