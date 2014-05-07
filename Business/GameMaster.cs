using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using DataObject;

namespace Business
{
    public static class GameMaster
    {
        public static QuestionWithAnswers GetQuestionWithAnswers()
        {
            var questionWithAnswer = DataAccessMachine.GetQuestionWithAnswers();
            questionWithAnswer.Answers = ShuffleAnswers(questionWithAnswer.Answers);
            return questionWithAnswer;
        }

        public static List<Answer> ShuffleAnswers(List<Answer> answers)
        {
            var shuffledAnswers = new List<Answer>();
            var rng = new Random();
            for (var i = 3; i >= 0; i--)
            {
                var index = rng.Next(i);
                shuffledAnswers.Add(answers.ElementAt(index));
                answers.RemoveAt(index);
            }

            return shuffledAnswers;
        }

        public static Boolean IsAnswerCorrect(Answer answer)
        {
            return answer.isCorrect;
        }

        public static Boolean IsAnswerCorrect(long answerId)
        {
            var answer = DataAccessMachine.GetAnswer(answerId);
            return answer.isCorrect;
        }

        public static void AddQuestionWithAnswers(QuestionWithAnswers questionWithAnswers, long quizId)
        {
            DataAccessMachine.AddQuestionWithAnswersToDb(questionWithAnswers, quizId);
        }

        public static List<Quiz> GetQuizes(string username)
        {
            long userId = DataAccessMachine.GetUserId(username);
            return DataAccessMachine.GetQuizes(userId);
        }

        public static long GetUserId(string username)
        {
            return DataAccessMachine.GetUserId(username);
        }
    
    
    }
}