<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ccavRequest_Handler.aspx.cs" Inherits="school_web.ccav.ccavRequest_Handler" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.10.2.min.js"></script> 
    <script type="text/javascript">
        $(document).ready(function () {
            $("#nonseamless").submit();
        });
    </script>
</head>
<body>
    <form id="nonseamless" method="post" name="redirect" action="https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction">
        <input type="hidden" id="encRequest" name="encRequest" value="<%=strEncRequest%>" />
        <input type="hidden" name="access_code" id="Hidden1" value="<%=strAccessCode%>" />
    </form>
</body>
</html>
