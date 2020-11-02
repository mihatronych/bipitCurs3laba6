<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebCl.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 530px;
            width: 224px;
        }
    </style>
</head>
<body style="height: 535px">
    <form id="form1" runat="server">
        <div>
            <div class="auto-style1" style="padding: 20px; position: fixed; height: 100%; width: 20%;">
                ID:
                <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True"></asp:TextBox>
                <br />
                Книга:
                <br />
                <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                Читатель:
                <asp:DropDownList ID="DropDownList3" runat="server" Width="200px">
                </asp:DropDownList>
                <br />
                Выдано с:<br />
                <asp:TextBox ID="TextBox2" runat="server" ReadOnly="True"></asp:TextBox>
                <br />
                Выдано до:<br />
                <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                <br />
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Отправить" />
                <br />
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                <asp:TextBox ID="TextBox3" runat="server" Visible="False"></asp:TextBox>
            </div>
        </div>
        <div style="padding: 20px; width: 78%; height: 100%; margin-left: 25%; margin-top: -18px; overflow: scroll;">
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
