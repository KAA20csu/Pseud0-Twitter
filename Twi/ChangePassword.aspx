<%@ Page Language="C#" Async="True" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Twi.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/changePassword.css" rel="stylesheet" />
    <title>Восстановление | FakeBird</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="inputWindow">
            <img src="img/eblem.png" class="littlEbl" />
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server" Text="Введите mail"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="checkMail" CssClass="inputBox"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server" Text="Введите новый пароль"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="password1" CssClass="inputBox" TextMode="Password"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server" Text="Повторите пароль"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="password2" CssClass="inputBox" TextMode="Password"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button runat="server" ID="changeBt" CssClass="enterButton" Text="Изменить" OnClick="ChangeBt_Click"/>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
