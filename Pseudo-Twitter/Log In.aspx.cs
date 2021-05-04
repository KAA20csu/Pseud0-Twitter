using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pseudo_Twitter
{
    public partial class Log_In : System.Web.UI.Page
    {
        private SqlConnection BaseDateConnection = null;
        protected async void Page_Load(object sender, EventArgs e)
        {
            string connection = ConfigurationManager.ConnectionStrings["UserBaseConnection"].ConnectionString;
            BaseDateConnection = new SqlConnection(connection);

            await BaseDateConnection.OpenAsync();
        }


        protected async void LogIn_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> GetUsers = new Dictionary<string, string>();

            SqlCommand UsersFromBD = new SqlCommand("SELECT [Login], [Password] FROM [Users]", BaseDateConnection);

            SqlDataReader DataReader = null;
            try
            {
                DataReader = await UsersFromBD.ExecuteReaderAsync();
                
                while(await DataReader.ReadAsync())
                {
                    GetUsers.Add(Convert.ToString(DataReader["Login"]), Convert.ToString(DataReader["Password"]));
                }
            }
            catch { }
            finally
            {
                if(DataReader != null)
                {
                    DataReader.Close();
                }
            }
        }
    }
}