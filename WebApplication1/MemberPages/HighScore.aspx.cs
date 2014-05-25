using System;
using Business;
using DataObject;

namespace Presentation.MemberPages
{
    public partial class HighScore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long gameId;
            long quizId;
            Game game = null;
            if (long.TryParse(Request.QueryString["gameId"], out gameId))
            {
                game = GameMaster.GetGame(gameId);
            }
            if (game != null && game.UserId == null)
            {
                game.UserName = User.Identity.Name;
                game.UserId = GameMaster.GetUserId(game.UserName);
                GameMaster.UpdateGame(game);
            }
            if (long.TryParse(Request.QueryString["quizId"], out quizId))
                Information.Text = "Showing top 10 scores for quiz: " + GameMaster.GetQuizName(quizId);
            else
                Information.Text = "QuizId not found";

        }
    }
}