<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Presentation.MemberPages.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link id="Link1" rel="stylesheet" runat="server" href="~/Stylesheets/QuizStylesheet.css" />
        <title>Quizzer</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="changePasswordDiv">
        <h1>Change Password</h1>
        <p>
            <asp:ChangePassword ID="ChangePassword1" runat="server" ContinueDestinationPageUrl="~/default.aspx">
            </asp:ChangePassword>
        </p>
    </div>
    </form>
</body>
</html>
