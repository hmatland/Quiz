﻿using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using DataObject;

namespace Presentation.MemberPages
{
    public partial class AddQuestionForm : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long quizId;
            bool quizIdParsed = long.TryParse(Request.QueryString["quizId"],out quizId);
            long userId = GameMaster.GetUserId(Membership.GetUser().UserName);
            bool isRightUser = GameMaster.CheckIfRightUser(quizId, userId);
            if (!isRightUser)
                Response.Redirect("~/default.aspx");
            if (quizIdParsed)
            {
                QuizName.Text = (string)GameMaster.GetQuizName(quizId);
            }
            else
            {
                Response.Redirect("~/default.aspx");
            }

           /* var quizesOwnedByUser = GameMaster.GetQuizes(Membership.GetUser().UserName);
            foreach (var quiz in quizesOwnedByUser)
            {
                QuizDropDownList.Items.Add(new ListItem(quiz.Quizname,quiz.Id.ToString()));
            }*/
        }

        protected void SubmitQuestionWithAnswers(object sender, EventArgs e)
        {
            var questionWithAnswers = new QuestionWithAnswers
            {
                QuestionText = QuestionTextBox.Text,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        answerText = CorrectTextBox.Text,
                        isCorrect = true
                    },
                    new Answer
                    {
                        answerText = WrongTextBox1.Text,
                        isCorrect = false
                    },
                    new Answer
                    {
                        answerText = WrongTextBox2.Text,
                        isCorrect = false
                    },
                    new Answer
                    {
                        answerText = WrongTextBox3.Text,
                        isCorrect = false
                    }
                }
            };
            long quizId = long.Parse(Request.QueryString["quizId"]);
            GameMaster.AddQuestionWithAnswers(questionWithAnswers, quizId);
            //Response.Redirect("default.aspx?id="+id);
        }
    }
}