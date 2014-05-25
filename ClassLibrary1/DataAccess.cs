using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using DataAccess.DataSetTableAdapters;
using DataObject;

namespace DataAccess
{
    public static class DataAccessMachine
    {
        private const string ConnectionString = "Data Source=(local);Initial Catalog=QuizDB;Integrated Security=SSPI";

        public static long? SaveGameToDb(Game game)
        {
            long? insertedId = 0;
            var tableAdapter = new GameTableAdapter();
            tableAdapter.Insert(game.UserId, game.Score, game.QuizId, ref insertedId);
            return insertedId;
        }

        public static List<Game> GetHighScoreList(long quizId)
        {
            var tableAdapter = new GameTableAdapter();
            var dataTable = tableAdapter.GetTopTenGames(quizId);
            var list = new List<Game>();
            foreach (var row in dataTable)
            {
                var game = new Game()
                {
                    Id = row.GameId,
                    QuizId = row.QuizId,
                    Score = row.Score,
                };
                if (row.IsNull("UserId"))
                {
                    game.UserId = null;
                    game.UserName = null;
                }
                else
                {
                    game.UserId = row.UserId;
                    game.UserName = GetUserName(row.UserId);
                }
                list.Add(game);

            }
            return list;
        }

        public static string GetUserName(long userId)
        {
            var tableAdapter = new UserTableAdapter();
            var result = tableAdapter.GetUserName(userId);
            foreach (var row in result)
            {
                return row.Name;
            }
            return null;
        }

        public static void DeleteQuestionWithAnswersFromDb(long questionId)
        {
            var tableAdapter = new QuestionTableAdapter();
            tableAdapter.DeleteQuestionWithAnswers(questionId);
            
            /*Old version with stored procedure, may be removed if tableAdapter works as it should.
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open(); 
                var cmd =
                    new SqlCommand("DeleteQuestionWithAnswers",
                        connection) {CommandType = CommandType.StoredProcedure};
                cmd.Parameters.Add("@questionid", SqlDbType.BigInt).Value = questionId;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }*/
        }
            
        public static void AddNewQuizToDb(Quiz quiz)
        {
            var tableAdapter = new QuizTableAdapter();
            tableAdapter.Insert(quiz.Quizname, quiz.MadeById);
        }

        /*public static void AddQuestionWithAnswersToDb(QuestionWithAnswers questionWithAnswers, long quizId)
        {
            AddQuestionToDb(questionWithAnswers.QuestionText, quizId);
            var questionId = GetIdOfQuestionText(questionWithAnswers.QuestionText);
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                foreach (var answer in questionWithAnswers.Answers)
                {
                    var answerCmd =
                        new SqlCommand(
                            "INSERT INTO Answer (Text, IsCorrect, QuestionId) VALUES (@Text, @IsCorrect, @QuestionId)",
                            connection);
                    answerCmd.Parameters.Add(@"Text", SqlDbType.VarChar, 250).Value = answer.answerText;
                    answerCmd.Parameters.Add(@"IsCorrect", SqlDbType.Bit).Value = answer.isCorrect;
                    answerCmd.Parameters.Add(@"QuestionId", SqlDbType.BigInt).Value = questionId;
                    answerCmd.Prepare();
                    answerCmd.ExecuteNonQuery();
                }
            }
        }*/

        public static void AddQuestionToDb(string questionText, long quizId)
        {
            var tableAdapter = new QuestionTableAdapter();
            tableAdapter.Insert(questionText, quizId);
            
            /*using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var questionCmd = new SqlCommand("INSERT INTO Question (Text, QuizId) VALUES (@Text, @QuizId)",
                    connection);
                questionCmd.Parameters.Add(@"Text", SqlDbType.VarChar, 250).Value = questionText;
                questionCmd.Parameters.Add(@"QuizId", SqlDbType.BigInt).Value = quizId;
                questionCmd.Prepare();
                questionCmd.ExecuteNonQuery();
            }*/
        }

        public static void AddAnswerToDb(Answer answer)
        {
            var tableAdapter = new AnswerTableAdapter();
            tableAdapter.Insert(answer.answerText, answer.isCorrect, answer.questionID);
        }

        public static void AddUserNameToQuizDb(string username)
        {
            var tableAdapter = new UserTableAdapter();
            tableAdapter.Insert(username);
        }

        public static IEnumerable<Quiz> GetAllQuizes()
        {
            var tableAdapter = new QuizTableAdapter();
            var dataTable = tableAdapter.GetData();
            foreach (var row in dataTable)
            {
                var quiz = new Quiz
                {
                    Id = row.QuizId,
                    MadeById = row.UserId,
                    Quizname = row.Name
                };
                yield return quiz;
            }
        }

        public static Answer GetAnswer(long answerId)
        {
            var tableAdapter = new AnswerTableAdapter();
            var dataTable = tableAdapter.SelectAnswer(answerId);

            foreach (var row in dataTable)
            {
               var answer = new Answer
               {
                   ID = answerId,
                   answerText = row.Text,
                   isCorrect = row.IsCorrect,
                   questionID = row.QuestionId
               };
                return answer;
            }
            
            return null;
            
        }

        public static Game GetGame(long gameId)
        {
            var tableadapter = new GameTableAdapter();
            var datatable = tableadapter.GetGame(gameId);
            foreach (var row in datatable)
            {
                var game = new Game
                {
                    QuizId = row.QuizId,
                    Score = row.Score,
                    Id = gameId
                };
                if (row.IsUserIdNull())
                    game.UserId = null;
                else
                    game.UserId = row.UserId;
                return game;
            }
            return null;
            

            /*using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var quizCmd = new SqlCommand("SELECT * FROM Game WHERE GameId = @GameId", connection);
                quizCmd.Parameters.Add("@GameId", SqlDbType.BigInt).Value = gameId;
                quizCmd.Prepare();
                var reader = quizCmd.ExecuteReader();
                
                while (reader.Read())
                {
                    var game = new Game
                    {
                        UserId = reader["UserId"] as int?,
                        QuizId = (long) reader["QuizId"],
                        Score = (int) reader["Score"],
                        Id = (long) reader["GameId"]
                    };
                    return game;
                }
                return null;
            }*/
        }

        public static long GetIdOfQuestionText(string questionText)
        {
            var tableAdapter = new QuestionTableAdapter();
            var datatable = tableAdapter.GetIdFromQuestionText(questionText);
            foreach(var row in datatable){
                return row.QuestionId;
            }
            return -1;


            /*using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var questionCmd = new SqlCommand("SELECT QuestionId FROM Question WHERE Text = @Text",
                    connection);
                questionCmd.Parameters.Add(@"Text", SqlDbType.VarChar, 250).Value = questionText;
                questionCmd.Prepare();
                var reader = questionCmd.ExecuteReader();
                while (reader.Read())
                {
                    return (long) reader["QuestionId"];
                }
                return -1;
            }*/
        }

        public static List<Answer> GetListOfAnswers(long questionId)
        {
            var tableAdapter = new AnswerTableAdapter();
            var dataTable = tableAdapter.GetListOfAnswers(questionId);
            var answers = new List<Answer>();
            foreach (var row in dataTable) { 
                var answer = new Answer
                {
                    answerText = row.Text,
                    ID = row.AnswerId,
                    isCorrect = row.IsCorrect,
                    questionID = questionId
                };
                answers.Add(answer);
            }
            return answers;
            /*using (var connection = new SqlConnection(ConnectionString))
            {
                var answers = new List<Answer>();
                connection.Open();
                var questionCmd = new SqlCommand("SELECT * FROM Answer WHERE QuestionID = @ID",
                    connection);
                questionCmd.Parameters.Add(@"ID", SqlDbType.BigInt).Value = questionId;
                questionCmd.Prepare();
                var reader = questionCmd.ExecuteReader();
                while (reader.Read())
                {
                    var answer = new Answer
                    {
                        answerText = (string) reader["Text"],
                        ID = (long) reader["AnswerId"],
                        isCorrect = (Boolean) reader["IsCorrect"],
                        questionID = (long) reader["QuestionID"]
                    };
                    answers.Add(answer);
                }
                return answers;
            }*/
        }

        public static QuestionWithAnswers GetNextQuestion(long quizId, long previousQuestionId)
        {
            var tableAdapter = new QuestionTableAdapter();
            var dataTable = tableAdapter.GetNextQuestion(previousQuestionId, quizId);
            foreach (var row in dataTable) {
                var questionWithAnswers = new QuestionWithAnswers
                {
                    Id = row.QuestionId,
                    QuestionText = row.Text,
                    QuizId = quizId
                };
                return questionWithAnswers;
            }
            return null;

            /*using (var connection = new SqlConnection(ConnectionString))
            {
                var questionWithAnswers = new QuestionWithAnswers();
                connection.Open();
                var questionCmd =
                    new SqlCommand("SELECT * FROM Question WHERE QuestionId > @QuestionId AND QuizId = @QuizId",
                        connection);
                questionCmd.Parameters.Add(@"QuestionId", SqlDbType.BigInt).Value = previousQuestionId;
                questionCmd.Parameters.Add(@"QuizId", SqlDbType.BigInt).Value = quizId;
                questionCmd.Prepare();
                var reader = questionCmd.ExecuteReader();
                while (reader.Read())
                {
                    questionWithAnswers.Id = (long) reader["QuestionId"];
                    questionWithAnswers.QuestionText = (string) reader["Text"];
                    questionWithAnswers.QuizId = (long) reader["QuizId"];
                    return questionWithAnswers;
                }
            }
            return null;*/
        }

        public static List<QuestionWithAnswers> GetQuestions(long quizId)
        {
            var tableAdapter = new QuestionTableAdapter();
            var dataTable = tableAdapter.GetQuestions(quizId);
            var questions = new List<QuestionWithAnswers>();
            foreach (var row in dataTable)
            {
                var question = new QuestionWithAnswers
                {
                    Id = row.QuestionId,
                    QuestionText = row.Text,
                    QuizId = row.QuizId
                };
                questions.Add(question);
            }
            return questions;
        }

        public static string GetQuizName(long quizId)
        {
            var tableAdapter = new QuizTableAdapter();
            var dataTable = tableAdapter.GetQuizName(quizId);
            foreach (var row in dataTable) 
            {
                return row.Name;
            }
            return "";
            
            /*using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var quizCmd = new SqlCommand("SELECT Name FROM Quiz WHERE QuizId = @ID", connection);
                quizCmd.Parameters.Add(@"ID", SqlDbType.BigInt).Value = quizId;
                quizCmd.Prepare();
                var reader = quizCmd.ExecuteReader();
                while (reader.Read())
                {
                    return (string) reader["Name"];
                }
                return "";
            }*/
        }

        public static List<Quiz> GetQuizes(long userId)
        {
            var tableAdapter = new QuizTableAdapter();
            var dataTable = tableAdapter.GetQuizes(userId);
            var quizes = new List<Quiz>();

            foreach (var row in dataTable)
            {
                var quiz = new Quiz
                {
                    Id = row.QuizId,
                    MadeById = userId,
                    Quizname = row.Name
                };
                quizes.Add(quiz);
            }
            return quizes;
            /*using (var connection = new SqlConnection(ConnectionString))
            {
                var quizes = new List<Quiz>();
                connection.Open();
                var questionCmd = new SqlCommand("SELECT * FROM Quiz WHERE UserId = @ID",
                    connection);
                questionCmd.Parameters.Add(@"ID", SqlDbType.BigInt).Value = userId;
                questionCmd.Prepare();
                var reader = questionCmd.ExecuteReader();
                while (reader.Read())
                {
                    var quiz = new Quiz
                    {
                        Id = (long) reader["QuizId"],
                        MadeById = (long) reader["UserId"],
                        Quizname = (string) reader["Name"]
                    };
                    quizes.Add(quiz);
                }
                return quizes;
            }*/
        }

        public static long GetUserId(string username)
        {
            var tableAdapter = new UserTableAdapter();
            var dataTable = tableAdapter.GetUserId(username);
            foreach (var row in dataTable)
            {
                return row.UserId;
                
            }
            return -1;
            /*using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var questionCmd = new SqlCommand("SELECT UserId FROM [User] WHERE Name = @username",
                    connection);
                questionCmd.Parameters.Add(@"username", SqlDbType.VarChar, 50).Value = username;
                questionCmd.Prepare();
                var reader = questionCmd.ExecuteReader();
                while (reader.Read())
                {
                    return (long) reader["UserId"];
                }
                return -1;
            }*/
        }

        public static long GetUserId(long quizId)
        {
            var tableAdapter = new QuizTableAdapter();
            var dataTable = tableAdapter.GetUserId(quizId);
            foreach (var row in dataTable)
            {
                return row.UserId;

            }
            return -1;
            /*using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var questionCmd = new SqlCommand("SELECT UserId FROM [Quiz] WHERE QuizId = @quizid",
                    connection);
                questionCmd.Parameters.Add(@"quizid", SqlDbType.BigInt).Value = quizId;
                questionCmd.Prepare();
                var reader = questionCmd.ExecuteReader();
                while (reader.Read())
                {
                    return (long) reader["UserId"];
                }
                return -1;
            }*/
        }


        public static void UpdateGame(Game game)
        {
            var tableAdapter = new GameTableAdapter();
            tableAdapter.Update(game.UserId, game.Score,game.QuizId,game.Id);
        }
    }
}