<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuestionForm.aspx.cs" Inherits="Presentation.MemberPages.AddQuestionForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link rel="stylesheet" runat="server" href="~/Stylesheets/QuizStylesheet.css" />
        <title>Quizzer</title>
</head>
<body>
    <form id="AddQuestionWithAnswersForm" runat="server">
    <div>
        <h1>Welcome <asp:LoginName ID="LoginName1" runat="server" />.</h1><br />
        <asp:Label ID="QuestionLabel" runat="server" Text="Enter question: "></asp:Label>
        <asp:TextBox ID="QuestionTextBox" runat="server" Width="373px"></asp:TextBox>
        <br />
        <asp:Label ID="QuizDropDownListLabel" runat="server" Text="Add question to which quiz "></asp:Label><asp:DropDownList ID="QuizDropDownList" runat="server" DataTextField="Quizname" DataValueField="Id">
        </asp:DropDownList>
        <br />
        <asp:Label ID="AnswerLabel1" runat="server" Text="Correct Answer:"></asp:Label>
        <asp:TextBox ID="CorrectTextBox" runat="server" Width="361px"></asp:TextBox>
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
        <asp:Button ID="SubmitButton" CssClass="buttons" runat="server" Text="Add question with answers to DB" Width="319px" OnClick="SubmitQuestionWithAnswers" />
    
    </div>
    </form>
</body>
</html>
