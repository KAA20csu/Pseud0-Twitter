using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Twi
{
    public partial class SignIn : System.Web.UI.Page
    {
        private SqlConnection Connection = null;
        protected async void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);
            await Connection.OpenAsync();
        }

        protected async void Login_Click(object sender, EventArgs e)
        {
            SqlCommand GetUsetInfo = new SqlCommand("SELECT [Login], [Password] FROM [Users]", Connection);

            SqlDataReader Reader = null;
            Person person = null;
            try
            {
                Reader = await GetUsetInfo.ExecuteReaderAsync();
                while(await Reader.ReadAsync())
                {
                    person = new Person(Reader["Login"].ToString(), Reader["Password"].ToString());
                }
            }
            catch { }
            finally
            {
                if (Reader != null)
                    Reader.Close();
            }
            if(PasswordBox.Text == person.Password)
            {
                Response.Redirect("UserPage.aspx");
            }
        }
    }
    class Person
    {
        public string Login { get;}
        public string Password { get;}

        public Person(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}