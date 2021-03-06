﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditQuizForm.aspx.cs" Inherits="Presentation.MemberPages.AddQuestionForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link rel="stylesheet" runat="server" href="~/Stylesheets/QuizStylesheet.css" />
        <title>Quizzer</title>
</head>
<body>
    <form id="AddQuestionWithAnswersForm" runat="server">
    <div>
        <div id="newQuestionDiv"><h1>Welcome <asp:LoginName ID="LoginName1" runat="server" />.</h1>
        <p>
            You are adding a question to: <asp:Label ID="QuizName" runat="server" Text="Label"></asp:Label>
        </p><br />
        <asp:Label ID="QuestionLabel" runat="server" Text="Enter question: "></asp:Label>
        <asp:TextBox ID="QuestionTextBox" runat="server" Width="354px"></asp:TextBox>
        <br />
        <asp:Label ID="AnswerLabel1" runat="server" Text="Correct Answer:"></asp:Label>
        <asp:TextBox ID="CorrectTextBox" runat="server" Width="355px"></asp:TextBox>
        <br />
        <asp:Label ID="AnswerLabel2" runat="server" Text="Wrong answer 1:"></asp:Label>
        <asp:TextBox ID="WrongTextBox1" runat="server" Width="355px"></asp:TextBox>
        <br />
        <asp:Label ID="AnswerLabel3" runat="server" Text="Wrong answer 2:"></asp:Label>
        <asp:TextBox ID="WrongTextBox2" runat="server" Width="355px"></asp:TextBox>
        <br />
        <asp:Label ID="AnswerLabel4" runat="server" Text="Wrong answer 3:"></asp:Label>
        <asp:TextBox ID="WrongTextBox3" runat="server" Width="353px"></asp:TextBox>
        <br />
        <asp:Button ID="SubmitButton" CssClass="mainMenuButton" runat="server" Text="Add question with answers to DB" Width="319px" OnClick="SubmitQuestionWithAnswers" />
        </div>
        <br />
        <br />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSourceQuestions" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound" CssClass="Grid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="QuestionText" HeaderText="QuestionText" SortExpression="QuestionText" />
                <asp:TemplateField HeaderText="Answers">
                    <ItemTemplate>
                        <!--<%# Container.DataItem.ToString() %>-->
                        <asp:BulletedList ID="blAnswer" Runat="server">
                        </asp:BulletedList>
                    </ItemTemplate>
                </asp:TemplateField>                
            </Columns>

<PagerStyle CssClass="pgr"></PagerStyle>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourceQuestions" runat="server" DeleteMethod="DeleteQuestionWithAnswersFromDb" SelectMethod="GetQuestionWithAnswers" TypeName="Business.GameMaster" InsertMethod="AddQuestionWithAnswers">
            <DeleteParameters>
                <asp:Parameter Name="id" Type="Int64" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="questionWithAnswers" Type="Object" />
                <asp:Parameter Name="quizId" Type="Int64" />
            </InsertParameters>
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="" Name="quizId" QueryStringField="quizId" Type="Int64" />
            </SelectParameters>
        </asp:ObjectDataSource>    
        <br />
        <div id="backbuttonDiv"><asp:Button ID="Back" runat="server" CssClass= "quitButton" OnClick="Back_Click" Text="Back" /></div>
        <br />
    </div>
    </form>
</body>
</html>
