﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using DataObject;

namespace Business
{
    public class GameMaster
    {
        public static QuestionWithAnswers GetNextQuestionWithAnswers(long quizId, long previousQuestionId)
        {
            var questionWithAnswers = DataAccessMachine.GetNextQuestion(quizId, previousQuestionId);
            if (questionWithAnswers == null)
                return null;
            questionWithAnswers.Answers = DataAccessMachine.GetListOfAnswers(questionWithAnswers.Id);
            questionWithAnswers.Answers = ShuffleAnswers(questionWithAnswers.Answers);
            return questionWithAnswers;
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
            DataAccessMachine.AddQuestionToDb(questionWithAnswers.QuestionText,quizId);
            foreach ( var answer in questionWithAnswers.Answers)
            {
                DataAccessMachine.AddAnswerToDb(answer);
            }
            
        }

        public static List<Quiz> GetQuizes(string username)
        {
            var userId = DataAccessMachine.GetUserId(username);
            return DataAccessMachine.GetQuizes(userId);
        }

        public static List<Quiz> GetAllQuizes() 
        {
            return DataAccessMachine.GetAllQuizes().ToList();
        }

        public static long GetUserId(string username)
        {
            return DataAccessMachine.GetUserId(username);
        }


        public static void AddUserNameToQuizDb(string userName)
        {
            DataAccessMachine.AddUserNameToQuizDb(userName);
        }

        public static void AddNewQuizToDb(Quiz quiz) {
            DataAccessMachine.AddNewQuizToDb(quiz);
        }

        public static long? SaveGame(Game game)
        {
            return DataAccessMachine.SaveGameToDb(game);
        }

        public static List<Game> GetHighScoreList(long quizId)
        {
            return DataAccessMachine.GetHighScoreList(quizId);
        } 

        public static Game GetGame(long gameId)
        {
            return DataAccessMachine.GetGame(gameId);
        }

        public static string GetQuizName(long quizId)
        {
            return DataAccessMachine.GetQuizName(quizId);
        }

        public static Boolean CheckIfRightUser(long quizId, long userId)
        {
            long quizBelongsToUserId = DataAccessMachine.GetUserId(quizId);
            if (quizBelongsToUserId == userId)
                return true;
            return false;
        }

        public static void DeleteQuestionWithAnswersFromDb(long id)
        {
            DataAccessMachine.DeleteQuestionWithAnswersFromDb(id);
        }

        public static List<QuestionWithAnswers> GetQuestionWithAnswers(long quizId) 
        {
            var questionsWithAnswers = DataAccessMachine.GetQuestions(quizId);
            foreach (var question in questionsWithAnswers)
            {
                question.Answers = DataAccessMachine.GetListOfAnswers(question.Id);
            }
            return questionsWithAnswers;
        }

        public static void UpdateGame(Game game)
        {
            DataAccessMachine.UpdateGame(game);
        }
    }
}