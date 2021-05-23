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
    public partial class News : System.Web.UI.Page
    {
        private SqlConnection Connection = null;
        protected async void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["PosctCN"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);
            await Connection.OpenAsync();

            SqlCommand GetPosts = new SqlCommand("SELECT [Name], [Post] FROM [Posts]", Connection);
            SqlDataReader Reader = null;
            List<string> postList = new List<string>();
            string name = "";
            try
            {
                Reader = await GetPosts.ExecuteReaderAsync();
                while (await Reader.ReadAsync())
                {
                    postList.Add(Reader["Post"].ToString());
                    name = Reader["Name"].ToString();
                }
            }
            catch { }
            finally
            {
                if (Reader != null)
                    Reader.Close();
            }
            foreach(var post in postList)
            {
                Label pLabel = new Label();
                pLabel.Text = name + "\n" + post;
                Controls.Add(pLabel);
            }
            
        }
    }
}