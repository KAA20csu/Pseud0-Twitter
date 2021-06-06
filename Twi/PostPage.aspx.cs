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
    public partial class PostPage : System.Web.UI.Page
    {
        private SqlConnection Connection = null;
        protected async void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);
            await Connection.OpenAsync();

            HttpCookie Post_Id = Request.Cookies["PostID"];
            SqlCommand GetCurrentPost = new SqlCommand("SELECT Users.Avatar, Users.Login, UserPosts.Id, UserPosts.Message " +
                "FROM Users JOIN UserPosts ON Users.Id = UserPosts.User_Id", Connection);
            SqlCommand GetComments = new SqlCommand("SELECT UserPosts.Id, CommentTable.Msg_Id, CommentTable.Text, CommentTable.Name FROM UserPosts " +
                "JOIN CommentTable ON UserPosts.Id=CommentTable.Msg_Id", Connection);
            Dictionary<string, string> CommentList = new Dictionary<string, string>();
            SqlDataReader CommentReader = await GetComments.ExecuteReaderAsync();
            while(await CommentReader.ReadAsync())
            {
                if(Post_Id.Value  == CommentReader["Msg_Id"].ToString())
                {
                    CommentList.Add(CommentReader["Name"].ToString(), CommentReader["Text"].ToString());
                }
            }
            try { }
            catch { }
            finally
            {
                if (CommentReader != null)
                    CommentReader.Close();
            }
            foreach(var cmnt in CommentList)
            {
                var div = new HtmlGenericControl("div");
                var user = new HtmlGenericControl("div");

                var name = Post.GetName(cmnt.Key);
                name.Style.Add("position", "null");
                user.Style.Add("text-align", "initial");
                name.Click += Clickk;


                user.Controls.Add(name);

                Label lab = new Label
                {
                    Text = cmnt.Value,
                };
                
                lab.Style.Add("font-size", "15px");
                lab.Style.Add("font-family", "monospace");
                div.Style.Add("border-radius", "8px");
                div.Controls.Add(user);
                div.Controls.Add(lab);
                place.Controls.Add(div);
            }
            SqlDataReader ReadPost = await GetCurrentPost.ExecuteReaderAsync();
            while(await ReadPost.ReadAsync())
            {
                if(ReadPost["Id"].ToString() == Post_Id.Value)
                {
                    text.Text = ReadPost["Message"].ToString();
                    ava.ImageUrl = ReadPost["Avatar"].ToString();
                    nameUser.Text = ReadPost["Login"].ToString();
                }
            }
            try { }
            catch { }
            finally
            {
                if (ReadPost != null)
                    ReadPost.Close();
            }
        }
        private void Clickk(object sender, EventArgs e)
        {
            var name = sender as Button;
            HttpCookie cookieLog = new HttpCookie("redirProf", name.Text);
            Response.Cookies.Add(cookieLog);
            Response.Redirect("UserProfileToOther.aspx", false);
        }
        protected async void Bt_Click(object sender, EventArgs e)
        {
            HttpCookie Post_Id = Request.Cookies["PostID"];
            HttpCookie Login = Request.Cookies["Login"];

            SqlCommand InsertComment = new SqlCommand("INSERT INTO CommentTable VALUES(@Msg_Id, @Text, @Name)", Connection);
            if(!String.IsNullOrWhiteSpace(comm.Text))
            {
                InsertComment.Parameters.AddWithValue("Msg_Id", Post_Id.Value);
                InsertComment.Parameters.AddWithValue("Text", comm.Text);
                InsertComment.Parameters.AddWithValue("Name", Login.Value);
                await InsertComment.ExecuteNonQueryAsync();

            }
            Response.Redirect("PostPage.aspx", false);
        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            HttpCookie login = Request.Cookies["login"];
            login.Expires = DateTime.Now.AddDays(-1);

            Response.Cookies.Add(login);
            Session.Remove(login.ToString());
            Response.Redirect("HomePage.html", false);
        }

        protected void nameUser_Click(object sender, EventArgs e)
        {
            var name = sender as Button;
            HttpCookie cookieLog = new HttpCookie("redirProf", name.Text);
            Response.Cookies.Add(cookieLog);
            Response.Redirect("UserProfileToOther.aspx", false);
        }
    }
}