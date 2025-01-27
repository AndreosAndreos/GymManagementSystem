using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Dlutto_management
{
    public partial class CoachEvaluations : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataBind();
            }
            
            if (Session["Email"] == null)
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
                    ShowAlert("All fields must be filled.");
                    return;
                }

                if (!IsValidEvaluation())
                {
                    ShowAlert("Evaluation must be an integer between 1 and 5.");
                    return;
                }

                addNewEvaluation();
            }
            catch (Exception ex)
            {
                ShowAlert("An error occurred while adding Evaluation: " + ex.Message);
            }
        }

        protected void Button2_Click(object sender, EventArgs e) // Update
        {
            try
            {
                if (!AreAllFieldsFilled())
                {
                    ShowAlert("All fields must be filled.");
                    return;
                }

                if (!IsValidEvaluation())
                {
                    ShowAlert("Evaluation must be an integer between 1 and 5.");
                    return;
                }

                updateEvaluation();
            }
            catch (Exception ex)
            {
                ShowAlert("An error occurred while updating Evaluation: " + ex.Message);
            }
        }

        protected void Button3_Click(object sender, EventArgs e) // Delete
        {
            try
            {
                if (!AreAllFieldsFilled())
                {
                    ShowAlert("All fields must be filled.");
                    return;
                }

                deleteEvaluation();
            }
            catch (Exception ex)
            {
                ShowAlert("An error occurred while deleting Evaluation: " + ex.Message);
            }
        }

        protected void Button4_Click(object sender, EventArgs e) // Go
        {
            getEvaluationByID();
        }

        void getEvaluationByID()
        {
            int evaluationID;
            if (!int.TryParse(TextBox3.Text.Trim(), out evaluationID))
            {
                ShowAlert("Invalid Evaluation ID");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Oceny_trenerow WHERE ID_oceny = @ID_oceny", con);
                    cmd.Parameters.AddWithValue("@ID_oceny", evaluationID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        TextBox4.Text = dt.Rows[0]["Ocena"].ToString();
                        TextBox1.Text = dt.Rows[0]["Komentarz"].ToString();
                        DropDownListClients.SelectedValue = dt.Rows[0]["ID_klienta"].ToString();
                        DropDownListTrainers.SelectedValue = dt.Rows[0]["ID_trenera"].ToString();
                    }
                    else
                    {
                        ShowAlert("Invalid Evaluation ID");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert(ex.Message);
            }
        }

        void deleteEvaluation()
        {
            int evaluationID;
            if (!int.TryParse(TextBox3.Text.Trim(), out evaluationID))
            {
                ShowAlert("Invalid Evaluation ID");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Oceny_trenerow WHERE ID_oceny = @ID_oceny", con);
                    cmd.Parameters.AddWithValue("@ID_oceny", evaluationID);
                    cmd.ExecuteNonQuery();
                }

                ShowAlert("Evaluation deleted successfully.");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ShowAlert("An error occurred while deleting Evaluation: " + ex.Message);
            }
        }

        void updateEvaluation()
        {
            int evaluationID, clientID, trainerID, evaluation;
            if (!int.TryParse(TextBox3.Text.Trim(), out evaluationID) ||
                !int.TryParse(DropDownListClients.SelectedValue, out clientID) ||
                !int.TryParse(DropDownListTrainers.SelectedValue, out trainerID) ||
                !int.TryParse(TextBox4.Text.Trim(), out evaluation))
            {
                ShowAlert("Invalid input format.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE dbo.Oceny_trenerow SET Ocena = @Ocena, Komentarz = @Komentarz, ID_klienta = @ID_klienta, ID_trenera = @ID_trenera WHERE ID_oceny = @ID_oceny", con);
                    cmd.Parameters.AddWithValue("@Ocena", evaluation);
                    cmd.Parameters.AddWithValue("@Komentarz", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_klienta", clientID);
                    cmd.Parameters.AddWithValue("@ID_trenera", trainerID);
                    cmd.Parameters.AddWithValue("@ID_oceny", evaluationID);
                    cmd.ExecuteNonQuery();
                }

                ShowAlert("Evaluation updated successfully.");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ShowAlert("An error occurred while updating Evaluation: " + ex.Message);
            }
        }

        void addNewEvaluation()
        {
            int clientID, trainerID, evaluation;
            if (!int.TryParse(DropDownListClients.SelectedValue, out clientID) ||
                !int.TryParse(DropDownListTrainers.SelectedValue, out trainerID) ||
                !int.TryParse(TextBox4.Text.Trim(), out evaluation))
            {
                ShowAlert("Invalid input format.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    
                    con.Open();
                    string query = "INSERT INTO dbo.Oceny_trenerow (Ocena, Komentarz, ID_klienta, ID_trenera) VALUES (@Ocena, @Komentarz, @ID_klienta, @ID_trenera)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Ocena", evaluation);
                    cmd.Parameters.AddWithValue("@Komentarz", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_klienta", clientID);
                    cmd.Parameters.AddWithValue("@ID_trenera", trainerID);
                    
                    int rowsAffected = cmd.ExecuteNonQuery();
                    ShowAlert("Error: No rows affected.");
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        ShowAlert("Evaluation added successfully.");
                        clearForm();
                        GridView1.DataBind();
                    }
                    else
                    {
                        ShowAlert("Error: No rows affected.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert("An error occurred while adding Evaluation: " + ex.Message);
            }
        }

        void ShowAlert(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{message}');", true);
        }

        bool IsValidEvaluation()
        {
            int evaluation;
            bool isInt = int.TryParse(TextBox4.Text.Trim(), out evaluation);
            return isInt && evaluation >= 1 && evaluation <= 5;
        }

        bool AreAllFieldsFilled()
        {
            return
                !string.IsNullOrWhiteSpace(TextBox4.Text.Trim()) &&
                !string.IsNullOrWhiteSpace(TextBox1.Text.Trim()) &&
                !string.IsNullOrWhiteSpace(DropDownListClients.SelectedValue) &&
                !string.IsNullOrWhiteSpace(DropDownListTrainers.SelectedValue);
        }

        void clearForm()
        {
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox1.Text = "";
            DropDownListClients.SelectedIndex = 0;
            DropDownListTrainers.SelectedIndex = 0;
        }
    }
}
