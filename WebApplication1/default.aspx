<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Presentation._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link id="Link1" rel="stylesheet" runat="server" href="~/Stylesheets/QuizStylesheet.css" />
    <title>Quizzer</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="login">
            <asp:LoginStatus ID="LoginStatus1" runat="server" />   
        </div>
         
    <div id="mainMenuDiv">
        <asp:LoginView ID="LoginView2" runat="server">
            <AnonymousTemplate>
                <h1>Welcome</h1>
            </AnonymousTemplate>
            <LoggedInTemplate>
                <h1>Welcome <asp:LoginName ID="LoginName1" runat="server" />.</h1>
            </LoggedInTemplate>
        </asp:LoginView> 
        <br />
        <br />
        <asp:DropDownList ID="ChooseQuizDropDown" runat="server" DataTextField="Quizname" DataValueField="Id" Width="300px">
        </asp:DropDownList>
        <br />
         <asp:Button ID="TakeQuiz" runat="server" CssClass="mainMenuButton" Text="Take quiz" OnClick ="goToTakeQuiz" />
        <br />
        <br />        
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                <asp:Button ID="Register" runat="server" CssClass="mainMenuButton" OnClick="goToRegisterPage" Text="Register" />
            </AnonymousTemplate>
            <LoggedInTemplate>
                <asp:Button ID="NewQuiz" runat="server" CssClass="mainMenuButton" Text="Make New Quiz" OnClick="newQuiz" />
                <br />
                <br />
                <asp:DropDownList ID="EditQuizDropDown" runat="server" DataTextField="Quizname" DataValueField="Id" Width="300px">
                </asp:DropDownList>
                <br />
                <asp:Button ID="editQuiz" runat="server" CssClass="mainMenuButton" Text="Edit Quiz" OnClick="editQuiz_Click" />
            </LoggedInTemplate>
        </asp:LoginView>

        <br />
        <h2>Welcome to Quizzer.</h2>
Quizzer is made by:<br/>
Matland, Haakon Nymo n9058729<br/>
Chiem, Henriette Victoria n9058672<br/>
The interface is quite simple. Select the quiz you want to do to play!<br/>
If you want to add your own quiz/questions, please register.<br/>
If you already have a user, log in create new quizzes, or edit your existing ones.<br/>
Another good reason to register, or logging in, is that you have the possibility to save your name to the highscore list appearing when you complete a quiz.

        <br />    
    </div>
    </form>

</body>
</html>
