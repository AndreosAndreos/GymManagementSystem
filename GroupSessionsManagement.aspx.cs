using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Dlutto_management
{
    public partial class GroupSessionsManagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin" || Session["Role"].ToString() != "Trener")
            {
                Response.Redirect("userlogin.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e) // Add
        {
            try
            {
                if (!AreAllFieldsFilled())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('All fields must be filled.');", true);
                    return;
                }

                if (checkIfSessionExists())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Evaluation with this ID already exists.');", true);
                }
                else
                {
                    addNewSession();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('An error occurred while adding Evaluation: " + ex.Message + "');", true);
            }
        }

        protected void Button2_Click(object sender, EventArgs e) // Update
        {
            try
            {
                if (!AreAllFieldsFilled())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('All fields must be filled.');", true);
                    return;
                }

                if (checkIfSessionExists())
                {
                    updateSession();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Evaluation with this ID doesn\'t exist.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('An error occurred while updating Evaluation: " + ex.Message + "');", true);
            }
        }

        protected void Button3_Click(object sender, EventArgs e) // Delete
        {
            try
            {
                if (!AreAllFieldsFilled())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('All fields must be filled.');", true);
                    return;
                }

                if (checkIfSessionExists())
                {
                    deleteSession();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Evaluation with this ID doesn\'t exist.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('An error occurred while deleting Evaluation: " + ex.Message + "');", true);
            }
        }

        protected void Button4_Click(object sender, EventArgs e) // Go
        {
            getSessionByID();
        }

        void getSessionByID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Zajecia_grupowe WHERE ID_zajec = @ID_zajec", con);
                    cmd.Parameters.AddWithValue("@ID_zajec", TextBox3.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        TextBox4.Text = dt.Rows[0]["Nazwa"].ToString();
                        TextBox2.Text = dt.Rows[0]["Opis"].ToString();
                        TextBox1.Text = dt.Rows[0]["Data_rozpoczecia"].ToString();
                        TextBox5.Text = dt.Rows[0]["Opis"].ToString();
                        DropDownList1.SelectedValue = dt.Rows[0]["ID_trenera_prowadzacego"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Invalid Evaluation ID');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                string script = $"alert('{errorMessage}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
            }
        }

        void deleteSession()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Zajecia_grupowe WHERE ID_zajec = @ID_zajec", con);
                    cmd.Parameters.AddWithValue("@ID_zajec", TextBox3.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Session deleted successfully.');", true);
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                string script = $"alert('{errorMessage}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
            }
        }

        void updateSession()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE dbo.Zajecia_grupowe SET Nazwa = @Nazwa, Opis = @Opis, Data_rozpoczecia = @Data_rozpoczecia, Liczba_miejsc = @Liczba_miejsc, ID_trenera_prowadzacego = @ID_trenera_prowadzacego  WHERE ID_zajec = @ID_zajec", con);
                    cmd.Parameters.AddWithValue("@Nazwa", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@Opis", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Data_rozpoczecia", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Liczba_miejsc", TextBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_trenera_prowadzacego", DropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@ID_zajec", TextBox3.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Evaluation updated successfully.');", true);
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                string script = $"alert('An error occurred while updating Evaluation: {errorMessage}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
            }
        }

        void addNewSession()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("INSERT INTO Zajecia_grupowe (Nazwa, Opis, Data_rozpoczecia, Liczba_miejsc, ID_trenera_prowadzacego) VALUES (@Nazwa, @Opis, @Data_rozpoczecia, @Liczba_miejsc, @ID_trenera_prowadzacego)", con);
                    cmd.Parameters.AddWithValue("@Nazwa", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@Opis", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Data_rozpoczecia", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Liczba_miejsc", TextBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_trenera_prowadzacego", DropDownList1.SelectedValue);

                    cmd.ExecuteNonQuery();

                    clearForm();
                    GridView1.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Session added successfully.');", true);
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 547)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Cannot add the session because of a foreign key constraint. Please ensure the referenced trainer ID exists.');", true);
                }
                else
                {
                    string errorMessage = sqlEx.Message;
                    string script = $"alert('An error occurred while adding Session: {errorMessage}');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                string script = $"alert('An error occurred while adding Session: {errorMessage}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
            }
        }

        bool checkIfSessionExists()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Zajecia_grupowe WHERE ID_zajec = @ID_zajec", con);
                    cmd.Parameters.AddWithValue("@ID_zajec", TextBox3.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt.Rows.Count >= 1;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                string script = $"alert('{errorMessage}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return false;
            }
        }
        bool AreAllFieldsFilled()
        {
            return
                
                !string.IsNullOrWhiteSpace(TextBox4.Text.Trim()) &&
                !string.IsNullOrWhiteSpace(TextBox2.Text.Trim()) &&
                !string.IsNullOrWhiteSpace(TextBox1.Text.Trim()) &&
                !string.IsNullOrWhiteSpace(TextBox5.Text.Trim()) &&
                !string.IsNullOrWhiteSpace(DropDownList1.SelectedValue);
        }

        void clearForm()
        {
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox2.Text = "";
            TextBox1.Text = "";
            TextBox5.Text = "";
            DropDownList1.SelectedIndex = 0;
        }
    }
}