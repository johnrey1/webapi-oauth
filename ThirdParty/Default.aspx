<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ThirdParty.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Do you want some data?
        <p></p>
        <asp:Button ID="Button1" runat="server" Text="Get Some Data" OnClick="GetData"/>
        <asp:Button ID="Button2" runat="server" Text="AuthorizeApp" OnClick="AuthzApp"/>
    </div>
    </form>
</body>
</html>
