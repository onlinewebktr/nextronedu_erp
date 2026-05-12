<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_stock_ledger.aspx.cs" Inherits="school_web.Dvlpr_Prof.Update_stock_ledger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="btn_update_purchase_stock_ledger" runat="server" OnClick="btn_update_purchase_stock_ledger_Click" Text="Update Purchase Stock" />

             <asp:Button ID="btn_update_sale_stock" runat="server" Text="Update Sale Stock" OnClick="btn_update_sale_stock_Click" />

        </div>
    </form>
</body>
</html>
