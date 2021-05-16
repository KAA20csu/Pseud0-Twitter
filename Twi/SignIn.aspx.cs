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
            SqlCommand GetUsetInfo = new SqlCommand("SELECT [Login], [Password], [Mail], [Sex] FROM [Users]", Connection);

            SqlDataReader Reader = null;
            Person person = null;
            try
            {
                Reader = await GetUsetInfo.ExecuteReaderAsync();
                while(await Reader.ReadAsync())
                {
                    person = new Person(Reader["Login"].ToString(), Reader["Password"].ToString(), Reader["Mail"].ToString(), Reader["Sex"].ToString());
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
                HttpCookie logCookie = new HttpCookie("login", LoginBox.Text);
                HttpCookie mailCookie = new HttpCookie("mail", person.Mail);
                HttpCookie sexCookie = new HttpCookie("sex", person.Sex);
                Response.Cookies.Add(logCookie);
                Response.Cookies.Add(mailCookie);
                Response.Cookies.Add(sexCookie);
                Response.Redirect("UserPage.aspx", false);
            }
        }
    }
    class Person
    {
        public string Login { get;}
        public string Password { get;}
        public string Mail { get; }
        public string Sex { get; }
        public Person(string login, string password, string mail, string sex)
        {
            Login = login;
            Password = password;
            Mail = mail;
            Sex = sex;
        }
    }
}