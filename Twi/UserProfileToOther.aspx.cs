using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Twi.Models;

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
           
            SqlCommand GetUserPosts = new SqlCommand("SELECT UserPosts.User_Id, UserPosts.Id, UserPosts.Message, Users.Id, Users.Avatar, Users.Login " +
                "FROM UserPosts JOIN Users ON UserPosts.User_id=Users.Id", Connection);
            Dictionary<string,string> PostList = new Dictionary<string, string>();
            SqlDataReader ReadUserPosts = await GetUserPosts.ExecuteReaderAsync();
            while (await ReadUserPosts.ReadAsync())
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


            UserProfile(redirCookie);



            foreach (var c in PostList)
            {

                var function = new HtmlGenericControl("div");
                var osnova = new HtmlGenericControl("div");

                var name = Post.GetName(redirCookie.Value);
                name.Click += NameClick;

                var commBt = Post.GetBtComm(c.Key);
                commBt.Click += CommClick;

                function.Controls.Add(commBt);
                osnova.Controls.Add(Post.GetInfoUser(face.ImageUrl, name));
                osnova.Controls.Add(Post.GetContent(c.Value));
                osnova.Controls.Add(function);

                Posts.Controls.Add(osnova);
            }
        }
        protected async void UserProfile(HttpCookie redirCookie)
        {
            SqlCommand GetUser = new SqlCommand("Select Users.Login, Users.Avatar from Users", Connection);

            SqlDataReader ReadUsers = await GetUser.ExecuteReaderAsync();
            while (await ReadUsers.ReadAsync())
            {
                if (redirCookie.Value == ReadUsers["Login"].ToString())
                {
                    face.ImageUrl = ReadUsers["Avatar"].ToString();
                    AuthorizedLogName.Text = ReadUsers["Login"].ToString();
                }
            }
            try { }
            catch { }
            finally
            {
                if (ReadUsers != null)
                {
                    ReadUsers.Close();
                }
            }
        }
        private void CommClick(object sender, EventArgs e)
        {
            var c = sender as Button;
            HttpCookie crntPost = new HttpCookie("PostID", c.ID);
            Response.Cookies.Add(crntPost);
            Response.Redirect("PostPage.aspx",false);
        }
        private void NameClick(object sender, EventArgs e)
        {
            var name = sender as Button;
            HttpCookie cookieLog = new HttpCookie("redirProf", name.Text);
            Response.Cookies.Add(cookieLog);
            Response.Redirect("UserProfileToOther.aspx", false);
        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            HttpCookie login = Request.Cookies["login"];
            login.Expires = DateTime.Now.AddDays(-1);

            Response.Cookies.Add(login);
            Session.Remove(login.ToString());
            Response.Redirect("HomePage.html", false);
        }
    }
}