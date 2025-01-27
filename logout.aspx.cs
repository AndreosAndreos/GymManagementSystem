using System;
using System.Web;

namespace Dlutto_management
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Session.Clear();
            
            Session.Abandon();

            Response.Redirect("Homepage.aspx");
        }
    }
}
