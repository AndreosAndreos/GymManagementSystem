using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Dlutto_management
{
    public partial class TrainersManagement : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("userlogin.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e) // Add
        {
            if (checkIfTrainerExists())
            {
                Response.Write("<script>alert('Trainer with this ID already exists.');</script>");
            }
            else
            {
                addNewTrainer();
            }
        }

        protected void Button2_Click(object sender, EventArgs e) // Update
        {
            if (checkIfTrainerExists())
            {
                updateTrainer();
            }
            else
            {
                Response.Write("<script>alert('Trainer with this ID doesn't exist.');</script>");
            }
        }

        protected void Button3_Click(object sender, EventArgs e) // Delete
        {
            if (checkIfTrainerExists())
            {
                deleteTrainer();
            }
            else
            {
                Response.Write("<script>alert('Trainer with this ID doesn't exist.');</script>");
            }
        }

        protected void Button4_Click(object sender, EventArgs e) // Go
        {
            getTrainerByID();
        }

        void getTrainerByID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Trenerzy WHERE ID_trenera = @ID_trenera", con);
                    cmd.Parameters.AddWithValue("@ID_trenera", TextBox3.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        TextBox1.Text = dt.Rows[0]["Specjalizacja"].ToString();
                        DropDownListUserID.SelectedValue = dt.Rows[0]["ID_uzytkownika"].ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid Trainer ID');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void deleteTrainer()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Trenerzy WHERE ID_Trenera = @ID_Trenera", con);
                    cmd.Parameters.AddWithValue("@ID_Trenera", TextBox3.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Trainer deleted successfully.');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void updateTrainer()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE dbo.Trenerzy SET Specjalizacja = @Specjalizacja, ID_uzytkownika = @ID_uzytkownika WHERE ID_trenera = @ID_trenera", con);
                    cmd.Parameters.AddWithValue("@Specjalizacja", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_uzytkownika", DropDownListUserID.SelectedValue);
                    cmd.Parameters.AddWithValue("@ID_trenera", TextBox3.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Trainer updated successfully.');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void addNewTrainer()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Trenerzy (Specjalizacja, ID_uzytkownika) VALUES (@Specjalizacja, @ID_uzytkownika)", con);
                    cmd.Parameters.AddWithValue("@Specjalizacja", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_uzytkownika", DropDownListUserID.SelectedValue);
                    cmd.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Trainer added successfully.');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool checkIfTrainerExists()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Trenerzy WHERE ID_trenera = @ID_trenera", con);
                    cmd.Parameters.AddWithValue("@ID_trenera", TextBox3.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt.Rows.Count >= 1;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        void clearForm()
        {
            TextBox1.Text = "";
            TextBox3.Text = "";
            DropDownListUserID.SelectedIndex = 0;
        }
    }
}
