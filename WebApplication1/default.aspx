﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Presentation._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link id="Link1" rel="stylesheet" runat="server" href="~/Stylesheets/QuizStylesheet.css" />
    <title>Quizzer</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="welcomeLabel" runat="server" Text="TestDefaultPage"></asp:Label>
        <br />
        <br />
        <asp:LoginStatus ID="LoginStatus1" runat="server" />
    
        <br />
        <br />
        <asp:DropDownList ID="ChooseQuizDropDown" runat="server" DataTextField="Quizname" DataValueField="Id">
        </asp:DropDownList>
        <br />
        <br />
         <asp:Button ID="TakeQuiz" runat="server" Text="Take quiz" OnClick ="goToTakeQuiz" />
        <br />
        <br />
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                <asp:Button ID="Register" runat="server" OnClick="goToRegisterPage" Text="Register" />
            </AnonymousTemplate>
            <LoggedInTemplate>
                <asp:Button ID="NewQuiz" runat="server" Text="Make New Quiz" OnClick="newQuiz" />
                <br />
                <asp:Button ID="editQuiz" runat="server" Text="Edit Quiz" OnClick="editQuiz_Click" Width="100px" />
            </LoggedInTemplate>
        </asp:LoginView>
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
