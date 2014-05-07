using System;
using System.Collections.Generic;
using System.Web.UI;
using Business;
using DataObject;

namespace Presentation.MemberPages
{
    public partial class AddQuestionForm : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SubmitQuestionWithAnswers(object sender, EventArgs e)
        {
            var questionWithAnswers = new QuestionWithAnswers
            {
                questionText = QuestionTextBox.Text,
                answers = new List<Answer>
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
            var id = GameMaster.AddQuestionWithAnswers(questionWithAnswers);
            Response.Redirect("default.aspx?id="+id);
        }
    }
}