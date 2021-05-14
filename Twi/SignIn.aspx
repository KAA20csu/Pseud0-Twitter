<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="Twi.SignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" href="css/main.css"/>
<link rel="stylesheet" href="css/input.css"/>
    <title></title>
</head>
<body id="windowSighIn">
    <form id="form1" runat="server">
        <div id="inputWindow">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Login" runat="server" Text="Логин:" CssClass="ggg"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:TextBox OnTextChanged="LoginBox_TextChanged" CssClass="inputInfo" runat="server" ID="LoginBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Пароль:"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="PasswordBox" TextMode="Password"/>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button runat="server" OnClick="Login_Click"/>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
    </form>
</body>
</html>
