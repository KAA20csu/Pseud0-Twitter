<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="PostPage.aspx.cs" Inherits="Twi.PostPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/head.css" rel="stylesheet" />
    <link href="css/postpage.css" rel="stylesheet" />
    <title>Обсуждение | FakeBird</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="huder">
            <a href="#">
                <img src="img/eblem.png" style="height: 68px" />
            </a>            
            </div>
            <div id="post">
                <div id="infoUser">
                    <asp:Image runat="server" ID="ava" />
                    <asp:Button runat="server" ID="nameUser" OnClick="nameUser_Click"></asp:Button>
                </div>
                <div id="content">
                    <asp:Label runat="server" ID="text"></asp:Label>        
                </div>
                 
            </div>
            <div id="comments">
                <asp:PlaceHolder runat="server" ID="place"></asp:PlaceHolder>
            </div>
            <div id="addCmDiv">
                <asp:TextBox runat="server" ID="comm" TextMode="MultiLine" Columns="30"></asp:TextBox>
                <asp:Button runat="server" OnClick="Bt_Click" ID="btComm" Text="Отправить"/>
            </div>
        </div>
       
        <input type="checkbox" id="nav-toggle" hidden="hidden"/>    
        <div class="nav">
            <label for="nav-toggle" class="nav-toggle"></label>
            <ul id="listMenu">     
                <asp:Button runat="server" Text="Выйти" OnClick="Unnamed_Click" ID="exitBt"/> 
                <asp:Button Text="Моя Страница" runat="server" PostBackUrl="~/UserPage.aspx" CssClass="menuBt"/>
                <asp:Button Text="Новостная Лента" runat="server" PostBackUrl="~/NewsPage.aspx" CssClass="menuBt"/>
            </ul>
        </div>  
    </form>
</body>
</html>
