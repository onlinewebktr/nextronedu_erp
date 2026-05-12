<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payfinal.aspx.cs" Inherits="school_web.Monthly_Payments.payfinal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pay</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />

    <script src="../assets/js/jquery-1.10.2.min.js"></script>
    <link href="../assets/css/app.css" rel="stylesheet" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="btn_reset" runat="server" Text="Back To Pay Again" OnClick="btn_reset_Click" CssClass="btn btn-primary" />
            <asp:Button ID="Button1" runat="server" Text="Pay Again" OnClick="Button1_Click" CssClass="btn btn-success" />

            <asp:HiddenField ID="hd_key" runat="server" />
            <asp:HiddenField ID="hd_amount" runat="server" />

            <asp:HiddenField ID="hd_fname" runat="server" />
            <asp:HiddenField ID="hd_email" runat="server" />
            <asp:HiddenField ID="hd_mobile" runat="server" />
            <asp:HiddenField ID="hd_razor_odr_id" runat="server" />
            <asp:HiddenField ID="hd_order_id" runat="server" />
            <asp:HiddenField ID="hd_logo" runat="server" />
            <asp:HiddenField ID="hd_regid" runat="server" />
            <asp:HiddenField ID="hd_schoolname" runat="server" />
             

 
        </div>
    </form>
</body>
</html>
