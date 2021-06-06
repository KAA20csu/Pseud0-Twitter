<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="UserProfileToOther.aspx.cs" Inherits="Twi.UserProfileToOther" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/head.css" rel="stylesheet" />
    <link href="css/userPage.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="huder">
                <a href="#">
                    <img src="img/eblem.png" style="height: 68px" />
                </a>   
            </div>
            <div id="backface">
                <asp:Image ID="face" runat="server"/><br/>
                <asp:Label runat="server" Text="Your Login" ID="AuthorizedLogName" CssClass="nameUser"></asp:Label>  
            </div>
            <div class="content">
                <asp:PlaceHolder runat="server" ID="Posts"></asp:PlaceHolder>
            </div>
        </div>        
        <input type="checkbox" id="nav-toggle" hidden="hidden"/>    
        <div class="nav">
            <label for="nav-toggle" class="nav-toggle"></label>
            <ul>     
                <asp:Button runat="server" Text="Выйти" OnClick="Unnamed_Click" ID="exitBt"/> 
                <asp:Button Text="Моя Страница" runat="server" PostBackUrl="~/UserPage.aspx" CssClass="menuBt"/>
                <asp:Button Text="Новостная Лента" runat="server" PostBackUrl="~/NewsPage.aspx" CssClass="menuBt"/>
            </ul>
        </div>    
    </form>
</body>
</html>
