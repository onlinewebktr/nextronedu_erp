<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mis-report.aspx.cs" Inherits="school_web.mis.mis_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MIS Report</title>
    <style>
        table {
            border-spacing: 0;
            border-collapse: collapse;
        }

        td, th {
            padding: 0;
        }

        .table {
            border-collapse: collapse;
        }

            .table td, .table th {
                background-color: #fff;
            }

        * {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        :after, :before {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        /*table {
            width: 100%;
            padding: 0px;
            margin: 0px;
        }

            table tr th {
                padding: 5px 5px;
                margin: 0px;
                text-align: left;
                border: 1px solid #ddd;
                font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif;
                color: #444b5e;
                font-size: 14px;
            }

            table tr td {
                padding: 5px 5px;
                margin: 0px;
                text-align: left;
                border: 1px solid #ddd;
                font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif;
                color: #444b5e;
                font-size: 14px;
            }*/
    </style>
</head>
<body style="margin: 0px; padding: 0px; width: 100%">
    <form id="form1" runat="server">
        <asp:Panel ID="pnl_otp_mail" runat="server">
            <div style="width: 97%; float: left; margin: 0px; background: #FAFAFA;">
                <div style="width: 100%; float: left; margin: 0px; float: left; padding: 0px; background: #ffffff">
                    <p style="display: none; padding: 5px 5px; margin: 0px; text-align: left; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 18px;">
                        Dear Sir,
                        <br />
                        <span style="padding: 5px 0px; margin: 0px; text-align: left; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 15px;">Below is the updation summary on Purnank School ERP of date
                            <asp:Label ID="lbl_date1" runat="server"></asp:Label>.</span>
                    </p>
                    <div style="width: 100%; float: left; margin: 0px 0px 15px 0px; border-radius: 5px; padding: 10px; background: rgb(255, 255, 255); border-bottom: 2px solid rgb(234, 234, 234);">
                        <table style="width: 100%; float: left">
                            <tr>
                                <th colspan="2" style="text-align: center; font-weight: 500; font-size: 16px; padding: 5px 5px; margin: 0px; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e;">Summary of
                                    <asp:Label ID="lbl_school_name" runat="server"></asp:Label><asp:Label ID="lbl_school_address" runat="server"></asp:Label>
                                    on
                                    <asp:Label ID="lbl_date2" runat="server"></asp:Label></th>
                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Fee Collection</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_total_fee_collection" runat="server"></asp:Label></td>
                            </tr>

                             <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Additional Fee/Student</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_Additional_fee" runat="server"></asp:Label>/<asp:Label ID="lbl_Additional_fee_std" runat="server"></asp:Label></td>
                            </tr>


                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Deleted Receipt/Amount</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_deleted_bill" runat="server"></asp:Label>/<asp:Label ID="lbl_deleted_bill_amount" runat="server"></asp:Label></td>
                            </tr>


                           
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Modified Receipt/Amount</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_modified_bill" runat="server"></asp:Label>/<asp:Label ID="lbl_modified_bill_amount" runat="server"></asp:Label></td>
                            </tr>


                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Student's Attendance</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_attended_std" runat="server"></asp:Label>/<asp:Label ID="lbl_total_student" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Teacher's Attendance</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_attended_emp" runat="server"></asp:Label>/<asp:Label ID="lbl_total_emp" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">SMS Usage/Balance</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_aval_balance" runat="server"></asp:Label>/<asp:Label ID="lbl_total_given_msg" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Assignments by teacher</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_homework_assign" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">New/inactive student</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_new_std" runat="server"></asp:Label>/<asp:Label ID="lbl_inactive_std" runat="server"></asp:Label></td>
                            </tr>

                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Total outstanding for the session</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_total_outstainding" runat="server"></asp:Label></td>
                            </tr>
                             <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Total Expenses </td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_total_expenses" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </div>


                    <div style="width: 100%; float: left; margin: 0px 0px 15px 0px; border-radius: 5px; padding: 10px; background: rgb(255, 255, 255); border-bottom: 2px solid rgb(234, 234, 234);">
                        <table style="width: 100%; float: left">
                            <tr>
                                <th colspan="2" style="text-align: center; font-weight: 500; font-size: 16px; padding: 5px 5px; margin: 0px; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e;">Today's Paymode Wise Collection Summary</th>
                            </tr>
                            <asp:Repeater ID="rd_view_modewise" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;"><%#Eval("Payment_mode") %></td>
                                        <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;"><%#Eval("AmountPaid") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-weight: 600; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Total</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-weight: 600; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_total_modewise" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </div>


                    <div style="width: 100%; float: left; margin: 0px 0px 15px 0px; border-radius: 5px; padding: 10px; background: rgb(255, 255, 255); border-bottom: 2px solid rgb(234, 234, 234);">
                        <table style="width: 100%; float: left">
                            <tr>
                                <th colspan="2" style="text-align: center; font-weight: 500; font-size: 16px; padding: 5px 5px; margin: 0px; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e;">Today's User Wise Collection Summary</th>
                            </tr>
                            <asp:Repeater ID="rp_userwisecollection" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;"><%#Eval("UserBy") %></td>
                                        <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;"><%#Eval("AmountPaid") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-weight: 600; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Total</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-weight: 600; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_totalusewise" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </div>

                    


                    <div style="width: 100%; float: left; margin: 0px 0px 15px 0px; border-radius: 5px; padding: 10px; background: rgb(255, 255, 255); border-bottom: 2px solid rgb(234, 234, 234);">
                        <table style="width: 100%; float: left">
                            <tr>
                                <th colspan="2" style="text-align: center; font-weight: 500; font-size: 16px; padding: 5px 5px; margin: 0px; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e;">Student Attendance Statics</th>
                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Total Class in the School</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_total_classes" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Attendance marked of classes</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_total_marked_classes" runat="server"></asp:Label></td>

                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Today Marked</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_total_marked_student" runat="server"></asp:Label></td>

                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Present</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_total_present_student" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Absent</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_total_absent_student" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Leave</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_total_leave_student" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </div>

                    <div style="width: 100%; float: left; margin: 0px 0px 15px 0px; border-radius: 5px; padding: 10px; background: rgb(255, 255, 255); border-bottom: 2px solid rgb(234, 234, 234);">
                        <table style="width: 100%; float: left">
                            <tr>
                                <th colspan="2" style="text-align: center; font-weight: 500; font-size: 16px; padding: 5px 5px; margin: 0px; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e;">Staff Attendance Statics</th>
                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Total staff in the School</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_ttl_employee" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Present</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_ttl_attended_emps" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Absent</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_ttl_staff_absent" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">Leave</td>
                                <td style="padding: 5px 5px; margin: 0px; text-align: left; border: 1px solid #ddd; font-family: Google Sans, Roboto, RobotoDraft, Helvetica, Arial, sans-serif; color: #444b5e; font-size: 14px;">
                                    <asp:Label ID="lbl_total_leave_staff" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </asp:Panel>



        <asp:Button ID="btn_sendmail" OnClick="btn_sendmail_Click" runat="server" Text="SendMail" />

        <asp:Label ID="lbl_s_msg" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>
