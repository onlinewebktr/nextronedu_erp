<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create_ledger_for_student.aspx.cs" Inherits="school_web.Dvlpr_Prof.Create_ledger_for_student" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btn_Create_ledger" runat="server" Text="Student Create Ledger" OnClick="btn_Create_ledger_Click" />
            <asp:Button ID="btn_create_legder" runat="server" Text="Employee Create Ledger" OnClick="btn_create_legder_Click" />
            <asp:Button ID="btn_student_amount_send_data_receipt_voucher" runat="server" Text="Student Amount Voucher" OnClick="btn_student_amount_send_data_receipt_voucher_Click" />
        </div>
    </form>
</body>
</html>
