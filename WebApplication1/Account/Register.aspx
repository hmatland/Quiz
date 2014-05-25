<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Presentation.Account.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link id="Link1" rel="stylesheet" runat="server" href="~/Stylesheets/QuizStylesheet.css" />
    <title>Register to Quizzer</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="registerDiv">
        <h1 style="width: 261px">Register</h1>
        <asp:CreateUserWizard ID="CreateUserWizard" runat="server" ContinueDestinationPageUrl="~/default.aspx" OnCreatedUser="CreateUserWizard_CreatedUser">
            <WizardSteps>
                <asp:CreateUserWizardStep runat="server" />
                <asp:CompleteWizardStep runat="server">
                </asp:CompleteWizardStep>
            </WizardSteps>
        </asp:CreateUserWizard>
        </div>
    </form>
</body>
</html>
