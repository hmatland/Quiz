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
            questionWithAnswer.answers = ShuffleAnswers(questionWithAnswer.answers);
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

        public static long AddQuestionWithAnswers(QuestionWithAnswers questionWithAnswers)
        {
            return DataAccessMachine.AddQuestionWithAnswersToDb(questionWithAnswers);
        }
    
    
    }
}