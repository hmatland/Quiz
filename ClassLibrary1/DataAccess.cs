using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataObject;

namespace DataAccess
{
    public static class DataAccessMachine
    {
        private const string ConnectionString = "Data Source=(local);Initial Catalog=Quiz;Integrated Security=SSPI";

        public static long AddQuestionWithAnswersToDb(QuestionWithAnswers questionWithAnswers)
        {
            AddQuestiontoDb(questionWithAnswers.questionText);
            var questionId = GetIdOfQuestionText(questionWithAnswers.questionText);
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                foreach (var answer in questionWithAnswers.answers)
                {
                    var answerCmd =
                        new SqlCommand(
                            "INSERT INTO [QUTAD\\n9058729].Answer (Text, IsCorrect, QuestionID) VALUES (@Text, @IsCorrect, @QuestionID)",
                            connection);
                    answerCmd.Parameters.Add(@"Text", SqlDbType.VarChar, 250).Value = answer.answerText;
                    answerCmd.Parameters.Add(@"IsCorrect", SqlDbType.Bit).Value = answer.isCorrect;
                    answerCmd.Parameters.Add(@"QuestionID", SqlDbType.BigInt).Value = questionId;
                    answerCmd.Prepare();
                    answerCmd.ExecuteNonQuery();
                }
            }
            return questionId;
        }

        public static long GetIdOfQuestionText(string questionText)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var questionCmd = new SqlCommand("SELECT ID FROM [QUTAD\\n9058729].Question WHERE Text = @Text",
                    connection);
                questionCmd.Parameters.Add(@"Text", SqlDbType.VarChar, 250).Value = questionText;
                questionCmd.Prepare();
                var reader = questionCmd.ExecuteReader();
                while (reader.Read())
                {
                    return (long) reader["ID"];
                }
                return -1;
            }
        }

        public static void AddQuestiontoDb(string questionText)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var questionCmd = new SqlCommand("INSERT INTO [QUTAD\\n9058729].Question (Text) VALUES (@Text)",
                    connection);
                questionCmd.Parameters.Add(@"Text", SqlDbType.VarChar, 250).Value = questionText;
                questionCmd.Prepare();
                questionCmd.ExecuteNonQuery();
            }
        }

        public static Answer GetAnswer(long answerId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var questionCmd = new SqlCommand("SELECT * FROM [QUTAD\\n9058729].Answer WHERE AnswerId = @ID",
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
                var questionCmd = new SqlCommand("SELECT TOP 1 * FROM [QUTAD\\n9058729].Question ORDER BY NEWID()",
                    connection);
                questionCmd.Prepare();
                var reader = questionCmd.ExecuteReader();
                while (reader.Read())
                {
                    questionWithAnswers.ID = (long) reader["ID"];
                    questionWithAnswers.questionText = (string) reader["Text"];
                }
            }
            questionWithAnswers.answers = GetListOfAnswers(questionWithAnswers.ID);
            return questionWithAnswers;
        }

        public static List<Answer> GetListOfAnswers(long id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var answers = new List<Answer>();
                connection.Open();
                var questionCmd = new SqlCommand("SELECT * FROM [QUTAD\\n9058729].Answer WHERE QuestionID = @ID",
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
    }
}