<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="Twi.UserPage"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/userPage.css" rel="stylesheet" />
    <link href="css/head.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form runat="server" id="data">
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
        <div id="content">
            <asp:TextBox runat="server" ID="PostBox"></asp:TextBox>
            <asp:Button runat="server" Text="Создать публикацию" OnClick="GoToChat" CssClass="enterButton"/>             
            <asp:FileUpload runat="server" ID="newAva"/>
            <asp:Button runat="server" Text="Загрузить фото" OnClick="Upload_Click"/>
            <asp:Button runat="server" Text="Выйти" OnClick="Unnamed_Click" /> 
            <div>
                <asp:Label runat="server" ID="PostLabel"></asp:Label>
            </div>
            
        </div>
        
    </div>    
    <input type="checkbox" id="nav-toggle" hidden="hidden"/>    
        <div class="nav">
            <label for="nav-toggle" class="nav-toggle"></label>
            <ul>     
                
            </ul>
        </div>  
    </form>
</body>
</html>
