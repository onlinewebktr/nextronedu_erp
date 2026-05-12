<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Check_Out.aspx.cs" Inherits="school_web.Student_Profile.webview.Billdesk.Check_Out" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../js/jquery-1.10.2.min.js"></script>

    <script type="text/javascript">
        function generateAction() {
            document.frmPost.txtCustomerID.value = document.getElementById("txtCustomerID").value;
            document.frmPost.txtTxnAmount.value = document.getElementById("txtTxnAmount").value;
            document.frmPost.txtAdditionalInfo1.value = document.getElementById("txtAdditionalInfo1").value;
            document.frmPost.txtAdditionalInfo3.value = document.getElementById("txtAdditionalInfo3").value;
            document.frmPost.txtAdditionalInfo4.value = document.getElementById("txtAdditionalInfo4").value;
            document.frmPost.txtAdditionalInfo5.value = document.getElementById("txtAdditionalInfo5").value;
            document.frmPost.txtAdditionalInfo6.value = document.getElementById("txtAdditionalInfo6").value;
            document.frmPost.txtAdditionalInfo7.value = document.getElementById("txtAdditionalInfo7").value;
            document.frmPost.txt_RU.value = document.getElementById("txt_RU").value;
            // document.frmPost.action = "https://test.sbiepay.sbi/secure/AggregatorHostedListener";
            document.frmPost.submit();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:HiddenField ID="txtCustomerID" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtTxnAmount" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtAdditionalInfo1" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtAdditionalInfo3" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtAdditionalInfo4" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtAdditionalInfo5" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtAdditionalInfo6" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtAdditionalInfo7" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txt_RU" runat="server"></asp:HiddenField>
        </div>
    </form>




    <form name="frmPost" method="post"
        action='https://www.billdesk.com/pgidsk/pgijsp/MerchantPaymentoption.jsp'>
        <input type='hidden' name='txtCustomerID' value="" />
        <input type='hidden' name='txtTxnAmount' value="" />
        <input type='hidden' name=' txtAdditionalInfo1' value="" />
        <input type='hidden' name=' txtAdditionalInfo2'
            value='INR' />
        <input type='hidden' name=' txtAdditionalInfo3'
            value="" />
        <input type='hidden' name=' txtAdditionalInfo4' value="" />
        <input type='hidden' name=' txtAdditionalInfo5' value="" />
        <input type='hidden' name=' txtAdditionalInfo6' value="" />
        <input type='hidden' name=' txtAdditionalInfo7' value="" />
        <input type='hidden' name='RU'
            value="" />
    </form>
</body>
</html>
