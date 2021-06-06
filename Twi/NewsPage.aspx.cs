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
using Twi.Models;

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

            SqlCommand GetAllPosts = new SqlCommand("SELECT UserPosts.Id, Users.Avatar, UserPosts.User_Id, UserPosts.Message, Users.Login " +
                "FROM UserPosts JOIN Users ON UserPosts.User_id=Users.Id", Connection);
            Stack<UserWithPost> AllPostsList = new Stack<UserWithPost>();
            SqlDataReader ReadAllPosts = await GetAllPosts.ExecuteReaderAsync();
            while(await ReadAllPosts.ReadAsync())
            {
                AllPostsList.Push(new UserWithPost(ReadAllPosts["Login"].ToString(), ReadAllPosts["Message"].ToString(), ReadAllPosts["Avatar"].ToString(), ReadAllPosts["Id"].ToString()));
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
                var function = new HtmlGenericControl("div");
                var osnova = new HtmlGenericControl("div");
                
                var name = Post.GetName(user.Name);
                name.Click += Clickk;

                var commBt = Post.GetBtComm(user.PostId);
                commBt.Click += CommClick;

                function.Controls.Add(commBt);
                osnova.Controls.Add(Post.GetInfoUser(user.AvaUrl, name));
                osnova.Controls.Add(Post.GetContent(user.Text));
                osnova.Controls.Add(function);
                NewsBox.Controls.Add(osnova);
            }
        }

        private void CommClick(object sender, EventArgs e)
        {
            var c = sender as Button;
            HttpCookie crntPost = new HttpCookie("PostID", c.ID);
            Response.Cookies.Add(crntPost);
            Response.Redirect("PostPage.aspx", false);
        }

        private void Clickk(object sender, EventArgs e)
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
    public class UserWithPost
    {
        public string Name { get; }
        public string Text { get; }
        public string AvaUrl { get; }
        public string PostId { get; }
        public UserWithPost(string name, string text, string avaUrl, string postId)
        {
            Name = name;
            Text = text;
            AvaUrl = avaUrl;
            PostId = postId;
        }
    }
}