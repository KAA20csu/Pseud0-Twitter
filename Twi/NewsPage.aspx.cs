using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Twi
{
    public partial class NewsPage : System.Web.UI.Page
    {
        private SqlConnection Connection { get; set; } = null;

        public HttpCookie Name;
        protected async void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);
            await Connection.OpenAsync();

            SqlCommand GetAllPosts = new SqlCommand("SELECT UserPosts.User_Id, UserPosts.Message, Users.Id, Users.Login " +
                "FROM UserPosts JOIN Users ON UserPosts.User_id=Users.Id", Connection);
            List<UserWithPost> AllPostsList = new List<UserWithPost>();
            SqlDataReader ReadAllPosts = await GetAllPosts.ExecuteReaderAsync();
            while(await ReadAllPosts.ReadAsync())
            {
                AllPostsList.Add(new UserWithPost(ReadAllPosts["Login"].ToString(), ReadAllPosts["Message"].ToString()));
            }
            try { }
            catch { }
            finally
            {
                if (ReadAllPosts != null)
                    ReadAllPosts.Close();
            }
            foreach(var user in AllPostsList)
            {
                var div = new HtmlGenericControl("div");

                Button name = new Button
                {
                    Text = user.Name + " "
                };
                name.Click += Clickk;
                Label post = new Label
                {
                    Text = user.Text + " "
                };
                name.Style.Add("font-size", "20px");
                name.Style.Add("font-family", "monospace");
                post.Style.Add("font-size", "20px");
                post.Style.Add("font-family", "monospace");
                div.Controls.Add(name);
                div.Controls.Add(post);
                NewsBox.Controls.Add(div);
            }
        }
        private void Clickk(object sender, EventArgs e)
        {
            var name = sender as Button;
            HttpCookie cookieLog = new HttpCookie("redirProf", name.Text);
            Response.Cookies.Add(cookieLog);
            Response.Redirect("UserProfileToOther.aspx", false);
        }
    }
    public class UserWithPost
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public UserWithPost(string name, string text)
        {
            Name = name;
            Text = text;
        }
    }
}