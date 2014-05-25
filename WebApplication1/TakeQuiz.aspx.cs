using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using Business;
using DataObject;

namespace Presentation
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private Game _game;
        private long _questionId;


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                StartNewQuizGame();
            }
            else
            {
                _game = (Game) ViewState["game"];
                _questionId = (long) ViewState["questionId"];
            }

            
        }

        protected void StartNewQuizGame()
        {
            long quizId;
            _game = new Game();
            
            var isParsed = long.TryParse(Request.QueryString["quizId"], out quizId);
            if (isParsed)
            {
                _game.QuizId = quizId;
                _game.Score = 0;
                _questionId = 0;
                var questionWithAnswers = GameMaster.GetNextQuestionWithAnswers(_game.QuizId, _questionId);
                if (questionWithAnswers != null)
                {
                    FillInQuestionAndAnswers(questionWithAnswers);
                    _questionId = questionWithAnswers.Id;
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
            ViewState.Add("game", _game);
            ViewState.Add("questionId", _questionId);

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
            if (isCorrect)
                _game.Score += 1;
           
            Information.Text = "The answer: " + ((Button)sender).Text + " is: " + isCorrect;
            var questionWithAnswers = GameMaster.GetNextQuestionWithAnswers(_game.QuizId, _questionId);
            ViewState.Add("game", _game);
            if (questionWithAnswers == null)
            {
                ViewState.Add("questionId", (long)-1);
                QuizComplete();              
            }
            else
            {
                ViewState.Add("questionId", questionWithAnswers.Id);
                FillInQuestionAndAnswers(questionWithAnswers); 
            }
                
        }

        protected void QuizComplete()
        {
            Question.Text = "Quiz: " + _game.QuizId +" has been completed!\n";
            Answer1.Visible = false;
            Answer2.Visible = false;
            Answer3.Visible = false;
            Answer4.Visible = false;
            GameOver.Visible = true;
            GameOver.Text = "All questions are answered. Your score is: " + _game.Score+"\n Press this button to see how you did compared to the highscorelist!\n (Requires log in)";
        }

        protected void Quit_Quiz(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        protected void SaveGame(object sender, EventArgs e)
        {
            
            var id = GameMaster.SaveGame(_game);
            Response.Redirect("/MemberPages/Highscore.aspx?gameId="+id+"&quizId="+_game.QuizId);
        }

    }
}