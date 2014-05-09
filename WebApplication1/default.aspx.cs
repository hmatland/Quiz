using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void goToLoginPage(object sender, EventArgs e)
        {
            Response.Redirect("Account/Login.aspx");
        }

        protected void goToRegisterPage(object sender, EventArgs e)
        {
            Response.Redirect("Account/Register.aspx");
        }

        protected void goToTakeQuiz(object sender, EventArgs e)
        {
            Response.Redirect("TakeQuiz.aspx");
        }

        protected void editQuiz_Click(object sender, EventArgs e)
        {
            Response.Redirect("/MemberPages/AddQuestionForm.aspx");

        }

        protected void newQuiz(object sender, EventArgs e)
        {
            Response.Redirect("/MemberPages/AddQuizForm.aspx");
        }

    }
}