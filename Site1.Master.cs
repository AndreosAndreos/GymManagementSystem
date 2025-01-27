using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dlutto_management
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["role"] == null || Session["role"].ToString().Equals(""))
                {
                    LinkButton1.Enabled = true;  // user login link button
                    LinkButton2.Enabled = true;  // sign up link button

                    LinkButton3.Enabled = true; // logout link button
                    LinkButton7.Enabled = false; // hello user link button

                    
                    LinkButton11.Enabled = false; // trainers management link button
                    LinkButton12.Enabled = false; // reservations management link button
                    LinkButton8.Enabled = false; // equipment management link button
                    LinkButton9.Enabled = false; // payments management link button
                    LinkButton10.Enabled = false; // member management link button
                }
                else if (Session["role"].ToString().Equals("Klient"))
                {
                    LinkButton1.Enabled = false;  // user login link button
                    LinkButton2.Enabled = false;  // sign up link button

                    LinkButton3.Enabled = true;   // logout link button
                    LinkButton7.Enabled = true;   // hello user link button
                    LinkButton7.Text = "Hello " + Session["Imie"].ToString();

                    
                    LinkButton11.Enabled = false; // trainers management link button
                    LinkButton12.Enabled = true;  // reservations management link button
                    LinkButton8.Enabled = false;  // equipment management link button
                    LinkButton9.Enabled = false;  // payments management link button
                    LinkButton10.Enabled = false; // member management link button
                }
                else if (Session["role"].ToString().Equals("Trener"))
                {
                    LinkButton1.Enabled = false;  // user login link button
                    LinkButton2.Enabled = false;  // sign up link button

                    LinkButton3.Enabled = true;   // logout link button
                    LinkButton7.Enabled = true;   // hello user link button
                    LinkButton7.Text = "Hello " + Session["Imie"].ToString();

                    
                    LinkButton11.Enabled = false; // trainers management link button
                    LinkButton12.Enabled = true; // reservations management link button
                    LinkButton8.Enabled = true;   // equipment management link button
                    LinkButton9.Enabled = false;  // payments management link button
                    LinkButton10.Enabled = false; // member management link button
                }
                else if (Session["role"].ToString().Equals("Admin"))
                {
                    LinkButton1.Enabled = false;  // user login link button
                    LinkButton2.Enabled = false;  // sign up link button

                    LinkButton3.Enabled = true;   // logout link button
                    LinkButton7.Enabled = true;   // hello user link button
                    LinkButton7.Text = "Hello Admin";

                    
                    LinkButton11.Enabled = true;  // trainers management link button
                    LinkButton12.Enabled = true;  // reservations management link button
                    LinkButton8.Enabled = true;   // equipment management link button
                    LinkButton9.Enabled = true;   // payments management link button
                    LinkButton10.Enabled = true;  // member management link button
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void SetLogoutVisibility()
        {
            if (Session["Email"] != null)
            {
                LinkButton3.Visible = true;
            }
            else
            {
                LinkButton3.Visible = false;
            }
        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("userlogin.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("usersingup.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("logout.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("CoachEvaluations.aspx");
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Response.Redirect("GroupSessionsManagement.aspx");
        }


        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Response.Redirect("EquipmentManagement.aspx");
        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaymentsManagement.aspx");
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberManagement.aspx");
        }

        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainersManagement.aspx");
        }

        protected void LinkButton12_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReservationsManagement.aspx");
        }
    }
}
