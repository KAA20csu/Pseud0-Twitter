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
            HttpCookie mail = Request.Cookies["mail"];
            HttpCookie sex = Request.Cookies["sex"];
            if (login != null)
            {
                AuthorizedLogName.Text = login.Value;
                //Mail.Text = mail.Value;
                //Sex.Text = sex.Value;
            }
        }
    }
}