<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteQuestion.aspx.cs" Inherits="Presentation.MemberPages.DeleteQuestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSourceQuestions" AllowPaging="True" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="QuestionText" HeaderText="QuestionText" SortExpression="QuestionText" />
                <asp:BoundField DataField="QuizId" HeaderText="QuizId" SortExpression="QuizId" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourceQuestions" runat="server" DeleteMethod="DeleteQuestionWithAnswersFromDb" InsertMethod="AddQuestionWithAnswers" SelectMethod="GetQuestionWithAnswers" TypeName="Business.GameMaster" UpdateMethod="AddQuestionWithAnswers">
            <DeleteParameters>
                <asp:Parameter Name="questionId" Type="Int64" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="questionWithAnswers" Type="Object" />
                <asp:Parameter Name="quizId" Type="Int64" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="questionWithAnswers" Type="Object" />
                <asp:Parameter Name="quizId" Type="Int64" />
            </UpdateParameters>
        </asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
