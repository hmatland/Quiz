using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.DataSetTableAdapters;
using DataObject;

namespace DataAccess
{
    public static class DataAccessMachine
    {
        private const string ConnectionString = "Data Source=(local);Initial Catalog=QuizDB;Integrated Security=SSPI";


        public static void DeleteQuestionFromDb(long questionId) 
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmd =
                    new SqlCommand("DELETE FROM Question WHERE QuestionId =@questionid",
                        connection);
                cmd.Parameters.Add("@questionid", SqlDbType.BigInt).Value = questionId;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteAllAnswers(long questionId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmd =
                    new SqlCommand("DELETE FROM Answer WHERE QuestionId = @questionid",
                        connection);
                cmd.Parameters.Add("@questionid", SqlDbType.BigInt).Value = questionId;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

            
        public static long AddGameToDb(long quizId, int score)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmd =
                    new SqlCommand("INSERT INTO Game (Score, QuizId) Output INSERTED.GameId VALUES (@Score, @QuizId)",
                        connection);
                cmd.Parameters.Add("@QuizId", SqlDbType.BigInt).Value = quizId;
                cmd.Parameters.Add("@Score", SqlDbType.Int).Value = score;
                cmd.Prepare();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return (long)reader["GameId"];
                }
            }
            return -1;
        }

        public static void AddNewQuizToDb(Quiz quiz)
        {
            var tableAdapter = new QuizTableAdapter();
            tableAdapter.Insert(quiz.Quizname, quiz.MadeById);
        }

        public static void AddQuestionWithAnswersToDb(QuestionWithAnswers questionWithAnswers, long quizId)
        {
            AddQuestiontoDb(questionWithAnswers.QuestionText, quizId);
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
        }

        public static void AddQuestiontoDb(string questionText, long quizId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var questionCmd = new SqlCommand("INSERT INTO Question (Text, QuizId) VALUES (@Text, @QuizId)",
                    connection);
                questionCmd.Parameters.Add(@"Text", SqlDbType.VarChar, 250).Value = questionText;
                questionCmd.Parameters.Add(@"QuizId", SqlDbType.BigInt).Value = quizId;
                questionCmd.Prepare();
                questionCmd.ExecuteNonQuery();
            }
        }

        public static void AddUserIdToGame(long gameId, long userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand("UPDATE Game SET UserId = @UserId WHERE GameId = @GameId", connection);
                cmd.Parameters.Add("@GameId", SqlDbType.BigInt).Value = gameId;
                cmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = userId;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public static void AddUserNameToQuizDb(string username)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var questionCmd = new SqlCommand("INSERT INTO [User] (Name) VALUES (@Name)",
                    connection);
                questionCmd.Parameters.Add(@"Name", SqlDbType.VarChar, 50).Value = username;
                questionCmd.Prepare();
                questionCmd.ExecuteNonQuery();
            }
        }

        public static List<Quiz> GetAllQuizes()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var quizes = new List<Quiz>();
                connection.Open();
                var getAllCmd = new SqlCommand("SELECT * FROM Quiz", connection);
                getAllCmd.Prepare();
                SqlDataReader reader = getAllCmd.ExecuteReader();
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
            }
        }

        public static Answer GetAnswer(long answerId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var questionCmd = new SqlCommand("SELECT * FROM Answer WHERE AnswerId = @ID",
                    connection);
                questionCmd.Parameters.Add(@"ID", SqlDbType.BigInt).Value = answerId;
                questionCmd.Prepare();
                var reader = questionCmd.ExecuteReader();
                while (reader.Read())
                {
                    var answer = new Answer
                    {
                        ID = answerId,
                        answerText = (string) reader["Text"],
                        isCorrect = (Boolean) reader["IsCorrect"],
                        questionID = (long) reader["QuestionID"]
                    };
                    return answer;
                }
                return null;
            }
        }

        public static Game GetGame(long gameId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var quizCmd = new SqlCommand("SELECT * FROM Game WHERE GameId = @GameId", connection);
                quizCmd.Parameters.Add("@GameId", SqlDbType.BigInt).Value = gameId;
                quizCmd.Prepare();
                var reader = quizCmd.ExecuteReader();
                var game = new Game();
                while (reader.Read())
                {
                    game.UserId = reader["UserId"] as int?;
                    game.QuizId = (long) reader["QuizId"];
                    game.Score = (int) reader["Score"];
                    game.Id = (long) reader["GameId"];
                    return game;
                }
                return null;
            }
        }

        public static long GetIdOfQuestionText(string questionText)
        {
            using (var connection = new SqlConnection(ConnectionString))
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
            }
        }

        public static List<Answer> GetListOfAnswers(long questionId)
        {
            using (var connection = new SqlConnection(ConnectionString))
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
            }
        }

        public static QuestionWithAnswers GetNextQuestion(long quizId, long previousQuestionId)
        {
            using (var connection = new SqlConnection(ConnectionString))
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
            return null;
        }

        public static List<QuestionWithAnswers> GetQuestions(long quizId) {

            using (var connection = new SqlConnection(ConnectionString))
            {
                var listOfQuestions = new List<QuestionWithAnswers>();
                
                connection.Open();
                var questionCmd = new SqlCommand("SELECT * FROM Question WHERE QuizId = @ID",
                    connection);
                questionCmd.Parameters.Add(@"ID", SqlDbType.BigInt).Value = quizId;
                questionCmd.Prepare();
                var reader = questionCmd.ExecuteReader();
                while (reader.Read())
                {
                   var questionWithAnswers = new QuestionWithAnswers
                   {
                    Id = (long) reader["QuestionId"],
                    QuestionText = (string) reader["Text"],
                    QuizId = (long) reader["QuizId"]
                   };
                    listOfQuestions.Add(questionWithAnswers);
                }  
                return listOfQuestions;
            }
        }

        public static string GetQuizName(long quizId)
        {
            using (var connection = new SqlConnection(ConnectionString))
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
            }
        }

        public static List<Quiz> GetQuizes(long userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
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
            }
        }

        public static long GetUserId(string username)
        {
            using (var connection = new SqlConnection(ConnectionString))
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
            }
        }

        public static long GetUserId(long quizId)
        {
            using (var connection = new SqlConnection(ConnectionString))
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
            }
        }

        public static void IncrementGameScore(long gameId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand("UPDATE Game SET Score = Score + 1 WHERE GameId = @GameId", connection);
                cmd.Parameters.Add("@GameId", SqlDbType.BigInt).Value = gameId;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }
}