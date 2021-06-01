using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Twi
{
    public partial class UserProfileToOther : System.Web.UI.Page
    {
        private SqlConnection Connection { get; set; } = null;

        protected async void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);
            await Connection.OpenAsync();

            HttpCookie redirCookie = Request.Cookies["redirProf"];
            SqlCommand GetUserPosts = new SqlCommand("SELECT UserPosts.User_Id, UserPosts.Id, UserPosts.Message, Users.Id, Users.Login " +
                "FROM UserPosts JOIN Users ON UserPosts.User_id=Users.Id", Connection);
            Dictionary<string,string> PostList = new Dictionary<string, string>();
            SqlDataReader ReadUserPosts = await GetUserPosts.ExecuteReaderAsync();
            while(await ReadUserPosts.ReadAsync())
            {
                if(redirCookie.Value == ReadUserPosts["Login"].ToString())
                {
                    PostList.Add(ReadUserPosts["Id"].ToString(), ReadUserPosts["Message"].ToString());
                }
            }
            try { }
            catch { }
            finally
            {
                if(ReadUserPosts != null)
                {
                    ReadUserPosts.Close();
                }
            }
            foreach(var c in PostList)
            {
                var div = new HtmlGenericControl("div");

                Label post = new Label
                {
                    Text = c.Value
                };
                Button btn = new Button
                {
                    Text = "Обсудить",
                    ID=c.Key
                };
                btn.Click += Click;
                post.Style.Add("font-size", "20px");
                post.Style.Add("font-family", "monospace");
                div.Controls.Add(post);
                div.Controls.Add(btn);
                Posts.Controls.Add(div);
            }
        }
        private void Click(object sender, EventArgs e)
        {
            var c = sender as Button;
            HttpCookie crntPost = new HttpCookie("PostID", c.ID);
            Response.Cookies.Add(crntPost);
            Response.Redirect("PostPage.aspx",false);
        }
    }
}