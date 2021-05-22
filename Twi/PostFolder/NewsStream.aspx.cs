using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Twi
{
    public partial class NewsStream : System.Web.UI.Page
    {
        private SqlConnection Connection = null;
        protected async void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["PosctCN"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);
            await Connection.OpenAsync();

            SqlCommand GetPosts = new SqlCommand("SELECT [Name], [Post] FROM [Posts]", Connection);
            SqlDataReader Reader = null;
            List<string> postss = new List<string>();
            HttpCookie Name = Request.Cookies["login"];
            IUser user = null;
            try
            {
                Reader = await GetPosts.ExecuteReaderAsync();
                while (await Reader.ReadAsync())
                {
                    postss.Add(Reader["Post"].ToString());
                    user = new IUser(Reader["Name"].ToString(), postss);
                }
            }
            catch { }
            finally
            {
                if (Reader != null)
                    Reader.Close();
            }
            if(user.Name == Name.Value)
            {
                foreach(var post in user.Posts)
                {
                    Label lpost = new Label();
                    TextBox comment = new TextBox();
                    lpost.Text = post + "\n";
                    form1.Controls.Add(lpost);
                    form1.Controls.Add(comment);
                }
            }
        }
    }
    public class IUser
    {
        public string Name;
        public List<string> Posts;
        public IUser(string name, List<string> posts)
        {
            Name = name;
            Posts = posts;
        }
    }
}