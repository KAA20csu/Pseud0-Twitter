using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

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
            SqlCommand GetUsetInfo = new SqlCommand("SELECT [Login], [Password], [Mail], [Sex] FROM [Users]", Connection);
            List<Person> RegPers = new List<Person>();
            SqlDataReader Reader = null;
            Person person = null;
            try
            {
                Reader = await GetUsetInfo.ExecuteReaderAsync();
                while (await Reader.ReadAsync())
                {
                    person = new Person(Reader["Login"].ToString(), Reader["Password"].ToString(), Reader["Mail"].ToString(), Reader["Sex"].ToString());
                    RegPers.Add(person);
                }
            }
            catch { }
            finally
            {
                if (Reader != null)
                    Reader.Close();
            }
            if (!Check(RegPers))
            {
                SqlCommand RegistrateUser = new SqlCommand("INSERT INTO [Users] VALUES(@Login, @Password, @Mail, @Sex, @Avatar)", Connection);
                RegistrateUser.Parameters.AddWithValue("Login", LoginBox.Text);
                RegistrateUser.Parameters.AddWithValue("Password", PasswordBox.Text);
                RegistrateUser.Parameters.AddWithValue("Mail", Mail.Text);
                RegistrateUser.Parameters.AddWithValue("Sex", Sex.SelectedValue.ToString().Trim('\n', '\r', ' '));
                RegistrateUser.Parameters.AddWithValue("Avatar", "ava.jpg");
                await RegistrateUser.ExecuteNonQueryAsync();
                Response.Redirect("SignIn.aspx", false);
            }
            else
            {
                string script = "alert('Такой логин уже существует!')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MessageBox", script, true);
            }
        }
        private bool Check(List<Person> RegPers)
        {
            foreach(var user in RegPers)
            {
                if(user.Login == LoginBox.Text)
                {
                    return true;
                }
            }
            return false;
        }
    }
}