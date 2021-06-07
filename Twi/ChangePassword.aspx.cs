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
    public partial class ChangePassword : System.Web.UI.Page
    {
        private SqlConnection Connection = null;
        private List<string> AllMails { get; set; } = new List<string>(); 
        protected  async void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);
            await Connection.OpenAsync();

            var getAllMails = new SqlCommand("SELECT Users.Mail FROM Users", Connection);
            SqlDataReader Reader = null;
            try 
            {
                Reader = await getAllMails.ExecuteReaderAsync();

                while (await Reader.ReadAsync())
                {
                    AllMails.Add(Reader["Mail"].ToString());
                }
            }
            catch { }
            finally
            {
                if (Reader != null)
                    Reader.Close();
            }
        }

        protected void ChangeBt_Click(object sender, EventArgs e)
        {
            if (AllMails.Contains(checkMail.Text))
            {
                if(password1.Text == password2.Text)
                {
                    ChangingPassword();

                }
            }
        }
        protected async void ChangingPassword()
        {
            SqlCommand com = new SqlCommand("UPDATE [Users] SET [Password] = @password WHERE [Mail] = @mail", Connection);

            com.Parameters.AddWithValue("mail", checkMail.Text);
            com.Parameters.AddWithValue("password", password2.Text);
            await com.ExecuteNonQueryAsync();

            Response.Redirect("SignIn.aspx", false);
        }

    }
}