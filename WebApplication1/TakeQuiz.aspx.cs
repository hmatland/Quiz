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
        private long _gameId;
        private long _quizId;
        private long _questionId;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                StartNewQuizGame();
            }
            else
            {
                _gameId = (long)ViewState["gameId"];
                _quizId = (long) ViewState["quizId"];
                _questionId = (long)ViewState["questionId"];
            }

            
        }

        protected void StartNewQuizGame()
        {
            
            var isParsed = long.TryParse(Request.QueryString["quizId"], out _quizId);
            if (isParsed)
            {
                _gameId = -1;
                ViewState.Add("quizId", _quizId);
                ViewState.Add("gameId", _gameId);
                var questionWithAnswers = GameMaster.GetNextQuestionWithAnswers(_quizId, 0);
                if (questionWithAnswers != null)
                {
                    FillInQuestionAndAnswers(questionWithAnswers);
                    ViewState.Add("questionId", questionWithAnswers.Id);
                }
                else
                {
                    Question.Text = "The quiz is empty, please choose another quiz";
                }
            }
            else
            {
                Question.Text = "QuizID: " + _quizId + " is not an valid ID";
            }
        }

        protected void FillInQuestionAndAnswers(QuestionWithAnswers questionWithAnswers) 
        {
            Question.Text = questionWithAnswers.QuestionText;

            var answer1 = questionWithAnswers.Answers.ElementAt(0);
            Answer1.Text = answer1.answerText;
            Answer1.CommandArgument = answer1.ID.ToString();

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
            var commandArgument = ((Button) sender).CommandArgument;

            var answerId = long.Parse(commandArgument);
            var isCorrect = GameMaster.IsAnswerCorrect(answerId);
            
            if (_gameId == -1)
            {
                _gameId = GameMaster.InitializeGame(_quizId, isCorrect);
                ViewState.Add("gameId", _gameId);
            }
            else
            {
                GameMaster.UpdateGame(_gameId,isCorrect);   
            }
            
            Information.Text = "The answer: " + ((Button)sender).Text + " is: " + isCorrect;
            var questionWithAnswers = GameMaster.GetNextQuestionWithAnswers(_quizId, _questionId);
            if (questionWithAnswers == null)
                QuizComplete(_quizId);
            else
            {
                ViewState.Add("questionId", questionWithAnswers.Id);
                FillInQuestionAndAnswers(questionWithAnswers); 
            }
                
        }

        protected void QuizComplete(long quizId)
        {
            Question.Text = "Quiz: " + quizId +" has been completed!\n";
            Answer1.Visible = false;
            Answer2.Visible = false;
            Answer3.Visible = false;
            Answer4.Visible = false;
            var game = GameMaster.GetGame(_gameId);
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