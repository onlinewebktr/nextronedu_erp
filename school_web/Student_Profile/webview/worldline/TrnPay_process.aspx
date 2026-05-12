<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrnPay_process.aspx.cs" Inherits="school_web.Student_Profile.webview.worldline.TrnPay_process" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pay</title>
    <script src="../../../assets/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function PostData() {
            document.getElementById("form1").submit();
        }
    </script>
</head>
<body onload="PostData()">
    
      <form id="form1" action="https://cgt.in.worldline-solutions.com/ipg/doMEPayRequest" runat="server" method="post">

   <%-- <form id="form1" action="https://ipg.in.worldline.com/doMEPayRequest" runat="server" method="post">--%>
        <div>
            <asp:Literal runat="server" ID="ltrPostData"></asp:Literal>
            <br />
            <asp:Label runat="server" ID="lbl"> Please Do not close and Refresh Browser</asp:Label>
        </div>
    </form>
</body>
</html>
