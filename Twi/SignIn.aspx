<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="Twi.SignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    
    <title> Войти | FakeBird </title>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/loginWindow.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="inputWindow">
            <img src="img/eblem.png" class="littlEbl" />
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server" Text="Логин:"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="LoginBox" CssClass="inputBox"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server" Text="Пароль:"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="PasswordBox" TextMode="Password" CssClass="inputBox"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button Text="Войти" runat="server" OnClick="Login_Click" CssClass="enterButton"/><br/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:HyperLink runat="server" NavigateUrl="~/ChangePassword.aspx" CssClass="linkwa" Text="Забыли пароль?"/>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
