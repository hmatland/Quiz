using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using DataObject;
using System.Web.Security;
namespace Presentation
{
    public partial class _default : System.Web.UI.Page
    {
        public long QuizId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var getAllQuizes = GameMaster.GetAllQuizes();
            foreach (var quiz in getAllQuizes)
            {
                ChooseQuizDropDown.Items.Add(new ListItem(quiz.Quizname, quiz.Id.ToString()));
            }
            if (Request.IsAuthenticated)
            {
                var quizesOwnedByUser = GameMaster.GetQuizes(Membership.GetUser().UserName);
                if (quizesOwnedByUser.Count == 0)
                {
                    LoginView1.FindControl("editQuiz").Visible = false;
                    LoginView1.FindControl("EditQuizDropDown").Visible = false;
                }
                foreach (var quiz in quizesOwnedByUser)
                {
                    var dropDownList = (DropDownList)LoginView1.FindControl("EditQuizDropDown");
                    dropDownList.Items.Add(new ListItem(quiz.Quizname, quiz.Id.ToString()));
                }
            }

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
            var quizId = long.Parse(ChooseQuizDropDown.SelectedValue);
            Response.Redirect("TakeQuiz.aspx?quizId=" + quizId);
        }

        protected void editQuiz_Click(object sender, EventArgs e)
        {
            var dropDownList = (DropDownList)LoginView1.FindControl("EditQuizDropDown");
            var quizId = long.Parse(dropDownList.SelectedValue); 
            Server.Transfer("/MemberPages/AddQuestionForm.aspx?quizId="+quizId);

        }

        protected void newQuiz(object sender, EventArgs e)
        {
            Response.Redirect("/MemberPages/AddQuizForm.aspx");
        }

       
    }
}