<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuizForm.aspx.cs" Inherits="Presentation.MemberPages.AddQuizForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quizzer</title>
</head>
<body>
    <form id="AddNewQuiz" runat="server">
    <div>
        <h1>Welcome <asp:LoginName ID="LoginName1" runat="server" />.</h1><br />
        <asp:Label ID="QuizLabel" runat="server" Text="Enter name of new quiz: "></asp:Label>
        <asp:TextBox ID="QuizTextBox" runat="server" Width="373px"></asp:TextBox>
        <br />
        <asp:Button ID="SubmitQuizButton" runat="server" Text="Add new quiz" Width="174px" OnClick="SubmitNewQuiz" />
    
    </div>
    </form>
</body>
</html>
