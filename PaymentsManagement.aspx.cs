using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Dlutto_management
{
    public partial class PaymentsManagement : System.Web.UI.Page
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
            try
            {
                if (checkIfPaymentExists())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Payment with this ID already exists.');", true);
                }
                else
                {
                    addNewPayment();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Payment added successfully.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('An error occurred while adding Payment: " + ex.Message + "');", true);
            }
        }


        protected void Button2_Click(object sender, EventArgs e) // Update
        {
            if (checkIfPaymentExists())
            {
                updatePayment();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Payment with this ID doesn't exist.');", true);
            }
        }

        protected void Button3_Click(object sender, EventArgs e) // Delete
        {
            if (checkIfPaymentExists())
            {
                deletePayment();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Payment with this ID doesn't exist.');", true);
            }
        }

        protected void Button4_Click(object sender, EventArgs e) // Go
        {
            getPaymentByID();
        }

        void getPaymentByID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Platnosci WHERE ID_platnosci = @ID_platnosci", con);
                    cmd.Parameters.AddWithValue("@ID_platnosci", TextBox3.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        TextBox4.Text = dt.Rows[0]["Data_platnosci"].ToString();
                        TextBox6.Text = dt.Rows[0]["Kwota"].ToString();
                        DropDownList1.SelectedValue = dt.Rows[0]["ID_klienta"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Invalid Payment ID');", true);
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


        void deletePayment()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Platnosci WHERE ID_platnosci = @ID_platnosci", con);
                    cmd.Parameters.AddWithValue("@ID_platnosci", TextBox3.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Payment deleted successfully.');</script>");
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

        void updatePayment()
        {
            try
            {

                if (string.IsNullOrEmpty(TextBox4.Text) || string.IsNullOrEmpty(TextBox3.Text) || string.IsNullOrEmpty(TextBox6.Text) || !string.IsNullOrWhiteSpace(DropDownList1.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please fill all necesary fields.');", true);
                    return;
                }

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE dbo.Platnosci SET Data_platnosci = @Data_platnosci, Kwota = @Kwota, ID_klienta = @ID_klienta WHERE ID_sprzetu = @ID_platnosci", con);
                    cmd.Parameters.AddWithValue("@Data_platnosci", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@Kwota", TextBox6.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_klienta", DropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@ID_platnosci", TextBox3.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Payment updated successfully.');</script>");
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


        void addNewPayment()
        {
            try
            {
                int clientID, kwota;
                if (!int.TryParse(DropDownList1.SelectedValue, out clientID) ||
                    !int.TryParse(TextBox6.Text.Trim(), out kwota))
                {
                    ShowAlert("Invalid input format.");
                    return;
                }

                if (string.IsNullOrEmpty(TextBox4.Text) || string.IsNullOrEmpty(TextBox6.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please fill in all fields.');", true);
                    return;
                }

                SqlConnection con = new SqlConnection(strcon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO Platnosci (Data_platnosci, Kwota, ID_klienta) VALUES (@Data_platnosci, @Kwota, @ID_klienta)", con);
                cmd.Parameters.AddWithValue("@Data_platnosci", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@Kwota", kwota);
                cmd.Parameters.AddWithValue("@ID_klienta", clientID);

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
        void ShowAlert(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{message}');", true);
        }
        bool checkIfPaymentExists()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Platnosci WHERE ID_platnosci = @ID_platnosci", con);
                    cmd.Parameters.AddWithValue("@ID_platnosci", TextBox3.Text.Trim());
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
            TextBox6.Text = "";
            DropDownList1.SelectedIndex = 0;
        }
    }
}