<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Presentation.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <link id="Link1" rel="stylesheet" runat="server" href="~/Stylesheets/QuizStylesheet.css" />
        <title>Quizzer</title>
        <style type="text/css">
            .auto-style1 {
                text-align: left;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:Label ID="Information" runat="server"></asp:Label><br />

            </div>
            <div id="questionDiv">
                <asp:Label ID="Question" Width="350px" runat="server">What is HelloKitty?</asp:Label>
            </div>

            <p></p>
            <div id ="buttonGroup">           
            <asp:Button ID="Answer1" runat="server" CssClass="buttons" Text="Button" Width="350px" OnClick="Answer_Click"/>
            <asp:Button ID="Answer2" runat="server" CssClass="buttons" Text="Button" Width="350px" OnClick="Answer_Click"/><br />
            <asp:Button ID="Answer3" runat="server" CssClass="buttons" Text="Button" Width="350px" OnClick="Answer_Click"/>
            <asp:Button ID="Answer4" runat="server" CssClass="buttons" Text="Button" Width="350px" OnClick="Answer_Click"/>
            </div>
                        
        </form>
    </body>
</html>