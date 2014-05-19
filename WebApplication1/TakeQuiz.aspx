<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TakeQuiz.aspx.cs" Inherits="Presentation.WebForm1" %>

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
                <asp:Label ID="Question" Width="350px" runat="server">What is HelloKitty?</asp:Label>
            </div>
            <div id ="questionButtonGroup">           
            <asp:Button ID="Answer1" runat="server" CssClass="questionButtons" Text="Button" Width="350px" OnClick="Answer_Click"/>
            <asp:Button ID="Answer2" runat="server" CssClass="questionButtons" Text="Button" Width="350px" OnClick="Answer_Click"/><br />
            <asp:Button ID="Answer3" runat="server" CssClass="questionButtons" Text="Button" Width="350px" OnClick="Answer_Click"/>
            <asp:Button ID="Answer4" runat="server" CssClass="questionButtons" Text="Button" Width="350px" OnClick="Answer_Click"/>
            <asp:Button ID="GameOver" runat="server" CssClass="questionButtons" Text="Button" Width="700px" OnClick="SaveGame" Visible="False"/>                
            </div>
            <br />
            <br />
            <div id="quitDiv">
            <asp:Button ID="Quit" runat="server" CssClass="quitButton" Text="Quit" OnClick ="Quit_Quiz"/>
            </div>
                        
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="GameDataSource" CssClass="Grid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="700px">
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="Username" SortExpression="UserName" />
                    <asp:BoundField DataField="Score" HeaderText="Score" SortExpression="Score" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="GameDataSource" runat="server" SelectMethod="GetHighScoreList" TypeName="Business.GameMaster">
                <SelectParameters>
                    <asp:QueryStringParameter Name="quizId" QueryStringField="quizId" Type="Int64" />
                </SelectParameters>
            </asp:ObjectDataSource>                        
        </form>
    </body>
</html>