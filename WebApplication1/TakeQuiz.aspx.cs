using System;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Business;
using DataObject;

namespace Presentation
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public string QuestionText;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var questionWithAnswers = GameMaster.GetQuestionWithAnswers();
                QuestionText = questionWithAnswers.QuestionText;
                FillInQuestionAndAnswers(questionWithAnswers);
            }
            
        }

        protected void FillInQuestionAndAnswers(QuestionWithAnswers questionWithAnswers)
        {
            Question.Text = questionWithAnswers.QuestionText;

            var answer1 = questionWithAnswers.Answers.ElementAt(0);
            Answer1.Text = answer1.answerText;
            Answer1.CommandArgument = answer1.ID.ToString(CultureInfo.InvariantCulture);

            var answer2 = questionWithAnswers.Answers.ElementAt(1);
            Answer2.Text = answer2.answerText;
            Answer2.CommandArgument = answer2.ID.ToString(CultureInfo.InvariantCulture);

            var answer3 = questionWithAnswers.Answers.ElementAt(2);
            Answer3.Text = answer3.answerText;
            Answer3.CommandArgument = answer3.ID.ToString(CultureInfo.InvariantCulture);

            var answer4 = questionWithAnswers.Answers.ElementAt(3);
            Answer4.Text = answer4.answerText;
            Answer4.CommandArgument = answer4.ID.ToString(CultureInfo.InvariantCulture);

        }

        protected void Answer_Click(object sender, EventArgs e)
        {
            var answerId  = long.Parse(((Button)sender).CommandArgument);
            Information.Text = "The answer: " + ((Button)sender).Text + " is: " + GameMaster.IsAnswerCorrect(answerId).ToString();
            var questionWithAnswers = GameMaster.GetQuestionWithAnswers();
            QuestionText = questionWithAnswers.QuestionText;
            FillInQuestionAndAnswers(questionWithAnswers);
        }

    }
}