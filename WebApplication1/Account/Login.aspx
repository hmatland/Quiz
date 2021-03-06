﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentation.Account.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link id="Link1" rel="stylesheet" runat="server" href="~/Stylesheets/QuizStylesheet.css" />
        <title>Login to Quizzer</title>
        <style type="text/css">
            #loginPageDiv {
                width: 308px;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>   
        <div id="loginPageDiv">
            <h1 style="width: 309px">Login</h1>
            <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/default.aspx">
            <LayoutTemplate>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator><br/>
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator><br/>
                <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal><br/>
                <asp:Button ID="LoginButton" runat="server" CssClass="mainMenuButton" CommandName="Login" Text="Log In" ValidationGroup="Login1" Width="100%" />
            </LayoutTemplate>
           </asp:Login>
        </div>   
    </div>
    </form>
</body>
</html>
