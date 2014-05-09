using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using DataObject;

namespace Presentation.MemberPages
{
    public partial class AddQuestionForm : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long quizId = (long)Session["sendId"];
            QuizName.Text = (string)GameMaster.GetQuizName(quizId);

           /* var quizesOwnedByUser = GameMaster.GetQuizes(Membership.GetUser().UserName);
            foreach (var quiz in quizesOwnedByUser)
            {
                QuizDropDownList.Items.Add(new ListItem(quiz.Quizname,quiz.Id.ToString()));
            }*/
        }

        protected void SubmitQuestionWithAnswers(object sender, EventArgs e)
        {
            var questionWithAnswers = new QuestionWithAnswers
            {
                QuestionText = QuestionTextBox.Text,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        answerText = CorrectTextBox.Text,
                        isCorrect = true
                    },
                    new Answer
                    {
                        answerText = WrongTextBox1.Text,
                        isCorrect = false
                    },
                    new Answer
                    {
                        answerText = WrongTextBox2.Text,
                        isCorrect = false
                    },
                    new Answer
                    {
                        answerText = WrongTextBox3.Text,
                        isCorrect = false
                    }
                }
            };
            long quizId = (long)Session["sendId"];
            GameMaster.AddQuestionWithAnswers(questionWithAnswers, quizId);
            //Response.Redirect("default.aspx?id="+id);
        }
    }
}