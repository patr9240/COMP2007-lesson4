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
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //store session info into authentication manager
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            //end session
            authenticationManager.SignOut();
            //redirect to landing page
            Response.Redirect("~/Login.aspx");
        }
    }
}