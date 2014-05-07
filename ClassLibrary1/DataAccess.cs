using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataObject;

namespace DataAccess
{
    public static class DataAccessMachine
    {
        private const string ConnectionString = "Data Source=(local);Initial Catalog=QuizDB;Integrated Security=SSPI";

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

        public static QuestionWithAnswers GetQuestionWithAnswers()
        {
            var questionWithAnswers = new QuestionWithAnswers();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var questionCmd = new SqlCommand("SELECT TOP 1 * FROM Question ORDER BY NEWID()",
                    connection);
                questionCmd.Prepare();
                var reader = questionCmd.ExecuteReader();
                while (reader.Read())
                {
                    questionWithAnswers.Id = (long) reader["QuestionId"];
                    questionWithAnswers.QuestionText = (string) reader["Text"];
                }
            }
            questionWithAnswers.Answers = GetListOfAnswers(questionWithAnswers.Id);
            return questionWithAnswers;
        }

        public static List<Answer> GetListOfAnswers(long id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var answers = new List<Answer>();
                connection.Open();
                var questionCmd = new SqlCommand("SELECT * FROM Answer WHERE QuestionID = @ID",
                    connection);
                questionCmd.Parameters.Add(@"ID", SqlDbType.BigInt).Value = id;
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
                    return (long)reader["UserId"];
                }
                return -1;
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
    }
}