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
    public partial class PostPage : System.Web.UI.Page
    {
        private SqlConnection Connection = null;
        public static TextBox tb;
        protected async void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);
            await Connection.OpenAsync();

            HttpCookie Post_Id = Request.Cookies["PostID"];
            SqlCommand GetCurrentPost = new SqlCommand("SELECT Users.Login, UserPosts.Id, UserPosts.Message " +
                "FROM Users JOIN UserPosts ON Users.Id = UserPosts.User_Id", Connection);
            SqlDataReader ReadPost = await GetCurrentPost.ExecuteReaderAsync();
            while(await ReadPost.ReadAsync())
            {
                if(ReadPost["Id"].ToString() == Post_Id.Value)
                {
                    var div = new HtmlGenericControl("div");

                    Label lab = new Label
                    {
                        Text = ReadPost["Message"].ToString()
                    };
                    tb = new TextBox();
                    Button btnSend = new Button();
                    btnSend.Click += Bt_Click;
                    lab.Style.Add("font-size", "20px");
                    lab.Style.Add("font-family", "monospace");
                    div.Controls.Add(lab);
                    div.Controls.Add(tb);
                    div.Controls.Add(btnSend);
                    pst.Controls.Add(div);
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
        protected async void Bt_Click(object sender, EventArgs e)
        {
            HttpCookie Post_Id = Request.Cookies["PostID"];
            //var c = sender as TextBox;
            SqlCommand InsertComment = new SqlCommand("INSERT INTO CommentTable VALUES(@Msg_Id, @Text)", Connection);
            if(tb.Text != null)
            {
                InsertComment.Parameters.AddWithValue("Msg_Id", Post_Id.Value);
                InsertComment.Parameters.AddWithValue("Text", tb.Text);
                await InsertComment.ExecuteNonQueryAsync();
            }
        }
    }
}