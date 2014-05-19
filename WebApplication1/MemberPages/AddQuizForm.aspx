<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuizForm.aspx.cs" Inherits="Presentation.MemberPages.AddQuizForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link id="Link1" rel="stylesheet" runat="server" href="~/Stylesheets/QuizStylesheet.css" />
    <title>Quizzer</title>
</head>
<body>
    <form id="AddNewQuiz" runat="server">
    <div>
        <h1>Welcome <asp:LoginName ID="LoginName1" runat="server" />.</h1><br />
        <asp:Label ID="QuizLabel" runat="server" Text="Enter name of new quiz: "></asp:Label>
        <asp:TextBox ID="QuizTextBox" runat="server" Height="25px" Width="373px"></asp:TextBox>
        &nbsp;<asp:Button ID="SubmitQuizButton" runat="server" CssClass="mainMenuButton" Text="Add new quiz" Height="25px" Width="174px" OnClick="SubmitNewQuiz" />
    
        <br />
        <br />
        <asp:Button ID="Back_button" runat="server" CssClass="quitButton" OnClick="Back_button_Click" Text="Back" />
    
    </div>
    </form>
</body>
</html>
