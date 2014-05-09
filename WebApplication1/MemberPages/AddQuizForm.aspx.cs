using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataObject;
using Business;
using System.Web.Security;


namespace Presentation.MemberPages
{
    public partial class AddQuizForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitNewQuiz(object sender, EventArgs e)
        {
            
            var quiz = new Quiz
            {
                Quizname = QuizTextBox.Text,
                MadeById = GameMaster.GetUserId(Membership.GetUser().UserName),
            };
            GameMaster.AddNewQuizToDb(quiz);
            Response.Redirect("AddQuestionForm.aspx");
        }

        

    }
}