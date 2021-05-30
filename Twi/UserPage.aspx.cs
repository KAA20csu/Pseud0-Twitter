using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Twi
{
    public partial class UserPage : System.Web.UI.Page
    {
        private SqlConnection Connection { get; set; } = null;
        
        protected async void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);
            await Connection.OpenAsync();
            HttpCookie login = Request.Cookies["login"];

            string img = null;
            
            SqlCommand GetImage = new SqlCommand("SELECT [Login], [Avatar] FROM [Users]", Connection);
            SqlDataReader Reader = null;
            try
            {
                Reader = await GetImage.ExecuteReaderAsync();
                while (await Reader.ReadAsync())
                {
                    if(login.Value == Reader["Login"].ToString())
                    {
                        img = Reader["Avatar"].ToString();
                    }
                }
            }
            catch { }
            finally
            {
                if (Reader != null)
                    Reader.Close();
            }
            if (login != null)
            {
                AuthorizedLogName.Text = login.Value;
                face.ImageUrl = img;
                UpdateMessages();
            }
            else
            {
                Response.Redirect("SignIn.aspx");
            }
            
        }
        private int IdValue { get; set; }
        private string LoginValue { get; set; }
        private async void UpdateMessages()
        {
            HttpCookie login = Request.Cookies["login"];
            if (PostBox.Text != null)
            {
                SqlCommand takeId = new SqlCommand("SELECT [Id], [Login] FROM [Users]", Connection);
                SqlDataReader reader = await takeId.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    if (login.Value == reader["Login"].ToString())
                        IdValue = int.Parse(reader["Id"].ToString());
                }
                try
                { }
                catch { }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }
                SqlCommand GetPosts = new SqlCommand("SELECT Users.Login, UserPosts.Message FROM Users JOIN UserPosts ON Users.Id = UserPosts.User_Id", Connection);
                SqlDataReader PostReader = await GetPosts.ExecuteReaderAsync();
                List<string> Posts = new List<string>();
                while (await PostReader.ReadAsync())
                {
                    if (login.Value == PostReader["Login"].ToString())
                        Posts.Add(PostReader["Message"].ToString());
                }
                try
                { }
                catch { }
                finally
                {
                    if (PostReader != null)
                        PostReader.Close();
                }
                //await GetPosts.ExecuteNonQueryAsync();
                foreach(string uPost in Posts)
                {
                    PostLabel.Text += uPost;
                }

            }
        }
        protected async void GoToChat(object sender, EventArgs e)
        {
            HttpCookie login = Request.Cookies["login"];
            if(PostBox.Text != null)
            {
                SqlCommand takeId = new SqlCommand("SELECT [Id], [Login] FROM [Users]", Connection);
                SqlDataReader reader = await takeId.ExecuteReaderAsync();
                while(await reader.ReadAsync())
                {
                    if (login.Value == reader["Login"].ToString())
                        IdValue = int.Parse(reader["Id"].ToString());
                }
                try
                {}
                catch{}
                finally
                {
                    if (reader != null)
                        reader.Close();
                }
                SqlCommand insertPost = new SqlCommand("INSERT INTO [UserPosts] VALUES(@User_Id, @Message)", Connection);
                insertPost.Parameters.AddWithValue("User_Id", IdValue);
                insertPost.Parameters.AddWithValue("Message", PostBox.Text);
                await insertPost.ExecuteNonQueryAsync();

            }
        }
        protected void Upload_Click(object sender, EventArgs e)
        {
            if (newAva.HasFile)
            {
                string trailingPath = newAva.FileName;
                string fullPath = Path.Combine(Server.MapPath("~/avatars"), trailingPath ?? throw new InvalidOperationException());

                newAva.SaveAs(fullPath);

                face.ImageUrl = "avatars/" + newAva.FileName;

                ChangeFildAva("avatars/" + newAva.FileName);
            }
        }
        private async void ChangeFildAva(string path)
        {
            HttpCookie login = Request.Cookies["login"];
            SqlCommand GetImage = new SqlCommand("SELECT [Login], [Avatar] FROM [Users]", Connection);
            SqlCommand com = new SqlCommand("UPDATE [Users] SET [Avatar] = @path WHERE [Login] = @login", Connection);
            List<IPerson> us = new List<IPerson>();
            SqlDataReader Reader = null;
            try
            {
                Reader = await GetImage.ExecuteReaderAsync();
                while (await Reader.ReadAsync())
                {
                    us.Add(new IPerson(Reader["Login"].ToString(), PostBox.Text));
                }
            }
            catch { }
            finally
            {
                if (Reader != null)
                    Reader.Close();
            }
            foreach(var US in us)
            {
                if (login.Value == US.Login)
                {
                    com.Parameters.AddWithValue("login", login.Value);
                    com.Parameters.AddWithValue("path", path);
                    await com.ExecuteNonQueryAsync();
                }
            }
            
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            HttpCookie login = Request.Cookies["login"];
            login.Expires = DateTime.Now.AddDays(-1);

            Response.Cookies.Add(login);
            Session.Remove(login.ToString());
            Response.Redirect("SignIn.aspx", false);
        }
        
    }
    public class IPerson
    {
        public string Login { get; }
        public string Text { get; }
        public IPerson(string log, string text)
        {
            Login = log;
            Text = text;
        }
    }
}
