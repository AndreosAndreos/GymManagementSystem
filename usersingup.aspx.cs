using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Reflection.Emit;

namespace Dlutto_management
{ 
    public partial class usersingup : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkMemberExists())
            {
                Response.Write("<script>alert('Member already exists');</script>");
            }
            else
            {
                SignUpNewUser();
            }
            
        }
        bool checkMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from Uzytkownicy where Email='" + TextBox5.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count >= 1) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
            
        }

        
        public string HashPassword(string Haslo)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Haslo));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        void SignUpNewUser()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TextBox7.Text.Trim()) || TextBox7.Text.Trim().Length < 2)
                {
                    Response.Write("<script>alert('Haslo nie moze byc puste/nmusi byc przynajmniej 3');</script>");
                    return; 
                }

                string hashedPassword = HashPassword(TextBox7.Text.Trim());

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO Uzytkownicy(Imie, Nazwisko, Email, Haslo, Rola_Uzytkownika, Data_urodzenia, ID_Uprawnienia, Nr_telefonu, Data_rozp_czlonkostwa) " +
                    "values (@Imie, @Nazwisko, @Email, @Haslo, @Rola_Uzytkownika, @Data_urodzenia, @ID_Uprawnienia, @Nr_telefonu, @Data_rozp_czlonkostwa)", con);
                cmd.Parameters.AddWithValue("@Imie", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@Nazwisko", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@Haslo", hashedPassword); 
                cmd.Parameters.AddWithValue("@Rola_Uzytkownika", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@Data_Urodzenia", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_Uprawnienia", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@Nr_telefonu", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@Data_rozp_czlonkostwa", TextBox11.Text.Trim());
                
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Sign up succesfull. Go to User Login to Login');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

    }
}