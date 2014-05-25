<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HighScore.aspx.cs" Inherits="Presentation.MemberPages.HighScore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link id="Link1" rel="stylesheet" runat="server" href="~/Stylesheets/QuizStylesheet.css" />
    <title>Quizzer</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Information" runat="server" Text="Label"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="GameDataSource" CssClass="Grid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="700px">
<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="Username" SortExpression="UserName" NullDisplayText="Anonymous" />
                    <asp:BoundField DataField="Score" HeaderText="Score" SortExpression="Score" />
                </Columns>

<PagerStyle CssClass="pgr"></PagerStyle>
            </asp:GridView>
            <asp:ObjectDataSource ID="GameDataSource" runat="server" SelectMethod="GetHighScoreList" TypeName="Business.GameMaster">
                <SelectParameters>
                    <asp:QueryStringParameter Name="quizId" QueryStringField="quizId" Type="Int64" />
                </SelectParameters>
            </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
