using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Dlutto_management
{
    public partial class ReservationsManagement : System.Web.UI.Page
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
            if (checkIfReservationExists())
            {
                Response.Write("<script>alert('Reservation with this ID already exists.');</script>");
            }
            else
            {
                addNewReservation();
            }
        }

        protected void Button2_Click(object sender, EventArgs e) // Update
        {
            if (checkIfReservationExists())
            {
                updateReservation();
            }
            else
            {
                Response.Write("<script>alert('Reservation with this ID doesn't exist.');</script>");
            }
        }

        protected void Button3_Click(object sender, EventArgs e) // Delete
        {
            if (checkIfReservationExists())
            {
                deleteReservation();
            }
            else
            {
                Response.Write("<script>alert('Reservation with this ID doesn't exist.');</script>");
            }
        }

        protected void Button4_Click(object sender, EventArgs e) // Go
        {
            getReservationByID();
        }

        void getReservationByID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Rezerwacje WHERE ID_rezerwacji = @ID_rezerwacji", con);
                    cmd.Parameters.AddWithValue("@ID_rezerwacji", TextBox3.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        TextBox4.Text = dt.Rows[0]["Data_rezerwacji"].ToString();
                        DropDownListClientID.SelectedValue = dt.Rows[0]["ID_klienta"].ToString();
                        DropDownListGroupSessionID.SelectedValue = dt.Rows[0]["ID_zajec_grupowych"].ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid Reservation ID');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void deleteReservation()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Rezerwacje WHERE ID_rezerwacji = @ID_rezerwacji", con);
                    cmd.Parameters.AddWithValue("@ID_rezerwacji", TextBox3.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Reservation deleted successfully.');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void updateReservation()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE dbo.Rezerwacje SET Data_rezerwacji = @Data_rezerwacji, ID_klienta = @ID_klienta, ID_zajec_grupowych = @ID_zajec_grupowych WHERE ID_rezerwacji = @ID_rezerwacji", con);
                    cmd.Parameters.AddWithValue("@Data_rezerwacji", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_klienta", DropDownListClientID.SelectedValue);
                    cmd.Parameters.AddWithValue("@ID_zajec_grupowych", DropDownListGroupSessionID.SelectedValue);
                    cmd.Parameters.AddWithValue("@ID_rezerwacji", TextBox3.Text.Trim());

                    cmd.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Reservation updated successfully.');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void addNewReservation()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Rezerwacje (Data_rezerwacji, ID_klienta, ID_zajec_grupowych) VALUES (@Data_rezerwacji, @ID_klienta, @ID_zajec_grupowych)", con);
                    cmd.Parameters.AddWithValue("@Data_rezerwacji", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_klienta", DropDownListClientID.SelectedValue);
                    cmd.Parameters.AddWithValue("@ID_zajec_grupowych", DropDownListGroupSessionID.SelectedValue);
                    cmd.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Reservation added successfully.');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool checkIfReservationExists()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Rezerwacje WHERE ID_rezerwacji = @ID_rezerwacji", con);
                    cmd.Parameters.AddWithValue("@ID_rezerwacji", TextBox3.Text.Trim());
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
            TextBox3.Text = "";
            TextBox4.Text = "";
            DropDownListClientID.SelectedIndex = 0;
            DropDownListGroupSessionID.SelectedIndex = 0;
        }
    }
}