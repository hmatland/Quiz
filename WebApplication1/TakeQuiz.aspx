﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TakeQuiz.aspx.cs" Inherits="Presentation.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <link id="Link1" rel="stylesheet" runat="server" href="~/Stylesheets/QuizStylesheet.css" />
        <title>Quizzer</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:Label ID="Information" runat="server"></asp:Label><br />

            </div>
            <div id="questionDiv">
                <asp:Label ID="Question" Width="356px" runat="server" Height="82px">What is HelloKitty?</asp:Label>
            </div>

            <p></p>
            <div id ="questionButtonGroup">           
            <asp:Button ID="Answer1" runat="server" CssClass="questionButtons" Text="Button" Width="350px" OnClick="Answer_Click"/>
            <asp:Button ID="Answer2" runat="server" CssClass="questionButtons" Text="Button" Width="350px" OnClick="Answer_Click"/><br />
            <asp:Button ID="Answer3" runat="server" CssClass="questionButtons" Text="Button" Width="350px" OnClick="Answer_Click"/>
            <asp:Button ID="Answer4" runat="server" CssClass="questionButtons" Text="Button" Width="350px" OnClick="Answer_Click"/>
            <asp:Button ID="GameOver" runat="server" CssClass="questionButtons" Text="Button" Width="700px" OnClick="SaveHighScore" Visible="False"/>
                
            </div>

            <asp:Button ID="Quit" runat="server" Text="Quit" OnClick ="Quit_Quiz"/>
                        
        </form>
    </body>
</html>