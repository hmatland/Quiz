<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteQuestion.aspx.cs" Inherits="Presentation.MemberPages.DeleteQuestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSourceQuestions">
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourceQuestions" runat="server"></asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
