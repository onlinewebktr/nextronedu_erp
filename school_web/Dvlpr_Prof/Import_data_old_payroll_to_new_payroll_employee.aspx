<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Import_data_old_payroll_to_new_payroll_employee.aspx.cs" Inherits="school_web.Dvlpr_Prof.Import_data_old_payroll_to_new_payroll_employee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btn_import_session" runat="server" OnClick="btn_import_session_Click" Text="Fetch Session" />

            <asp:Button ID="btn_fetch_Department_master" runat="server" OnClick="btn_fetch_Department_master_Click" Text="Department Master" />

            <asp:Button ID="btn_fetch_employe_data" runat="server" OnClick="btn_fetch_employe_data_Click" Text="Employee " />
        </div>
    </form>
</body>
</html>
