using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Business;
using DataObject;

namespace Presentation
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Session.Remove("gameId");
                long quizId;
                var isParsed = long.TryParse(Request.QueryString["quizId"], out quizId);
                if (isParsed)
                {
                    var questionWithAnswers = GameMaster.GetNextQuestionWithAnswers(quizId, 0);
                    if (questionWithAnswers != null)
                    {
                        FillInQuestionAndAnswers(questionWithAnswers);
                    }
                    else
                    {
                        Question.Text = "The quiz is empty, please choose another quiz";
                    }
                }
                else
                {
                    Question.Text = "QuizID: " + quizId + " is not an valid ID";
                }
            }
            
        }

        protected void FillInQuestionAndAnswers(QuestionWithAnswers questionWithAnswers) 
        {
            Question.Text = questionWithAnswers.QuestionText;

            var answer1 = questionWithAnswers.Answers.ElementAt(0);
            Answer1.Text = answer1.answerText;
            Answer1.CommandArgument = questionWithAnswers.QuizId.ToString()+";"+questionWithAnswers.Id.ToString()+";"+answer1.ID.ToString();

            var answer2 = questionWithAnswers.Answers.ElementAt(1);
            Answer2.Text = answer2.answerText;
            Answer2.CommandArgument = questionWithAnswers.QuizId.ToString()+";"+questionWithAnswers.Id.ToString()+";"+answer2.ID.ToString(CultureInfo.InvariantCulture);

            var answer3 = questionWithAnswers.Answers.ElementAt(2);
            Answer3.Text = answer3.answerText;
            Answer3.CommandArgument = questionWithAnswers.QuizId.ToString()+";"+questionWithAnswers.Id.ToString()+";"+answer3.ID.ToString(CultureInfo.InvariantCulture);

            var answer4 = questionWithAnswers.Answers.ElementAt(3);
            Answer4.Text = answer4.answerText;
            Answer4.CommandArgument = questionWithAnswers.QuizId.ToString()+";"+questionWithAnswers.Id.ToString()+";"+answer4.ID.ToString(CultureInfo.InvariantCulture);

        }

        protected void Answer_Click(object sender, EventArgs e)
        {
            var commandArguments = ((Button) sender).CommandArgument.Split(';');
            var quizId = long.Parse(commandArguments[0]);
            var questionId = long.Parse(commandArguments[1]);
            var answerId = long.Parse(commandArguments[2]);
            var isCorrect = GameMaster.IsAnswerCorrect(answerId);
            
            
            if (Session["gameId"] == null)
            {
                Session["gameId"] = GameMaster.InitializeGame(quizId, isCorrect);
            }
            else
            {
                GameMaster.UpdateGame((long)Session["gameId"],isCorrect);   
            }
            
            Information.Text = "The answer: " + ((Button)sender).Text + " is: " + isCorrect;
            var questionWithAnswers = GameMaster.GetNextQuestionWithAnswers(quizId, questionId);
            if (questionWithAnswers == null)
                QuizComplete(quizId);
            else
                FillInQuestionAndAnswers(questionWithAnswers);
        }

        protected void QuizComplete(long quizId)
        {
            Question.Text = "Quiz: " + quizId +" has been completed!\n";
            Answer1.Visible = false;
            Answer2.Visible = false;
            Answer3.Visible = false;
            Answer4.Visible = false;
            var game = GameMaster.GetGame((long)Session["gameId"]);
            GameOver.Visible = true;
            GameOver.Text = "All questions are answered. Your score is: " + game.Score;
        }

        protected void Quit_Quiz(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        protected void SaveHighScore(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}