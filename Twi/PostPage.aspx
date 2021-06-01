<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="PostPage.aspx.cs" Inherits="Twi.PostPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:PlaceHolder runat="server" ID="pst"></asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="cmntt"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
