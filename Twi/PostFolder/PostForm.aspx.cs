using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Twi.PostFolder
{
    public partial class PostForm : System.Web.UI.Page
    {
        private SqlConnection Connection = null;
        protected async void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["PosctCN"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);
            await Connection.OpenAsync();
        }

        protected async void SendPostClick(object sender, EventArgs e)
        {
            HttpCookie login = Request.Cookies["login"];
            SqlCommand SendPost = new SqlCommand("INSERT INTO [Posts] VALUES(@Name, @Post)", Connection);
            SendPost.Parameters.AddWithValue("Name", login.Value);
            SendPost.Parameters.AddWithValue("Post", PostBox.Text);
            await SendPost.ExecuteNonQueryAsync();
            Response.Redirect("NewsStream.aspx");
        }
    }
}