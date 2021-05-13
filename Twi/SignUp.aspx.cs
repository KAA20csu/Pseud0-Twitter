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
    public partial class SignUp : System.Web.UI.Page
    {
        private SqlConnection Connection = null;
        protected async void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);
            await Connection.OpenAsync();
        }

        protected async void Register_Click(object sender, EventArgs e)
        {
            SqlCommand RegistrateUser = new SqlCommand("INSERT INTO [Users] VALUES(@Login, @Password)", Connection);
            RegistrateUser.Parameters.AddWithValue("Login", LoginBox.Text);
            RegistrateUser.Parameters.AddWithValue("Password", PasswordBox.Text);
            await RegistrateUser.ExecuteNonQueryAsync();
            Response.Redirect("SignIn.aspx", false);
        }
    }
}