using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace Dlutto_management
{
    public partial class MemberManagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            if (Session["Role"] == null || Session["role"].ToString() != "Admin")
            {
                Response.Redirect("userlogin.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e) // Add
        {
            try
            {
                if (checkIfMemberExists())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Member with this ID or Email already exists.');", true);
                }
                else
                {
                    addNewMember();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Member added successfully.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('An error occurred while adding member: {ex.Message}');", true);
            }
        }


        protected void Button2_Click(object sender, EventArgs e) // Update
        {
            if (!checkIfMemberExistsById())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Member with this ID doesn't exist.');", true);
                return;
            }

            if (checkIfEmailExistsForOtherMember())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Another member with this Email already exists.');", true);
                return;
            }

            updateMember();
        }


        protected void Button3_Click(object sender, EventArgs e) // Delete
        {
            if (checkIfMemberExists())
            {
                deleteMember();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Member with this ID doesn't exist.');", true);
            }
        }

        protected void Button4_Click(object sender, EventArgs e) // Go
        {
            getMemberByID();
        }

        bool checkIfMemberExistsById()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Uzytkownicy WHERE ID_Uzytkownika = @ID_Uzytkownika", con);
                    cmd.Parameters.AddWithValue("@ID_Uzytkownika", TextBox3.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt.Rows.Count >= 1;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{ex.Message}');", true);
                return false;
            }
        }

        bool checkIfEmailExistsForOtherMember()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Uzytkownicy WHERE Email = @Email AND ID_Uzytkownika != @ID_Uzytkownika", con);
                    cmd.Parameters.AddWithValue("@Email", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_Uzytkownika", TextBox3.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt.Rows.Count >= 1;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{ex.Message}');", true);
                return false;
            }
        }



        void getMemberByID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Uzytkownicy WHERE ID_Uzytkownika = @ID_Uzytkownika", con);
                    cmd.Parameters.AddWithValue("@ID_Uzytkownika", TextBox3.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        TextBox4.Text = dt.Rows[0]["Imie"].ToString();
                        TextBox2.Text = dt.Rows[0]["Nazwisko"].ToString();
                        TextBox1.Text = dt.Rows[0]["Email"].ToString();
                        // Do not populate password field for security reasons
                        DropDownList3.SelectedValue = dt.Rows[0]["Rola_Uzytkownika"].ToString();
                        TextBox7.Text = dt.Rows[0]["Data_urodzenia"].ToString();
                        DropDownList1.SelectedValue = dt.Rows[0]["ID_uprawnienia"].ToString();
                        TextBox9.Text = dt.Rows[0]["Nr_telefonu"].ToString();
                        TextBox10.Text = dt.Rows[0]["Data_rozp_czlonkostwa"].ToString();
                        TextBox11.Text = dt.Rows[0]["Data_zak_czlonkostwa"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Invalid Member ID');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{ex.Message}');", true);
            }
        }

        void deleteMember()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Uzytkownicy WHERE ID_Uzytkownika = @ID_Uzytkownika", con);
                    cmd.Parameters.AddWithValue("@ID_Uzytkownika", TextBox3.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Member deleted successfully.');", true);
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{ex.Message}');", true);
            }
        }

        void updateMember()
        {
            try
            {
                if (string.IsNullOrEmpty(TextBox4.Text) || string.IsNullOrEmpty(TextBox2.Text) || string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(DropDownList3.SelectedValue) || string.IsNullOrEmpty(TextBox7.Text) || string.IsNullOrEmpty(TextBox9.Text) || string.IsNullOrEmpty(TextBox10.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please fill in all fields.');", true);
                    return;
                }

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE dbo.Uzytkownicy SET Imie = @Imie, Nazwisko = @Nazwisko, Email = @Email, Haslo = @Haslo, Rola_Uzytkownika = @Rola_Uzytkownika, Data_urodzenia = @Data_urodzenia, ID_Uprawnienia = @ID_Uprawnienia, Nr_telefonu = @Nr_telefonu, Data_rozp_czlonkostwa = @Data_rozp_czlonkostwa, Data_zak_czlonkostwa = @Data_zak_czlonkostwa WHERE ID_Uzytkownika = @ID_Uzytkownika", con);
                    cmd.Parameters.AddWithValue("@Imie", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@Nazwisko", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Haslo", HashPassword(TextBox5.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Rola_Uzytkownika", DropDownList3.SelectedValue);
                    cmd.Parameters.AddWithValue("@Data_urodzenia", TextBox7.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_Uprawnienia", DropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@Nr_telefonu", TextBox9.Text.Trim());
                    cmd.Parameters.AddWithValue("@Data_rozp_czlonkostwa", TextBox10.Text.Trim());
                    cmd.Parameters.AddWithValue("@Data_zak_czlonkostwa", TextBox11.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_Uzytkownika", TextBox3.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Member updated successfully.');", true);
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{ex.Message}');", true);
            }
        }

        void addNewMember()
        {
            try
            {
                if (string.IsNullOrEmpty(TextBox4.Text) || string.IsNullOrEmpty(TextBox2.Text) || string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(DropDownList3.SelectedValue) || string.IsNullOrEmpty(TextBox7.Text) || string.IsNullOrEmpty(TextBox9.Text) || string.IsNullOrEmpty(TextBox10.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please fill in all fields.');", true);
                    return;
                }

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("INSERT INTO Uzytkownicy (Imie, Nazwisko, Email, Haslo, Rola_Uzytkownika, Data_urodzenia, ID_Uprawnienia, Nr_telefonu, Data_rozp_czlonkostwa, Data_zak_czlonkostwa) VALUES (@Imie, @Nazwisko, @Email, @Haslo, @Rola_Uzytkownika, @Data_urodzenia, @ID_Uprawnienia, @Nr_telefonu, @Data_rozp_czlonkostwa, @Data_zak_czlonkostwa)", con);
                    cmd.Parameters.AddWithValue("@Imie", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@Nazwisko", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Haslo", HashPassword(TextBox5.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Rola_Uzytkownika", DropDownList3.SelectedValue);
                    cmd.Parameters.AddWithValue("@Data_urodzenia", TextBox7.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_Uprawnienia", DropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@Nr_telefonu", TextBox9.Text.Trim());
                    cmd.Parameters.AddWithValue("@Data_rozp_czlonkostwa", TextBox10.Text.Trim());
                    cmd.Parameters.AddWithValue("@Data_zak_czlonkostwa", TextBox11.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Member added successfully.');", true);
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{ex.Message}');", true);
            }
        }


        bool checkIfMemberExists()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Uzytkownicy WHERE ID_Uzytkownika = @ID_Uzytkownika OR Email = @Email", con);
                    cmd.Parameters.AddWithValue("@ID_Uzytkownika", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", TextBox1.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt.Rows.Count >= 1;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{ex.Message}');", true);
                return false;
            }
        }


        void clearForm()
        {
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox2.Text = "";
            TextBox1.Text = "";
            TextBox5.Text = "";
            DropDownList3.SelectedIndex = 0; 
            TextBox7.Text = "";
            DropDownList1.SelectedIndex = 0; 
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
        }


        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
