using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Twi
{
    public partial class UserPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie login = Request.Cookies["login"];
            if (login != null)
            {
                AuthorizedLogName.Text = login.Value;
            }
            else
            {
                Response.Redirect("SignIn.aspx");
            }
        }

        protected void GoToChat(object sender, EventArgs e)
        {
            Response.Redirect("PostFolder/PostForm.aspx");
        }
        protected void LogOut_Click(object sender, EventArgs e)
        {
            HttpCookie login = new HttpCookie("login", string.Empty);
            login.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(login);
            Response.Redirect("HomePage.html");
        }
    }
}