<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="Twi.UserPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/userPage.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <div class="container">
        <div class="huder">
            <a href="#">
                <img src="img/eblem.png" style="height: 68px" />
            </a>            
        </div> 
        <div id="backface">
            <div id="face"></div>
            <asp:Label runat="server" Text="Your Login" ID="AuthorizedLogName" CssClass="nameUser"></asp:Label>            
        </div>
    </div>
    <input type="checkbox" id="nav-toggle" hidden="hidden"/>    
        <div class="nav">
            <label for="nav-toggle" class="nav-toggle"></label>
            <ul>
               <li><a href="#1" class="li">Один</a></li>
               <li><a href="#2" class="li">Два</a></li>           
            </ul>
        </div>                
        
        <%--<form id="form1" runat="server">        
            <div>
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Your Login" ID="AuthorizedLogName"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Your Mail" ID="Mail"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Your Sex" ID="Sex"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </form>--%>
    
</body>
</html>
