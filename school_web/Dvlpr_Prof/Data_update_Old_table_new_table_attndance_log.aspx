<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Data_update_Old_table_new_table_attndance_log.aspx.cs" Inherits="school_web.Dvlpr_Prof.Data_update_Old_table_new_table_attndance_log" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 0px; padding: 0px; height: 30px; width: 100%;">
            <asp:Button ID="btn_update_attendance_log" runat="server" Text="Attendance log Old to New" OnClick="btn_update_attendance_log_Click" />


             <asp:Button ID="btn_update" runat="server" Text="Attendance Update HR Daily Attendance Record" OnClick="btn_update_Click" />

             <asp:Button ID="btn_insert_one_row" runat="server" Text="Insert One Row First Month" OnClick="btn_insert_one_row_Click" />
        </div>
    </form>
</body>
</html>
