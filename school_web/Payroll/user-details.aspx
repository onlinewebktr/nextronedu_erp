<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user-details.aspx.cs" Inherits="school_web.Payroll.user_details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>
    <link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&display=swap" rel="stylesheet" />
    <link href="../assets/css/Print.css" rel="stylesheet" />
    <script src="../assets/js/jquery-1.10.2.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&display=swap" rel="stylesheet"/>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <section class="section">
            <div class="print_form">
                <div class="prnt-btn-sec">
                    <div class="prnt-btn-sec-cntr">
                        <div class="print-btn-sec">
                            <div class="noPrint" style="float: left">
                                <asp:Button ID="btn_back" runat="server" ToolTip="Back" CssClass="back-btn" OnClick="btn_back_Click" />
                            </div>
                            <div class="noPrint" style="float: right">
                                <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" runat="server" ToolTip="Print" CssClass="print-btn"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="print-pg-sec" id="tblPrintIQ" runat="server">
                    <div class="print_form-inr">
                        <div class="print_form-img">
                            <h2 class="success-htitle-h">User  Details</h2>
                        </div>
                        <div class="print_form-dtls">
                            <table class="print_table">
                                <tr>
                                    <td colspan="4">
                                        <h2 class="messbox-sec-h2" style="width: 100%;">Personal Information</h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td>User Type : </td>
                                    <td>
                                        <asp:Label ID="lbl_emp_type" runat="server" Text=""></asp:Label>
                                    </td>


                                    <td>User Name :</td>
                                    <td>
                                        <asp:Label ID="lbl_emp_name" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Gender :</td>
                                    <td>
                                        <asp:Label ID="lbl_gender" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Date of Birth :</td>
                                    <td>
                                        <asp:Label ID="lbl_dob" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Blood Group :</td>
                                    <td>
                                        <asp:Label ID="lbl_blood_group" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Religion :</td>
                                    <td>
                                        <asp:Label ID="lbl_religion" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>


                                <tr>
                                    <td>Marital Status :</td>
                                    <td>
                                        <asp:Label ID="lbl_merital_status" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Father's/Husband Name :</td>
                                    <td>
                                        <asp:Label ID="lbl_father_name" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>PAN :</td>
                                    <td>
                                        <asp:Label ID="lbl_pan" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Address :</td>
                                    <td>
                                        <asp:Label ID="lbl_address" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>City :</td>
                                    <td>
                                        <asp:Label ID="lbl_city" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Pincode :</td>
                                    <td>
                                        <asp:Label ID="lbl_pincode" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>State :</td>
                                    <td>
                                        <asp:Label ID="lbl_state" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Email :</td>
                                    <td>
                                        <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Mobile No. :</td>
                                    <td>
                                        <asp:Label ID="lbl_mobile_no" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Bank Name :</td>
                                    <td>
                                        <asp:Label ID="lbl_bank_name" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Branch :</td>
                                    <td>
                                        <asp:Label ID="lbl_branch" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Account No. :</td>
                                    <td>
                                        <asp:Label ID="lbl_account_no" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>IFSC :</td>
                                    <td>
                                        <asp:Label ID="lbl_ifsc_code" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>MICR :</td>
                                    <td>
                                        <asp:Label ID="lbl_micr" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>



                                <tr>
                                    <td colspan="4">
                                        <h2 class="messbox-sec-h2" style="width: 100%;">Organization Details</h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Employee Code :</td>
                                    <td>
                                        <asp:Label ID="lbl_emp_code" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Punch Card No :</td>
                                    <td>
                                        <asp:Label ID="lbl_punch_card" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Official Email Id :</td>
                                    <td>
                                        <asp:Label ID="lbl_official_email" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Qualification :</td>
                                    <td>
                                        <asp:Label ID="lbl_qualification" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Grade :</td>
                                    <td>
                                        <asp:Label ID="lbl_grade" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Department :</td>
                                    <td>
                                        <asp:Label ID="lbl_department" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Designation :</td>
                                    <td>
                                        <asp:Label ID="lbl_designation" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>E.P.F. No. :</td>
                                    <td>
                                        <asp:Label ID="lbl_epf_no" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Join Date :</td>
                                    <td>
                                        <asp:Label ID="lbl_join_date" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>PF leaving Date :</td>
                                    <td>
                                        <asp:Label ID="lbl_pf_leaving_date" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Reason :</td>
                                    <td>
                                        <asp:Label ID="lbl_reson" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>ESIC No. :</td>
                                    <td>
                                        <asp:Label ID="lbl_esic_no" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Join Date :</td>
                                    <td>
                                        <asp:Label ID="lbl_join_date1" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>ESIC Leaving Date :</td>
                                    <td>
                                        <asp:Label ID="lbl_esci_leaving_date" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Reason :</td>
                                    <td>
                                        <asp:Label ID="lbl_reson1" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Date of Joining :</td>
                                    <td>
                                        <asp:Label ID="lbl_doj1" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
