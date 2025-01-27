using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dlutto_management
{
    public partial class Toolsmanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            if (Session["Email"] == null)
            {
                Response.Redirect("userlogin.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e) // Add
        {
            try
            {
                if (checkIfEquipementExists())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Equipment with this ID already exists.');", true);
                }
                else
                {
                    addNewEquipement();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Equipment added successfully.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('An error occurred while adding equipment: " + ex.Message + "');", true);
            }
        }


        protected void Button2_Click(object sender, EventArgs e) // Update
        {
            if (checkIfEquipementExists())
            {
                updateEquipement();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Reservation with this ID doesn't exist.');", true);
            }
        }

        protected void Button3_Click(object sender, EventArgs e) // Delete
        {
            if (checkIfEquipementExists())
            {
                deleteEquipement();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Reservation with this ID doesn't exist.');", true);
            }
        }

        protected void Button4_Click(object sender, EventArgs e) // Go
        {
            getEqupementByID();
        }

        void getEqupementByID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Sprzet WHERE ID_sprzetu = @ID_sprzetu", con);
                    cmd.Parameters.AddWithValue("@ID_sprzetu", TextBox3.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        TextBox4.Text = dt.Rows[0]["Nazwa"].ToString();
                        TextBox1.Text = dt.Rows[0]["Opis"].ToString();
                        TextBox5.Text = dt.Rows[0]["Data_zakupu"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Invalid Equipement ID');", true);
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

        void deleteEquipement()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Sprzet WHERE ID_sprzetu = @ID_sprzetu", con);
                    cmd.Parameters.AddWithValue("@ID_sprzetu", TextBox3.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Equipement deleted successfully.');</script>");
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

        void updateEquipement()
        {
            try
            {
                if (string.IsNullOrEmpty(TextBox4.Text) || string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox5.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Proszę wypełnić wszystkie pola.');", true);
                    return;
                }

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE dbo.Sprzet SET Nazwa = @Nazwa, Opis = @Opis, Data_zakupu = @Data_zakupu WHERE ID_sprzetu = @ID_sprzetu", con);
                    cmd.Parameters.AddWithValue("@Nazwa", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@Opis", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Data_zakupu", TextBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_sprzetu", TextBox3.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Equipement updated successfully.');</script>");
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


        void addNewEquipement()
        {
            try
            {
                if (string.IsNullOrEmpty(TextBox4.Text) || string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox5.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please fill in all fields.');", true);
                    return;
                }

                SqlConnection con = new SqlConnection(strcon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO Sprzet (Nazwa, Opis, Data_zakupu) VALUES (@Nazwa, @Opis, @Data_zakupu)", con);
                cmd.Parameters.AddWithValue("@Nazwa", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@Opis", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@Data_zakupu", TextBox5.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();

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


        bool checkIfEquipementExists()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Sprzet WHERE ID_sprzetu = @ID_sprzetu", con);
                    cmd.Parameters.AddWithValue("@ID_sprzetu", TextBox3.Text.Trim());
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
        void clearForm()
        {
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox1.Text = "";
            TextBox5.Text = "";
        }
    }
}