using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pseudo_Twitter
{
    public partial class User_PersonalPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie login = Request.Cookies["login"];
            HttpCookie sign = Request.Cookies["sign"];

            if(login != null && sign != null)
            {
                if(sign.Value == GetSign.Sign(login.Value + "bytepp"))
                {
                    UserLogin.Text = login.Value;
                    return;
                }
            }
            Response.Redirect("Log In.aspx");
        }
    }
}